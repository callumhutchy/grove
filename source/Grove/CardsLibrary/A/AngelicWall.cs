﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;

  public class AngelicWall : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Angelic Wall")
        .ManaCost("{1}{W}")
        .Type("Creature Wall")
        .Text("{Defender}, {Flying}")
        .FlavorText(
          "The air stirred as if fanned by angels wings, and the enemy was turned aside.")
        .Power(0)
        .Toughness(4)
        .SimpleAbilities(Static.Defender, Static.Flying);
    }
  }
}