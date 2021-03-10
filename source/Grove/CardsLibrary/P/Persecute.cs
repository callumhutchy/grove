﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;

  public class Persecute : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Persecute")
        .ManaCost("{2}{B}{B}")
        .Type("Sorcery")
        .Text("Choose a color. Target player reveals his or her hand and discards all cards of that color.")
        .FlavorText("My finest warrior was lost to the Phyrexians. I pray that Lady Selenia died honorably.")
        .Cast(p =>
          {
            p.Effect = () => new DiscardAllCardsOfChosenColor();
            p.TargetSelector.AddEffect(trg => trg.Is.Player());

            p.TimingRule(new OnFirstMain());
            p.TargetingRule(new EffectOpponent());
          });
    }
  }
}