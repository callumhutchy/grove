﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI.TargetingRules;
  using Effects;
  using Modifiers;

  public class EndlessObedience : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Endless Obedience")
        .ManaCost("{4}{B}{B}")
        .Type("Sorcery")
        .Text("{Convoke} {I}(Your creatures can help cast this spell. Each creature you tap while casting this spell pays for {1} or one mana of that creature's color.){/I}{EOL}Put target creature card from a graveyard onto the battlefield under your control.")
        .FlavorText("The death of a scout can be as informative as a safe return.")
        .SimpleAbilities(Static.Convoke)
        .Cast(p =>
        {
          p.Text = "Put target creature card from a graveyard onto the battlefield under your control.";

          p.Effect = () => new CompoundEffect(
            new ApplyModifiersToTargets(
              () => new ChangeController(m => m.SourceCard.Controller)){ ShouldResolve = ctx => ctx.Opponent == ctx.Target.Controller() },
            new PutTargetsToBattlefield());

          p.TargetSelector.AddEffect(trg => trg.Is.Creature().In.Graveyard());

          p.TargetingRule(new EffectOrCostRankBy(c => -c.Score));
        });
    }
  }
}
