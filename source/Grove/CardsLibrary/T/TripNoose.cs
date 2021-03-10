﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Costs;
  using Grove.Effects;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;

  public class TripNoose : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Trip Noose")
        .ManaCost("{2}")
        .Type("Artifact")
        .Text("{2},{T}: Tap target creature.")
        .FlavorText("A taut slipknot trigger is the only thing standing between you and standing.")
        .Cast(p => p.TimingRule(new OnFirstMain()))
        .ActivatedAbility(p =>
          {
            p.Text = "{2},{T}: Tap target creature.";
            p.Cost = new AggregateCost(
              new PayMana(2.Colorless()),
              new Tap());
            p.Effect = () => new TapTargets();
            p.TargetSelector.AddEffect(trg => trg.Is.Creature().On.Battlefield());
            p.TimingRule(new OnStep(Step.BeginningOfCombat));
            p.TargetingRule(new EffectTapCreature());
          });
    }
  }
}