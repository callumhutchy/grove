﻿namespace Grove.Core.Decisions.Results
{
  using System.Collections.Generic;
  using Infrastructure;

  [Copyable]
  public abstract class Playable
  {
    public ActivationParameters ActivationParameters;
    public Card Card;
    public int Index;
    public virtual bool WasPriorityPassed { get { return false; } }
    public Player Controller { get { return Card.Controller; } }

    public virtual bool CanPlay()
    {
      return true;
    }

    public virtual void Play() {}

    public abstract List<Playable> Replicate(int count);
  }
}