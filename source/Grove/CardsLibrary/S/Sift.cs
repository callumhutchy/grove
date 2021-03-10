﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI.TimingRules;

  public class Sift : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Sift")
        .ManaCost("{3}{U}")
        .Type("Sorcery")
        .Text("Draw three cards, then discard a card.")
        .FlavorText("Dwell longest on the thoughts that shine brightest.")
        .Cast(p =>
          {
            p.Effect = () => new DrawCards(count: 3, discardCount: 1);
            p.TimingRule(new OnFirstMain());
          });
    }
  }
}