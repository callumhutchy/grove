﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI.CostRules;
  using AI.TargetingRules;
  using AI.TimingRules;
  using Effects;

  public class PowerSink : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Power Sink")
        .ManaCost("{U}").HasXInCost()
        .Type("Instant")
        .Text(
          "Counter target spell unless its controller pays X. If he or she doesn't, that player taps all lands with mana abilities he or she controls and empties his or her mana pool.")
        .Cast(p =>
          {
            p.Effect = () => new CounterTargetSpell(ep =>
              {
                ep.DoNotCounterCost = P(e => e.X.GetValueOrDefault());
                ep.TapLandsAndEmptyManaPool = true;
              });

            p.TargetSelector.AddEffect(trg => trg.Is.CounterableSpell().On.Stack());

            p.CostRule(new XIsManaAvailableToOpponentPlus1());
            p.TimingRule(new WhenTopSpellIsCounterable());
            p.TargetingRule(new EffectCounterspell());
          });
    }
  }
}