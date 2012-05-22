﻿namespace Grove.Infrastructure
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using log4net;

  public class ChangeTracker : ICopyable
  {
    private static readonly ILog Log = LogManager.GetLogger(typeof (ChangeTracker));
    private readonly Stack<Action> _changeHistory = new Stack<Action>();
    private bool _isEnabled;

    public void Copy(object original, CopyService copyService)
    {
      var org = (ChangeTracker) original;
      _isEnabled = org._isEnabled;
    }

    public Snapshot CreateSnapshot()
    {
      Log.Debug("Create snapshot.");

      if (!_isEnabled)
        throw new InvalidOperationException("Tracker is disabled, did you forget to enable it?");

      return new Snapshot(_changeHistory.Count);
    }

    public void Disable()
    {
      _isEnabled = false;
      _changeHistory.Clear();
    }

    public ChangeTracker Enable()
    {
      _isEnabled = true;
      return this;
    }

    public void NotifyCollectionWillBeCleared<T>(ITrackableCollection<T> trackableCollection)
    {
      if (!_isEnabled)
        return;

      var elements = trackableCollection.ToList();

      if (elements.Count == 0)
        return;

      _changeHistory.Push(() => {
        foreach (var element in elements)
        {
          trackableCollection.AddWithoutTracking(element);
        }
      });
    }

    public void NotifyValueAdded<T>(ITrackableCollection<T> trackableCollection, T added)
    {
      if (!_isEnabled)
        return;

      _changeHistory.Push(() => trackableCollection.RemoveWithoutTracking(added));
    }

    public void NotifyValueChanged<T>(ITrackableValue<T> trackableValue)
    {
      if (!_isEnabled)
        return;

      var value = trackableValue.Value;
      _changeHistory.Push(() => trackableValue.Value = value);
    }

    public void NotifyValueWillBeRemoved<T>(ITrackableCollection<T> trackableCollection, T removed)
    {
      if (!_isEnabled)
        return;

      var index = trackableCollection.IndexOf(removed);

      if (index == -1)
        return;

      _changeHistory.Push(() => trackableCollection.InsertWithoutTracking(index, removed));
    }

    public void Restore(Snapshot snapshot)
    {
      Log.Debug("Restore from snapshot.");

      if (!_isEnabled)
        throw new InvalidOperationException("Tracker is disabled, did you forget to enable it?");

      while (_changeHistory.Count > snapshot.History)
      {
        var action = _changeHistory.Pop();
        action();
      }
    }
  }
}