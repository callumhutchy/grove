﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Cards;
  using Core.Dsl;

  public class PegasusCharger : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return Card
        .Named("Pegasus Charger")
        .ManaCost("{2}{W}")
        .Type("Creature Pegasus")
        .Text("{Flying, first strike}")
        .FlavorText(
          "'The clouds came alive and dove to the earth Hooves flashed among the dark army, who fled before the spectacle of fury.'{EOL}—Song of All, canto 211")
        .Power(2)
        .Toughness(1)
        .Timing(Timings.Creatures())
        .Abilities(
          Static.Flying,
          Static.FirstStrike
        );
    }
  }
}