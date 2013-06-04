﻿namespace Grove.UserInterface.Permanent
{
  using System.Collections.Generic;
  using System.Linq;
  using Gameplay;
  using Infrastructure;

  public class CombatMarkers
  {
    private readonly List<int> _available = Enumerable.Range(1, 100).ToList();
    private readonly Dictionary<Card, int> _used = new Dictionary<Card, int>();

    public int GenerateMarker(Card card)
    {
      if (_used.ContainsKey(card))
      {
        return _used[card];
      }
      
      var marker = _available.Pop();
      _used.Add(card, marker);
      return marker;
    }    

    public void ReleaseMarker(Card card)
    {
      int marker;
      if (_used.TryGetValue(card, out marker))
      {
        _used.Remove(card);
        _available.Add(marker);
        _available.Sort((e1, e2) => e1.CompareTo(e2));
      }
    }
  }
}