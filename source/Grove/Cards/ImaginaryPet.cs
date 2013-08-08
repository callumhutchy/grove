﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Gameplay.Effects;
  using Gameplay.Misc;
  using Gameplay.States;
  using Gameplay.Triggers;

  public class ImaginaryPet : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Imaginary Pet")
        .ManaCost("{1}{U}")
        .Type("Creature Illusion")
        .Text("At the beginning of your upkeep, if you have a card in hand, return Imaginary Pet to its owner's hand.")
        .FlavorText("'It followed me home. Can I keep it?'")
        .Power(4)
        .Toughness(4)
        .TriggeredAbility(p =>
          {
            p.Text =
              "At the beginning of your upkeep, if you have a card in hand, return Imaginary Pet to its owner's hand.";

            p.Trigger(new OnStepStart(Step.Upkeep) {Condition = (t, g) => t.OwningCard.Controller.Hand.Count > 0});
            p.Effect = () => new ReturnToHand(returnOwningCard: true) {ShouldResolve = e => e.Controller.Hand.Count > 0};
            p.TriggerOnlyIfOwningCardIsInPlay = true;
          }
        );
    }
  }
}