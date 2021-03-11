namespace Grove.CardsLibrary.S
{
  using Grove.Effects;
  using Grove.Triggers;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class SolemnSimulacrum : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Solemn Simulacrum")
        .Type("Artifact Creature - Golem")
        .ManaCost("{4}")
        .Text("When Solemn Simulacrum enter the battlefield, you may search your library for a basic land card, put that on to the battlefield tapped, then shuffle your library.{EOL}When Solemn Simulacrum dies, you may draw a card.")
        .TriggeredAbility(p =>
        {
          p.Text = "When Solemn Simulacrum enter the battlefield, you may search your library for a basic land card, put that on to the battlefield tapped, then shuffle your library.";
          p.Trigger(new OnZoneChanged(to: Zone.Battlefield));
          p.Effect = () => new SearchLibraryPutToZone(
              zone: Zone.Battlefield,
              afterPutToZone: (c, g) => c.Tap(),
              minCount: 0,
              maxCount: 1,
              validator: (c, ctx) => c.Is().BasicLand,
              text: "Search your library for a basic land card.",
              rankingAlgorithm: SearchLibraryPutToZone.ChooseLandToPutToBattlefield);
        })
        .TriggeredAbility(p =>
        {
          p.Text = "When Solemn Simulacrum dies, you may draw a card.";
          p.Trigger(new OnZoneChanged(from: Zone.Battlefield, to: Zone.Graveyard));
          p.Effect = () => new DrawCards(1);
        });
    }
  }
}
