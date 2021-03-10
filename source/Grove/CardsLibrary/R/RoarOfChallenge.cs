﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using System.Linq;
  using AI;
  using AI.TargetingRules;
  using AI.TimingRules;
  using Effects;
  using Modifiers;

  public class RoarOfChallenge : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Roar of Challenge")
        .ManaCost("{2}{G}")
        .Type("Sorcery")
        .Text(
          "All creatures able to block target creature this turn do so.{EOL}{I}Ferocious{/I} — That creature gains indestructible until end of turn if you control a creature with power 4 or greater.")
        .Cast(p =>
        {
          p.Effect = () => new FerociousEffect(
            L(new ApplyModifiersToTargets(
                () => new AddSimpleAbility(Static.Lure) { UntilEot = true })),
            L(new ApplyModifiersToTargets(
                () => new AddSimpleAbility(Static.Indestructible) { UntilEot = true })
              .SetTags(EffectTag.Indestructible)));

          p.TargetSelector.AddEffect(trg => trg.Is.Creature().On.Battlefield());

          p.TargetingRule(new EffectBigWithoutEvasions());
          p.TimingRule(new OnFirstMain());
        });
    }
  }
}
