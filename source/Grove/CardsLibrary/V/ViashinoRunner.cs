﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Modifiers;

  public class ViashinoRunner : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Viashino Runner")
        .ManaCost("{3}{R}")
        .Type("Creature Viashino")
        .Text("Viashino Runner can't be blocked except by two or more creatures.")
        .FlavorText(
          "It moved this way, an' that way, an' then before I could stick it, it jumped over my head an' was gone.")
        .Power(3)
        .Toughness(2)
        .StaticAbility(p => p.Modifier(() => new SetMinBlockerCount(2)));
    }
  }
}