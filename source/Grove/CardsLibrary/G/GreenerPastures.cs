﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using System.Linq;
  using Grove.Effects;
  using Grove.AI.TimingRules;
  using Grove.Triggers;

  public class GreenerPastures : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Greener Pastures")
        .ManaCost("{2}{G}")
        .Type("Enchantment")
        .Text(
          "At the beginning of each player's upkeep, if that player controls more lands than each other player, the player puts a 1/1 green Saproling creature token onto the battlefield.")
        .Cast(p =>
          {
            p.TimingRule(new OnSecondMain());
            p.TimingRule(new WhenYouHaveMorePermanents(c => c.Is().Land));
          })
        .TriggeredAbility(p =>
          {
            p.Text =
              "At the beginning of each player's upkeep, if that player controls more lands than each other player, the player puts a 1/1 green Saproling creature token onto the battlefield.";

            p.Trigger(new OnStepStart(Step.Upkeep, activeTurn: true, passiveTurn: true)
              {
                Condition = ctx =>
                  {
                    var activeCount = ctx.Players.Active.Battlefield.Lands.Count();
                    var passiveCount = ctx.Players.Passive.Battlefield.Lands.Count();
                    return activeCount > passiveCount;
                  }
              });

            p.Effect = () => new CreateTokens(
              count: 1,
              tokenController: P((e, g) => g.Players.Active),
              token: Card
                .Named("Saproling")
                .FlavorText(
                  "The nauseating wriggling of a saproling is exceeded only by the nauseating wriggling of its prey.")
                .Power(1)
                .Toughness(1)
                .Type("Token Creature - Saproling")
                .Colors(CardColor.Green));

            p.TriggerOnlyIfOwningCardIsInPlay = true;
          });
    }
  }
}