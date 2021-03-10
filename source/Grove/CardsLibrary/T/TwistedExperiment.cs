﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;

  public class TwistedExperiment : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Twisted Experiment")
        .ManaCost("{1}{B}")
        .Type("Enchantment - Aura")
        .Text("Enchanted creature gets +3/-1.")
        .FlavorText("Gatha showed remarkable prowess in increasing his subjects' stature. Their lifespans, however, were another matter.")
        .Cast(p =>
          {
            p.Effect = () => new Attach(
              () => new AddPowerAndToughness(3, -1));

            p.TargetSelector.AddEffect(trg => trg.Is.Creature().On.Battlefield());
            p.TimingRule(new OnFirstMain());
            p.TargetingRule(new EffectCombatEnchantment(filter: c => c.Toughness >= 2));
          });
    }
  }
}