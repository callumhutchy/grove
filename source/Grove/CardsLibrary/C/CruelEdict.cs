﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI.TimingRules;

  public class CruelEdict : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Cruel Edict")
        .ManaCost("{1}{B}")
        .Type("Sorcery")
        .Text("Target opponent sacrifices a creature.")
        .FlavorText("Choose your next words carefully. They will be your last.")
        .Cast(p =>
          {
            p.Effect = () => new PlayerSacrificePermanents(
              count: 1,
              player: P(e => e.Controller.Opponent),
              filter: c => c.Is().Creature,
              text: "Sacrifice a creature."
              );

            p.TimingRule(new NonTargetRemovalTimingRule(1));
          });
    }
  }
}