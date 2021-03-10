﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Effects;
  using Events;
  using Modifiers;
  using Triggers;

  public class WallOfFrost : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Wall of Frost")
        .ManaCost("{1}{U}{U}")
        .Type("Creature — Wall")
        .Text(
          "{Defender}{I}(This creature can't attack.){/I}{EOL}Whenever Wall of Frost blocks a creature, that creature doesn't untap during its controller's next untap step.")
        .FlavorText(
          "\"I have seen countless petty warmongers gaze on it for a time before turning away.\"{EOL}—Sarlena, paladin of the Northern Verge")
        .Power(0)
        .Toughness(7)
        .SimpleAbilities(Static.Defender)
        .TriggeredAbility(p =>
          {
            p.Text =
              "Whenever Wall of Frost blocks a creature, that creature doesn't untap during its controller's next untap step.";
            p.Trigger(new WhenThisBlocks());            

            p.Effect = () => new ApplyModifiersToCard(
              card: P(e => e.TriggerMessage<BlockerJoinedCombatEvent>().Attacker.Card),              
              modifiers: () =>
                {
                  var modifier = new AddSimpleAbility(Static.DoesNotUntap);

                  modifier.AddLifetime(new EndOfStep(
                    Step.Untap,
                    l => l.Modifier.SourceCard.Controller.IsActive));

                  return modifier;
                });
          });
    }
  }
}