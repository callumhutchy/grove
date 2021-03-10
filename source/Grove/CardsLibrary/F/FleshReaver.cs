﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Effects;
  using Events;
  using Triggers;

  public class FleshReaver : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Flesh Reaver")
        .ManaCost("{1}{B}")
        .Type("Creature Horror")
        .Text(
          "Whenever Flesh Reaver deals damage to a creature or opponent, Flesh Reaver deals that much damage to you.")
        .FlavorText(
          "Though the reaver is horrifyingly effective, its dorsal vents spit a highly corrosive cloud of filth.")
        .Power(4)
        .Toughness(4)
        .TriggeredAbility(p =>
          {
            p.Text =
              "Whenever Flesh Reaver deals damage to a creature or opponent, Flesh Reaver deals that much damage to you.";
            
            p.Trigger(new OnDamageDealt(dmg =>
              dmg.IsDealtByOwningCard &&
              (dmg.IsDealtToCreature || dmg.IsDealtToOpponent)));
                            
            p.Effect = () => new DealExistingDamageToPlayer(
              P(e => e.TriggerMessage<DamageDealtEvent>().Damage),
              P(e => e.Controller));
          });
    }
  }
}