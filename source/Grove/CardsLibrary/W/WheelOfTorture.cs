﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI.TimingRules;
  using Grove.Triggers;

  public class WheelOfTorture : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Wheel of Torture")
        .ManaCost("{3}")
        .Type("Artifact")
        .Text(
          "At the beginning of each opponent's upkeep, Wheel of Torture deals X damage to that player, where X is 3 minus the number of cards in his or her hand.")
        .FlavorText("I'd like to buy a bowel.")
        .Cast(p => p.TimingRule(new OnSecondMain()))
        .TriggeredAbility(p =>
          {
            p.Text =
              "At the beginning of each opponent's upkeep, Wheel of Torture deals X damage to that player, where X is 3 minus the number of cards in his or her hand.";

            p.Trigger(new OnStepStart(Step.Upkeep, activeTurn: false, passiveTurn: true));

            p.Effect = () => new DealDamageToPlayer(
              amount: P(e => 3 - e.Controller.Opponent.Hand.Count, EvaluateAt.OnResolve),
              player: P(e => e.Controller.Opponent));

            p.TriggerOnlyIfOwningCardIsInPlay = true;
          });
    }
  }
}