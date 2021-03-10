﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Costs;
  using Grove.Effects;
  using Grove.AI;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;

  public class PhyrexianGhoul : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Phyrexian Ghoul")
        .ManaCost("{2}{B}")
        .Type("Creature - Zombie")
        .Text("Sacrifice a creature: Phyrexian Ghoul gets +2/+2 until end of turn.")
        .FlavorText("Phyrexia wastes nothing. Its food chain is a spiraling cycle.")
        .Power(2)
        .Toughness(2)
        .ActivatedAbility(p =>
          {
            p.Text = "Sacrifice a creature: Phyrexian Ghoul gets +2/+2 until end of turn.";
            p.Cost = new Sacrifice();
            p.Effect = () => new ApplyModifiersToSelf(() => new AddPowerAndToughness(2, 2) {UntilEot = true})
              .SetTags(EffectTag.IncreasePower, EffectTag.IncreaseToughness);

            p.TargetSelector.AddCost(trg => trg.Is.Creature(ControlledBy.SpellOwner).On.Battlefield());

            p.TimingRule(new PumpOwningCardTimingRule(2, 2));
            p.TargetingRule(new EffectOrCostRankBy(c => c.Score) {TargetLimit = 1, ConsiderTargetingSelf = false} );
          });
    }
  }
}