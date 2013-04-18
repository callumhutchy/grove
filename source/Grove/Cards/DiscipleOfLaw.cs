﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Dsl;

  public class DiscipleOfLaw : CardsSource
  {
    public override IEnumerable<CardFactory> GetCards()
    {
      yield return Card
        .Named("Disciple of Law")
        .ManaCost("{1}{W}")
        .Type("Creature - Human Cleric")
        .Text("Protection from red{EOL}Cycling {2} ({2}, Discard this card: Draw a card.)")
        .FlavorText("A religious order for religious order.")
        .Power(1)
        .Toughness(2)
        .Protections(CardColor.Red)
        .Cycling("{2}");
    }
  }
}