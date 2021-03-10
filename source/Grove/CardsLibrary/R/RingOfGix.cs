﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Costs;
  using Grove.Effects;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;

  public class RingOfGix : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Ring of Gix")
        .Type("Artifact")        
        .ManaCost("{3}")
        .Text("{Echo} {3}{EOL}{1},{T}: Tap target artifact, creature, or land.")
        .FlavorText("Not every cage is made of bars.")
        .Echo("{3}")
        .ActivatedAbility(p =>
          {
            p.Text = "{1},{T}: Tap target artifact, creature, or land.";

            p.Cost = new AggregateCost(
              new PayMana(1.Colorless()),
              new Tap());

            p.Effect = () => new TapTargets();

            p.TargetSelector.AddEffect(trg => trg.Is.Card(
              c => c.Is().Creature || c.Is().Land || c.Is().Artifact).On.Battlefield());

            p.TimingRule(new OnStep(Step.BeginningOfCombat));
            p.TargetingRule(new EffectTapCreature());
          });
    }
  }
}