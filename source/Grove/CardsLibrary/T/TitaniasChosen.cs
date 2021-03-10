﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;
  using Grove.Triggers;

  public class TitaniasChosen : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Titania's Chosen")
        .ManaCost("{2}{G}")
        .Type("Creature Elf Archer")
        .Text("Whenever a player casts a green spell, put a +1/+1 counter on Titania's Chosen.")
        .FlavorText("What do a hero and an arrow have in common? In times of war are many more made.")
        .Power(1)
        .Toughness(1)
        .Cast(p => p.TimingRule(new OnFirstMain()))
        .TriggeredAbility(p =>
          {
            p.Text = "Whenever a player casts a green spell, put a +1/+1 counter on Titania's Chosen.";
            p.Trigger(new OnCastedSpell((c, ctx) => c.HasColor(CardColor.Green)));
            p.Effect = () => new ApplyModifiersToSelf(
              modifiers: () => new AddCounters(() => new PowerToughness(1, 1), 1));

            p.TriggerOnlyIfOwningCardIsInPlay = true;
          });
    }
  }
}