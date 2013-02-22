﻿namespace Grove.Core.Ai.TimingRules
{
  using System.Linq;

  public class All : TimingRule
  {
    private readonly TimingRule[] _rules;

    private All() {}

    public All(params TimingRule[] rules)
    {
      _rules = rules;
    }

    public override bool ShouldPlay(TimingRuleParameters p)
    {
      return _rules.All(x => x.ShouldPlay(p));
    }

    public override void Initialize(Game game)
    {
      foreach (var rule in _rules)
      {
        rule.Initialize(game);
      }
    }
  }
}