﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI;
  using AI.TargetingRules;
  using AI.TimingRules;
  using Effects;
  using Triggers;

  public class BodySnatcher : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Body Snatcher")
        .ManaCost("{2}{B}{B}")
        .Type("Creature Minion")
        .Text(
          "When Body Snatcher enters the battlefield, exile it unless you discard a creature card.{EOL}When Body Snatcher dies, exile Body Snatcher and return target creature card from your graveyard to the battlefield.")
        .OverrideScore(p => p.Battlefield = Scores.ManaCostToScore[2])
        .Power(2)
        .Toughness(2)
        .Cast(p => p.TimingRule(new WhenYourHandCountIs(minCount: 1, selector: c => c.Is().Creature)))
        .TriggeredAbility(p =>
          {
            p.Text = "When Body Snatcher enters the battlefield, exile it unless you discard a creature card.";
            p.Trigger(new OnZoneChanged(to: Zone.Battlefield));
            p.Effect = () => new ExileOwnerUnlessYouDiscardCreatureCard();
          })
        .TriggeredAbility(p =>
          {
            p.Text =
              "When Body Snatcher dies, exile Body Snatcher and return target creature card from your graveyard to the battlefield.";

            p.Trigger(new OnZoneChanged(@from: Zone.Battlefield, to: Zone.Graveyard));

            p.Effect = () => new CompoundEffect(
              new ExileOwner(),
              new PutTargetsToBattlefield());

            p.TargetSelector.AddEffect(trg => trg.Is.Creature().In.YourGraveyard());
            p.TargetingRule(new EffectOrCostRankBy(c => -c.Score));
          });
    }
  }
}