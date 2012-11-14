﻿namespace Grove.Cards
{
  using System;
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Dsl;

  public class PouncingJaguar : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return Card
        .Named("Pouncing Jaguar")
        .ManaCost("{G}")
        .Type("Creature Cat")
        .Text(
          "{Echo} {G} (At the beginning of your upkeep, if this came under your control since the beginning of your last upkeep, sacrifice it unless you pay its echo cost.)")
        .FlavorText("One pounce, she's hungry—you die quickly. Two, she's teaching her cubs—you're in for a long day.")
        .Power(2)
        .Toughness(2)
        .Timing(Timings.Creatures())
        .Echo("{G}");
    }
  }
}