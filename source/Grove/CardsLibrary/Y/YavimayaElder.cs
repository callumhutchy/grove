﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI;
  using Grove.Costs;
  using Grove.Effects;
  using Grove.AI.TimingRules;
  using Grove.Triggers;

  public class YavimayaElder : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Yavimaya Elder")
        .ManaCost("{1}{G}{G}")
        .Type("Creature Human Druid")        
        .Text("When Yavimaya Elder dies, you may search your library for up to two basic land cards, reveal them, and put them into your hand. If you do, shuffle your library.{EOL}{2}, Sacrifice Yavimaya Elder: Draw a card.")
        .OverrideScore(p => p.Battlefield = Scores.ManaCostToScore[2])
        .Power(2)
        .Toughness(1)        
        .TriggeredAbility(p =>
          {
            p.Text = "When Yavimaya Elder dies, you may search your library for up to two basic land cards, reveal them, and put them into your hand. If you do, shuffle your library.";
            p.Trigger(new OnZoneChanged(@from: Zone.Battlefield, to: Zone.Graveyard));
            p.Effect = () => new SearchLibraryPutToZone(
              zone: Zone.Hand,
              minCount: 0,
              maxCount: 2,
              validator: (c, ctx) => c.Is().BasicLand,
              text: "Search your library for basic land cards.",
              rankingAlgorithm: SearchLibraryPutToZone.ChooseLandToPutToBattlefield);
          })
        .ActivatedAbility(p =>
          {
            p.Text = "{2}, Sacrifice Yavimaya Elder: Draw a card.";

            p.Cost = new AggregateCost(
              new PayMana(2.Colorless()),
              new Sacrifice());

            p.Effect = () => new DrawCards(1);

            p.TimingRule(new Any(
              new WhenOwningCardWillBeDestroyed(),              
              new OnEndOfOpponentsTurn()));            
          });
    }
  }
}