﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI;
  using Grove.AI.CostRules;
  using Grove.AI.TimingRules;

  public class FaultLine : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Fault Line")
        .ManaCost("{R}{R}").HasXInCost()
        .Type("Instant")
        .Text("Fault Line deals X damage to each creature without flying and each player.")
        .FlavorText("We live on the serpent's back.")
        .Cast(p =>
          {
            p.Effect = () => new DealDamageToCreaturesAndPlayers(
              amountPlayer: (e, player) => e.X.GetValueOrDefault(),
              amountCreature: (e, creature) => e.X.GetValueOrDefault(),
              filterCreature: (effect, card) => !card.Has().Flying);

            p.TimingRule(new MassRemovalTimingRule(removalTag: EffectTag.DealDamage));
            p.CostRule(new XIsAvailableMana());
          });
    }
  }
}