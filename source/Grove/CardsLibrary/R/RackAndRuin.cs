﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;

  public class RackAndRuin : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Rack and Ruin")
        .ManaCost("{2}{R}")
        .Type("Instant")
        .Text("Destroy two target artifacts.")
        .FlavorText(
          "My people are bound by masters centuries dead. Each artifact we destroy is another link broken in that chain.")
        .Cast(p =>
          {
            p.Effect = () => new DestroyTargetPermanents();
            p.TargetSelector.AddEffect(
              trg => trg.Is.Card(c => c.Is().Artifact).On.Battlefield(),
              trg => {                
                trg.MinCount = 2;
                trg.MaxCount = 2;
              });

            p.TargetingRule(new EffectDestroy());
            p.TimingRule(new TargetRemovalTimingRule(EffectTag.Destroy));
          });
    }
  }
}