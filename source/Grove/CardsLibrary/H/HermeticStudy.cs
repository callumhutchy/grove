﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Costs;
  using Grove.Effects;
  using Grove.AI;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;

  public class HermeticStudy : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Hermetic Study")
        .ManaCost("{1}{U}")
        .Type("Enchantment Aura")
        .Text(
          "Enchanted creature has '{T}: This creature deals 1 damage to target creature or player.'")
        .FlavorText("Books can be replaced; a prize student cannot. Be patient.")
        .Cast(p =>
          {
            p.Effect = () => new Attach(() =>
              {
                var ap = new ActivatedAbilityParameters
                  {
                    Text = "{T}: This creature deals 1 damage to target creature or player.",
                    Cost = new Tap(),
                    Effect = () => new DealDamageToTargets(1),
                  };

                ap.TargetSelector.AddEffect(trg => trg.Is.CreatureOrPlayer().On.Battlefield());
                ap.TargetingRule(new EffectDealDamage(1));
                ap.TimingRule(new TargetRemovalTimingRule(removalTag: EffectTag.DealDamage));

                return new AddActivatedAbility(new ActivatedAbility(ap));
              });

            p.TargetSelector.AddEffect(trg => trg.Is.Creature().On.Battlefield());
            p.TimingRule(new OnSecondMain());
            p.TargetingRule(new EffectOrCostRankBy(c => c.Score, ControlledBy.SpellOwner));
          });
    }
  }
}