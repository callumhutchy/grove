﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI.TimingRules;
  using Grove.Triggers;

  public class Waylay : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Waylay")
        .ManaCost("{2}{W}")
        .Type("Instant")
        .Text(
          "Put three Knight tokens into play. Treat these tokens as 2/2 white creatures. Exile them at end of turn.")
        .FlavorText("'You reek of corruption,' spat the knight. 'Why are you even here?'")
        .Cast(p =>
          {
            p.Effect = () => new CreateTokens(
              count: 3,
              token: Card
                .Named("Knight")
                .FlavorText("'You reek of corruption,' spat the knight. 'Why are you even here?'")
                .Power(2)
                .Toughness(2)
                .OverrideScore(p1 => p1.Battlefield = 20)
                .Type("Token Creature - Knight")
                .Colors(CardColor.White)
                .TriggeredAbility(tp =>
                  {
                    tp.Text = "Exile this at the end of turn.";
                    tp.Trigger(new OnStepStart(
                      step: Step.EndOfTurn,
                      passiveTurn: true,
                      activeTurn: true));
                    tp.Effect = () => new ExileOwner();
                    tp.TriggerOnlyIfOwningCardIsInPlay = true;
                  })
              );

            p.TimingRule(new WhenStackIsEmpty());

            p.TimingRule(new Any(
              new OnEndOfOpponentsTurn(),
              new AfterOpponentDeclaresAttackers()));
          });
    }
  }
}