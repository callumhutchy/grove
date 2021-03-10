﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using System.Linq;
  using Grove.Effects;
  using Grove.AI.TargetingRules;

  public class Corrupt : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Corrupt")
        .ManaCost("{5}{B}")
        .Type("Sorcery")
        .Text(
          "Corrupt deals damage equal to the number of Swamps you control to target creature or player. You gain life equal to the damage dealt this way.")
        .FlavorText("Yawgmoth brushed Urza's mind, and Urza's world convulsed.")
        .Cast(cp =>
          {
            cp.Effect = () => new DealDamageToTargets(
              amount: P(e => e.Controller.Battlefield.Count(x => x.Is("swamp"))),
              gainLife: true);

            cp.TargetSelector.AddEffect(trg => trg.Is.CreatureOrPlayer().On.Battlefield());

            cp.TargetingRule(new EffectDealDamage(p =>
              p.Controller.Battlefield.Count(x => x.Is("swamp"))));
          });
    }
  }
}