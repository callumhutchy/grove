﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Effects;
  using Triggers;

  public class GoblinLackey : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Goblin Lackey")
        .ManaCost("{R}")
        .Type("Creature Goblin")
        .Text(
          "Whenever Goblin Lackey successfully deals damage to a player, you may choose a Goblin card in your hand and put that Goblin into play.")
        .FlavorText("All bark, someone else's bite.")
        .Power(1)
        .Toughness(1)
        .TriggeredAbility(p =>
          {
            p.Text =
              "Whenever Goblin Lackey successfully deals damage to a player, you may choose a Goblin card in your hand and put that Goblin into play.";
            p.Trigger(new OnDamageDealt(dmg => dmg.IsDealtByOwningCard && dmg.IsDealtToPlayer));
            p.Effect = () => new PutSelectedCardsToBattlefield(
              text: "Select a goblin in your hand.",
              validator: card => card.Is("goblin"),
              fromZone: Zone.Hand);
          });
    }
  }
}