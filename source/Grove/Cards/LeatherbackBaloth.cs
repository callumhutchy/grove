﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;

  public class LeatherbackBaloth : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return C.Card
        .Named("Leatherback Baloth")
        .ManaCost("{G}{G}{G}")
        .Type("Creature - Beast")
        .FlavorText(
          "Heavy enough to withstand the Roil, leatherback skeletons are havens for travelers in storms and landshifts.")
        .Power(4)
        .Toughness(5);
    }
  }
}