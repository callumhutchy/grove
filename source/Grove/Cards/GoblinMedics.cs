﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Artifical.TargetingRules;
  using Gameplay.Effects;
  using Gameplay.Misc;
  using Gameplay.Triggers;

  public class GoblinMedics : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Goblin Medics")
        .ManaCost("{2}{R}")
        .Type("Creature Goblin Shaman")
        .Text("Whenever Goblin Medics becomes tapped, it deals 1 damage to target creature or player.")
        .FlavorText("First, do some harm.")
        .Power(1)
        .Toughness(1)
        .TriggeredAbility(p =>
          {
            p.Text = "Whenever Goblin Medics becomes tapped, it deals 1 damage to target creature or player.";
            p.Trigger(new OnOwnerGetsTapped());
            p.Effect = () => new DealDamageToTargets(1);
            p.TargetSelector.AddEffect(trg => trg.Is.CreatureOrPlayer().On.Battlefield());
            p.TargetingRule(new DealDamage(1));
            p.TriggerOnlyIfOwningCardIsInPlay = true;
          });
    }
  }
}