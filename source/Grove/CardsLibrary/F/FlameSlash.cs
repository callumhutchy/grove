﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;

  public class FlameSlash : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Flame Slash")
        .ManaCost("{R}")
        .Type("Sorcery")
        .Text("Flame Slash deals 4 damage to target creature.")
        .FlavorText(
          "After millennia asleep, the Eldrazi had forgotten about Zendikar's fiery temper and dislike of strangers.")
        .Cast(p =>
          {
            p.Effect = () => new DealDamageToTargets(4);
            p.TargetSelector.AddEffect(trg => trg.Is.Creature().On.Battlefield());
            p.TargetingRule(new EffectDealDamage(4));
            p.TimingRule(new TargetRemovalTimingRule(removalTag: EffectTag.DealDamage));
          });
    }
  }
}