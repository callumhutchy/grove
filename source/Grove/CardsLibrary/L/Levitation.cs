﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;

  public class Levitation : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Levitation")
        .ManaCost("{2}{U}{U}")
        .Type("Enchantment")
        .Text("Creatures you control have flying.")
        .FlavorText("Barrin's pride in his apprentice was diminished somewhat when he had to get the others back down.")
        .Cast(p =>
          {
            p.TimingRule(new OnFirstMain());
            p.TimingRule(new WhenYouDontControlSamePermanent());
          })
        .ContinuousEffect(p =>
          {
            p.Modifier = () => new AddSimpleAbility(Static.Flying);
            p.Selector = (card, ctx) => card.Controller == ctx.You && card.Is().Creature;
          });
    }
  }
}