﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Costs;
  using Grove.Effects;
  using Grove.AI;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;

  public class PhyrexianDebaser : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Phyrexian Debaser")
        .ManaCost("{3}{B}")
        .Type("Creature Carrier")
        .Text("{Flying}{EOL}{T}, Sacrifice Phyrexian Debaser: Target creature gets -2/-2 until end of turn.")
        .FlavorText("The second stage of the illness: high fever and severe infectiousness.")
        .Power(2)
        .Toughness(2)
        .SimpleAbilities(Static.Flying)
        .ActivatedAbility(p =>
          {
            p.Text = "{T}, Sacrifice Phyrexian Debaser: Target creature gets -2/-2 until end of turn.";
            p.Cost = new AggregateCost(
              new Tap(),
              new Sacrifice());

            p.Effect = () => new ApplyModifiersToTargets(
              () => new AddPowerAndToughness(-2, -2) {UntilEot = true}) {ToughnessReduction = 2};

            p.TargetSelector.AddEffect(trg => trg.Is.Creature().On.Battlefield());                        
            
            p.TargetingRule(new EffectReduceToughness(2));
            
            p.TimingRule(new Any(
              new WhenOwningCardWillBeDestroyed(), 
              new TargetRemovalTimingRule(removalTag: EffectTag.ReduceToughness, combatOnly: true)));
          });
    }
  }
}