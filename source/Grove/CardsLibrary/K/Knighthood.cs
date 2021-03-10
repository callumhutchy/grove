﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;

  public class Knighthood : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Knighthood")
        .ManaCost("{2}{W}")
        .Type("Enchantment")
        .Text("Creatures you control have first strike.")
        .FlavorText("He has returned. He who brought the dark ones. He who poisoned our paradise. How shall we greet him? With swift and certain death.")
        .Cast(p =>
          {
            p.TimingRule(new OnFirstMain());
            p.TimingRule(new WhenYouDontControlSamePermanent());
          })
        .ContinuousEffect(p =>
          {
            p.Modifier = () => new AddSimpleAbility(Static.FirstStrike);
            p.Selector = (card, ctx) => card.Controller == ctx.You && card.Is().Creature;
          });
    }
  }
}