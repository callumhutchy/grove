﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;

  public class GraniteGrip : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Granite Grip")
        .ManaCost("{2}{R}")
        .Type("Enchantment - Aura")
        .Text("Enchanted creature gets +1/+0 for each Mountain you control.")
        .FlavorText("There's beauty in the desert—but it's best to view it from afar.")
        .Cast(p =>
          {
            p.Effect = () => new Attach(() =>
              new ModifyPowerToughnessForEachPermanent(
                power: 1,
                toughness: 0,
                filter: (c, _) => c.Is("mountain"),
                modifier: () => new IntegerIncrement()));

            p.TargetSelector.AddEffect(trg => trg.Is.Creature().On.Battlefield());
            p.TimingRule(new OnFirstMain());
            p.TargetingRule(new EffectCombatEnchantment());
          });
    }
  }
}