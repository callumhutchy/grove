﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;

  public class Purify : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Purify")
        .ManaCost("{3}{W}{W}")
        .Type("Sorcery")
        .Text("Destroy all artifacts and enchantments.")
        .FlavorText("Our Mother The sky was Her hair; the sun, Her face. She danced on the grass and in the hills.")
        .Cast(p =>
          {
            p.Effect = () => new DestroyAllPermanents((c, ctx) => c.Is().Enchantment || c.Is().Artifact);
          });
    }
  }
}