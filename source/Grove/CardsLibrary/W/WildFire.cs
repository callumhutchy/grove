﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;

  public class WildFire : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Wildfire")
        .ManaCost("{4}{R}{R}")
        .Type("Sorcery")
        .Text("Each player sacrifices four lands. Wildfire deals 4 damage to each creature.")
        .FlavorText("'Shiv hatched from a shell of stone around a yolk of flame.'—Viashino myth")
        .Cast(p =>
          {
            p.Effect = () => new CompoundEffect(
              new PlayersSacrificePermanents(count: 4, validator: c => c.Is().Land, text: "Select lands to sacrifice."),
              new DealDamageToCreaturesAndPlayers(amountCreature: 4));
          });
    }
  }
}