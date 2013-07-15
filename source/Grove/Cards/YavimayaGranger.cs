﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Gameplay.Effects;
  using Gameplay.Misc;
  using Gameplay.Triggers;
  using Gameplay.Zones;

  public class YavimayaGranger : CardsSource
  {
    public override IEnumerable<CardFactory> GetCards()
    {
      yield return Card
        .Named("Yavimaya Granger")
        .ManaCost("{2}{G}")
        .Type("Creature Elf")
        .Text(
          "{Echo} {2}{G}{EOL}When Yavimaya Granger enters the battlefield, you may search your library for a basic land card, put that card onto the battlefield tapped, then shuffle your library.")
        .Power(2)
        .Toughness(2)
        .Echo("{2}{G}")
        .TriggeredAbility(p =>
          {
            p.Text =
              "When Yavimaya Granger enters the battlefield, you may search your library for a basic land card, put that card onto the battlefield tapped, then shuffle your library.";
            p.Trigger(new OnZoneChanged(to: Zone.Battlefield));
            p.Effect = () => new SearchLibraryPutToZone(
              c =>
                {
                  c.PutToBattlefield();
                  c.Tap();
                },
              minCount: 0,
              maxCount: 1,
              validator: (e, c) => c.Is().BasicLand,
              text: "Search your library for a basic land card.");
          });
    }
  }
}