﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI.TargetingRules;
  using AI.TimingRules;
  using Costs;
  using Effects;

  public class CropRotation : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Crop Rotation")
        .ManaCost("{G}")
        .Type("Instant")
        .Text(
          "As an additional cost to cast Crop Rotation, sacrifice a land.{EOL}Search your library for a land card and put that card onto the battlefield. Then shuffle your library.")
        .FlavorText("Hmm . . . maybe lotuses this year.")
        .Cast(p =>
          {
            p.Cost = new AggregateCost(
              new PayMana(Mana.Green),
              new Sacrifice());

            p.Effect = () => new SearchLibraryPutToZone(
              zone: Zone.Battlefield,
              minCount: 0,
              maxCount: 1,
              validator: (c, ctx) => c.Is().Land,
              text: "Search your library for a land card.");

            p.TargetSelector.AddCost(
              trg => trg.Is.Card(c => c.Is().Land, ControlledBy.SpellOwner).On.Battlefield(),
              trg => trg.Message = "Select a land to sacrifice.");

            p.TimingRule(new OnFirstMain());
            p.TargetingRule(new CostSacrificeLandToSearchLand());
          });
    }
  }
}