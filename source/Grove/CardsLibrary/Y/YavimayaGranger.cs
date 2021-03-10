﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI;
  using Effects;
  using Triggers;

  public class YavimayaGranger : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Yavimaya Granger")
        .ManaCost("{2}{G}")
        .Type("Creature Elf")
        .Text(
          "{Echo} {2}{G}{EOL}When Yavimaya Granger enters the battlefield, you may search your library for a basic land card, put that card onto the battlefield tapped, then shuffle your library.")
        .OverrideScore(p => p.Battlefield = Scores.ManaCostToScore[2])
        .Power(2)
        .Toughness(2)
        .Echo("{2}{G}")
        .TriggeredAbility(p =>
          {
            p.Text =
              "When Yavimaya Granger enters the battlefield, you may search your library for a basic land card, put that card onto the battlefield tapped, then shuffle your library.";
            p.Trigger(new OnZoneChanged(to: Zone.Battlefield));
            p.Effect = () => new SearchLibraryPutToZone(
              zone: Zone.Battlefield,
              afterPutToZone: (c, g) => c.Tap(),
              minCount: 0,
              maxCount: 1,
              validator: (c, ctx) => c.Is().BasicLand,
              text: "Search your library for a basic land card.",
              rankingAlgorithm: SearchLibraryPutToZone.ChooseLandToPutToBattlefield);
          });
    }
  }
}