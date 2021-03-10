﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.AI.TimingRules;

  public class DefenderOfChaos : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Defender of Chaos")
        .ManaCost("{2}{R}")
        .Type("Creature Human Knight")
        .Text("{Protection from white}, {Flash}")
        .FlavorText("Some knights will not follow orders—only disorder.")
        .Power(2)
        .Toughness(1)
        .SimpleAbilities(Static.Flash)
        .Protections(CardColor.White)
        .Cast(p => p.TimingRule(new AfterOpponentDeclaresAttackers()));
    }
  }
}