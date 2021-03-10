﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Costs;
  using Grove.Effects;
  using Grove.AI;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;

  public class PhyrexianBroodlings : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Phyrexian Broodlings")
        .ManaCost("{1}{B}{B}")
        .Type("Creature Minion")
        .Text("{1}, Sacrifice a creature: Put a +1/+1 counter on Phyrexian Broodlings.")
        .FlavorText(
          "With limited resources and near unlimited time, Kerrick used parts from one beast to build another.")
        .Power(2)
        .Toughness(2)
        .ActivatedAbility(p =>
          {
            p.Text = "{1}, Sacrifice a creature: Put a +1/+1 counter on Phyrexian Broodlings.";

            p.Cost = new AggregateCost(
              new PayMana(1.Colorless()),
              new Sacrifice());

            p.Effect = () => new ApplyModifiersToSelf(() => new AddCounters(
              () => new PowerToughness(1, 1), count: 1)).SetTags(EffectTag.IncreasePower, EffectTag.IncreaseToughness);

            p.TargetSelector.AddCost(trg => trg.Is.Creature(ControlledBy.SpellOwner).On.Battlefield());
            
            p.TimingRule(new PumpOwningCardTimingRule(1, 1));
            p.TargetingRule(new EffectOrCostRankBy(c => c.Score) {TargetLimit = 1, ConsiderTargetingSelf = false});
          });
    }
  }
}