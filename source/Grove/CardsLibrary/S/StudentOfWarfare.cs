﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.AI;

  public class StudentOfWarfare : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Student of Warfare")
        .ManaCost("{W}")
        .Type("Creature Human Knight")
        .Text(
          "{Level up} {W} ({W}: Put a level counter on this. Level up only as sorcery.){EOL}{Level 2-6:}Student of Warfare has First strike and is a 3/3 creature.{EOL}{Level 7+:}Student of Warfare has Double strike and is a 4/4 creature.")
        .Power(1)
        .Toughness(1)
        .Leveler(
          "{W}",
          EffectTag.IncreaseToughness,
          Level(min: 2, max: 6, power: 3, toughness: 3, ability: Static.FirstStrike),
          Level(min: 7, power: 4, toughness: 4, ability: Static.DoubleStrike)
        );
    }
  }
}