﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;

  public class DriftingMeadow : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Drifting Meadow")
        .Type("Land")
        .Text(
          "Drifting Meadow enters the battlefield tapped.{EOL}{T}: Add {W} to your mana pool.{EOL}{Cycling} {2}({2}, Discard this card: Draw a card.)")
        .Cycling("{2}")
        .Cast(p => p.Effect = () => new CastPermanent(tap: true))
        .ManaAbility(p =>
          {
            p.Text = "{T}: Add {W} to your mana pool.";
            p.ManaAmount(Mana.White);
          });
    }
  }
}