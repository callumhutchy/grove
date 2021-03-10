﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Effects;
  using Triggers;

  public class SecondChance : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Second Chance")
        .ManaCost("{2}{U}")
        .Type("Enchantment")
        .Text(
          "At the beginning of your upkeep, if you have 5 or less life, sacrifice Second Chance and take an extra turn after this one.")
        .FlavorText("The greatest gift is the opportunity to right one's wrongs.")
        .TriggeredAbility(p =>
          {
            p.Text =
              "At the beginning of your upkeep, if you have 5 or less life, sacrifice Second Chance and take an extra turn after this one.";

            p.Trigger(new OnStepStart(step: Step.Upkeep)
              {
                Condition = ctx => ctx.You.Life <= 5
              });

            p.Effect = () => new CompoundEffect(
              new SacrificeOwner(),
              new TakeExtraTurn());

            p.TriggerOnlyIfOwningCardIsInPlay = true;
          });
    }
  }
}