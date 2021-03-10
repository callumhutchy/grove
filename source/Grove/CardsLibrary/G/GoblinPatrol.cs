﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;

  public class GoblinPatrol : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Goblin Patrol")
        .ManaCost("{R}")
        .Type("Creature Goblin")
        .Text(
          "{Echo} {R} (At the beginning of your upkeep, if this came under your control since the beginning of your last upkeep, sacrifice it unless you pay its echo cost.)")
        .FlavorText("Take the sharp metal stick and make a lotta holes.")
        .Power(2)
        .Toughness(1)
        .Echo("{R}");
    }
  }
}