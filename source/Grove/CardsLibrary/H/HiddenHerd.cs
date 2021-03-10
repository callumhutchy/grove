﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;
  using Grove.Triggers;

  public class HiddenHerd : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Hidden Herd")
        .ManaCost("{G}")
        .Type("Enchantment")
        .Text(
          "When an opponent plays a nonbasic land, if Hidden Herd is an enchantment, Hidden Herd becomes a 3/3 Beast creature.")
        .Cast(p => p.TimingRule(new OnSecondMain()))
        .TriggeredAbility(p =>
          {
            p.Text =
              "When an opponent plays a nonbasic land, if Hidden Herd is an enchantment, Hidden Herd becomes a 3/3 Beast creature.";

            p.Trigger(new OnLandPlayed(
              filter: (ability, card) =>
                ability.OwningCard.Controller != card.Controller && ability.OwningCard.Is().Enchantment &&
                  card.Is().NonBasicLand));

            p.Effect = () => new ApplyModifiersToSelf(() => new ChangeToCreature(
              power: 3,
              toughness: 3,
              type: t => t.Change(baseTypes: "creature", subTypes: "beast"),
              colors: L(CardColor.Green)
              ));

            p.TriggerOnlyIfOwningCardIsInPlay = true;
          }
        );
    }
  }
}