﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI.TargetingRules;

  public class SoulFeast : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Soul Feast")
        .ManaCost("{3}{B}{B}")
        .Type("Sorcery")
        .Text("Target player loses 4 life and you gain 4 life.")
        .FlavorText("As no one has ever accepted a second invitation to Davvol's table, the evincar often dines alone.")
        .Cast(p =>
          {
            p.Effect = () => new CompoundEffect(
              new ChangeLife(-4, P(e => e.Target.Player())),
              new ChangeLife(4, P(e => e.Controller)));

            p.TargetSelector.AddEffect(trg => trg.Is.Player());
            p.TargetingRule(new EffectOpponent());
          });
    }
  }
}