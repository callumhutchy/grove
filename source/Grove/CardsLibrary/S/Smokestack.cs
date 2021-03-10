﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI.TimingRules;
  using Effects;
  using Triggers;

  public class Smokestack : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Smokestack")
        .ManaCost("{4}")
        .Type("Artifact")
        .Text(
          "At the beginning of your upkeep, you may put a soot counter on Smokestack.{EOL}At the beginning of each player's upkeep, that player sacrifices a permanent for each soot counter on Smokestack.")
        .Cast(p => p.TimingRule(new OnSecondMain()))
        .TriggeredAbility(p =>
          {
            p.Text = "At the beginning of your upkeep, you may put a soot counter on Smokestack.";
            p.Trigger(new OnStepStart(Step.Upkeep));
            p.Effect = () => new ChooseToAddCounter(CounterType.Soot, e => e.Source.OwningCard.Counters <= 3)
              {
                TriggerOrderRule = TriggerOrder.High - 1 // ensure it triggers before sac, so it is resolved later
              };

            p.TriggerOnlyIfOwningCardIsInPlay = true;
          })
        .TriggeredAbility(p =>
          {
            p.Text =
              "At the beginning of each player's upkeep, that player sacrifices a permanent for each soot counter on Smokestack.";

            p.Trigger(new OnStepStart(Step.Upkeep, activeTurn: true, passiveTurn: true));

            p.Effect = () => new PlayersSacrificePermanents(
              count: P(e => e.Source.OwningCard.CountersCount(), EvaluateAt.OnResolve),
              text: "Sacrifice a permanent for each soot counter on Smokestack.",
              playerFilter: (_, player) => player.IsActive)
              {
                TriggerOrderRule = TriggerOrder.High
              };

            p.TriggerOnlyIfOwningCardIsInPlay = true;
          });
    }
  }
}