﻿namespace Grove.Artifical.TimingRules
{
  using System;
  using System.Linq;
  using Gameplay;

  public class WhenOpponentControllsPermanents : TimingRule
  {
    private readonly int _minCount;
    private readonly Func<Card, bool> _selector;

    private WhenOpponentControllsPermanents() {}

    public WhenOpponentControllsPermanents(Func<Card, bool> selector = null, int minCount = 1)
    {
      _minCount = minCount;
      _selector = selector ?? delegate { return true; };
    }

    public override bool ShouldPlay(TimingRuleParameters p)
    {
      return Players.Permanents().Count(x => _selector(x) && x.Controller != p.Controller) >= _minCount;
    }
  }
}