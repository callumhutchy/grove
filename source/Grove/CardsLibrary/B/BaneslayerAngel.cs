﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;

  public class BaneslayerAngel : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Baneslayer Angel")
        .ManaCost("{3}{W}{W}")
        .Type("Creature - Angel")
        .Text("{Flying}, {First strike}, {Lifelink}{EOL}Baneslayer Angel has protection from Demons and from Dragons.")
        .FlavorText("Some angels protect the meek and innocent. Others seek out and smite evil wherever it lurks.")
        .Power(5)
        .Toughness(5)
        .Protections("demon")
        .Protections("dragon")                
        .SimpleAbilities(
          Static.Flying,
          Static.FirstStrike,
          Static.Lifelink
        );
    }
  }
}