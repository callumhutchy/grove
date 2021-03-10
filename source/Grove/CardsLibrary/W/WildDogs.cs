﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Effects;
  using Triggers;

  public class WildDogs : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Wild Dogs")
        .ManaCost("{G}")
        .Type("Creature Hound")
        .Text(
          "At the beginning of your upkeep, if a player has more life than each other player, the player with the most life gains control of Wild Dogs.{EOL}Cycling {2} ({2}, Discard this card: Draw a card.)")
        .Power(2)
        .Toughness(1)
        .Cycling("{2}")
        .TriggeredAbility(p =>
          {
            p.Text =
              "At the beginning of your upkeep, if a player has more life than each other player, the player with the most life gains control of Wild Dogs.";
            p.Trigger(new OnStepStart(Step.Upkeep)
              {
                Condition = ctx => ctx.You.Life < ctx.Opponent.Life
              });
            p.Effect = () => new SwitchController();
            p.TriggerOnlyIfOwningCardIsInPlay = true;
          });
    }
  }
}