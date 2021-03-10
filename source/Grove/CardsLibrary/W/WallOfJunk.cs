﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.Triggers;

  public class WallOfJunk : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Wall of Junk")
        .Type("Artifact Creature Wall")
        .ManaCost("{2}")
        .Text(
          "{Defender}{EOL}Whenever Wall of Junk blocks, return it to its owner's hand at end of combat. (Return it only if it's on the battlefield.)")
        .FlavorText(
          "Urza saw the wall and realized that even if he tore every Phyrexian to pieces, they would still resist him.")
        .Power(0)
        .Toughness(7)
        .SimpleAbilities(Static.Defender)
        .TriggeredAbility(p =>
          {
            p.Text =
              "Whenever Wall of Junk blocks, return it to its owner's hand at end of combat. (Return it only if it's on the battlefield.)";
            p.Trigger(new OnStepStart(
              step: Step.EndOfCombat,
              passiveTurn: true,
              activeTurn: false) {Condition = ctx => ctx.OwningCard.IsBlocker});

            p.Effect = () => new ReturnToHand(returnOwningCard: true);
            p.TriggerOnlyIfOwningCardIsInPlay = true;
          });
    }
  }
}