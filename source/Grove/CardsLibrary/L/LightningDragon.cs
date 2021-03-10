﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;

  public class LightningDragon : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Lightning Dragon")
        .ManaCost("{2}{R}{R}")
        .Type("Creature - Dragon")
        .Text(
          "{Flying};{echo} (At the beginning of your upkeep, if this came under your control since the beginning of your last upkeep, sacrifice it unless you pay its echo cost.){EOL}{R}: Lightning Dragon gets +1/+0 until end of turn.")
        .Power(4)
        .Toughness(4)
        .Echo("{2}{R}{R}")
        .SimpleAbilities(Static.Flying)
        .Pump(
          cost: Mana.Red,
          text: "{R}: Lightning Dragon gets +1/+0 until end of turn.",
          powerIncrease: 1,
          toughnessIncrease: 0);
    }
  }
}