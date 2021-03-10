﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;
  using Grove.Triggers;

  public class OpalCaryatid : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Opal Caryatid")
        .ManaCost("{W}")
        .Type("Enchantment")
        .Text(
          "When an opponent casts a creature spell, if Opal Caryatid is an enchantment, Opal Caryatid becomes a 2/2 Soldier creature.")
        .Cast(p => p.TimingRule(new OnSecondMain()))
        .TriggeredAbility(p =>
          {
            p.Text =
              "When an opponent casts a creature spell, if Opal Caryatid is an enchantment, Opal Caryatid becomes a 2/2 Soldier creature.";
            p.Trigger(new OnCastedSpell((c, ctx) =>
              ctx.Opponent == c.Controller && ctx.OwningCard.Is().Enchantment && c.Is().Creature));
            p.Effect = () => new ApplyModifiersToSelf(() => new ChangeToCreature(
              power: 2,
              toughness: 2,
              type: t => t.Change(baseTypes: "creature", subTypes: "soldier"),
              colors: L(CardColor.White)));

            p.TriggerOnlyIfOwningCardIsInPlay = true;
          });
    }
  }
}