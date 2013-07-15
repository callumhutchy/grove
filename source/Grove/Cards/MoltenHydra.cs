﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Artifical;
  using Artifical.RepetitionRules;
  using Artifical.TargetingRules;
  using Artifical.TimingRules;
  using Gameplay;
  using Gameplay.Costs;
  using Gameplay.Counters;
  using Gameplay.Effects;
  using Gameplay.ManaHandling;
  using Gameplay.Misc;
  using Gameplay.Modifiers;

  public class MoltenHydra : CardsSource
  {
    public override IEnumerable<CardFactory> GetCards()
    {
      yield return Card
        .Named("Molten Hydra")
        .ManaCost("{1}{R}")
        .Type("Creature Hydra")
        .Text(
          "{1}{R}{R}: Put a +1/+1 counter on Molten Hydra.{EOL}{T},Remove all +1/+1 counters from Molten Hydra: Molten Hydra deals damage to target creature or player equal to the number of +1/+1 counters removed this way.")
        .Power(1)
        .Toughness(1)
        .ActivatedAbility(p =>
          {
            p.Text = "{1}{R}{R}: Put a +1/+1 counter on Molten Hydra.";
            p.Cost = new PayMana("{1}{R}{R}".Parse(), ManaUsage.Abilities, supportsRepetitions: true);
            p.Effect = () => new ApplyModifiersToSelf(() => new AddCounters(
              () => new PowerToughness(1, 1), count: 1)) {Category = EffectCategories.ToughnessIncrease};
            
            p.TimingRule(new Any(new IncreaseOwnersPowerOrToughness(1, 1), new EndOfTurn()));
            p.RepetitionRule(new MaxRepetitions());
          })
        .ActivatedAbility(p =>
          {
            p.Text =
              "{T},Remove all +1/+1 counters from Molten Hydra: Molten Hydra deals damage to target creature or player equal to the number of +1/+1 counters removed this way.";

            p.Cost = new AggregateCost(
              new Tap(),
              new RemoveCounters(CounterType.PowerToughnes));

            p.Effect =
              () => new DealDamageToTargets(P(e => e.Source.OwningCard.CountersCount(CounterType.PowerToughnes)));
            
            p.TargetSelector.AddEffect(trg => trg.Is.CreatureOrPlayer().On.Battlefield());
            
            p.TimingRule(new OwningCardHas(c => c.CountersCount(CounterType.PowerToughnes) > 0));
            p.TargetingRule(new DealDamage(p1 => p1.Card.CountersCount(CounterType.PowerToughnes)));            
            p.TimingRule(new TargetRemoval());
          });

    }
  }
}