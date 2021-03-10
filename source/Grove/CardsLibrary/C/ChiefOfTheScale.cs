﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Modifiers;

  public class ChiefOfTheScale : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Chief of the Scale")
        .ManaCost("{W}{B}")
        .Type("Creature - Human Warrior")
        .Text("Other Warrior creatures you control get +0/+1.")
        .FlavorText("\"We are the shield unbroken. If we fall today, we will die well, and our trees will bear our names in honor.\"")
        .Power(2)
        .Toughness(3)
        .ContinuousEffect(p =>
        {
          p.Modifier = () => new AddPowerAndToughness(0, 1);
          p.Selector = (c, ctx) => c.Is("warrior") && c.Controller == ctx.You && c.Is().Creature && c != ctx.Source;
        });
    }
  }
}
