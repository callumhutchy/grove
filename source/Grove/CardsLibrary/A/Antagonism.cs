﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI.TimingRules;
  using Effects;
  using Triggers;

  public class Antagonism : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Antagonism")
        .ManaCost("{3}{R}")
        .Type("Enchantment")
        .Text(
          "At the beginning of each player's end step, Antagonism deals 2 damage to that player unless one of his or her opponents was dealt damage this turn.")
        .Cast(p => p.TimingRule(new OnSecondMain()))
        .TriggeredAbility(p =>
          {
            p.Text =
              "At the beginning of each player's end step, Antagonism deals 2 damage to that player unless one of his or her opponents was dealt damage this turn.";

            p.Trigger(new OnStepStart(Step.EndOfTurn, activeTurn: true, passiveTurn: true)
              {
                Condition = ctx => ctx.Turn.Events.HasBeenDamaged(ctx.Opponent) == false
              });

            p.Effect = () => new DealDamageToPlayer(
              amount: 2,
              player: P((e, g) => g.Players.Active));

            p.TriggerOnlyIfOwningCardIsInPlay = true;
          });
    }
  }
}