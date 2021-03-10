﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Costs;
  using Grove.Effects;
  using Grove.AI;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;
  using Grove.Triggers;

  public class SwordOfFeastAndFamine : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Sword of Feast and Famine")
        .ManaCost("{3}")
        .Type("Artifact - Equipment")
        .Text(
          "Equipped creature gets +2/+2 and has protection from black and from green.{EOL}Whenever equipped creature deals combat damage to a player, that player discards a card and you untap all lands you control.{EOL}{Equip} {2}")
        .Cast(p => p.TimingRule(new OnFirstMain()))
        .TriggeredAbility(p =>
          {
            p.Text =
              "Whenever equipped creature deals combat damage to a player, that player discards a card and you untap all lands you control.";

            p.Trigger(new OnDamageDealt(dmg =>
              dmg.IsCombat &&
                dmg.IsDealtByEnchantedCreature &&
                dmg.IsDealtToPlayer));      

            p.Effect = () => new CompoundEffect(
              new OpponentDiscardsCards(selectedCount: 1),
              new UntapAllLands());
          })
        .ActivatedAbility(p =>
          {
            p.Text = "{2}: Attach to target creature you control. Equip only as a sorcery.";
            p.Cost = new PayMana(2.Colorless());
            p.Effect = () => new Attach(
              () => new AddPowerAndToughness(2, 2),
              () => new AddProtectionFromColors(L(CardColor.Black, CardColor.Green)))
              .SetTags(EffectTag.IncreasePower, EffectTag.IncreaseToughness, EffectTag.Protection);                            

            p.TargetSelector.AddEffect(trg => trg.Is.ValidEquipmentTarget().On.Battlefield());
            p.TimingRule(new OnFirstDetachedOnSecondAttached());
            p.TargetingRule(new EffectCombatEquipment());
            p.ActivateAsSorcery = true;
          });
    }
  }
}