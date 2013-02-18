﻿namespace Grove.Core.Effects
{
  using System;

  public class PutIntoPlay : Effect
  {
    private readonly Func<Effect, bool> _tapIf;

    private PutIntoPlay() {}

    public PutIntoPlay(Func<Effect, bool> tapIf)
    {
      _tapIf = tapIf;
    }

    public PutIntoPlay(bool tap = false) : this(delegate { return tap; }) {}

    protected override void ResolveEffect()
    {
      if (_tapIf(this))
      {
        Source.OwningCard.Tap();
      }
    }
  }
}