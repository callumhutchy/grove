﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Artifical;
  using Artifical.TargetingRules;
  using Artifical.TimingRules;
  using Gameplay;
  using Gameplay.Characteristics;
  using Gameplay.Costs;
  using Gameplay.Effects;
  using Gameplay.ManaHandling;
  using Gameplay.Misc;
  using Gameplay.Modifiers;
  using Gameplay.Triggers;

  public class SwordOfBodyAndMind : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Sword of Body and Mind")
        .ManaCost("{3}")
        .Type("Artifact - Equipment")
        .Text(
          "Equipped creature gets +2/+2 and has protection from green and from blue.{EOL}Whenever equipped creature deals combat damage to a player, you put a 2/2 green Wolf creature token onto the battlefield and that player puts the top ten cards of his or her library into his or her graveyard.{EOL}{Equip} {2}")
        .Cast(p => p.TimingRule(new FirstMain()))
        .TriggeredAbility(p =>
          {
            p.Text =
              "Whenever equipped creature deals combat damage to a player, you put a 2/2 green Wolf creature token onto the battlefield and that player puts the top ten cards of his or her library into his or her graveyard.";

            p.Trigger(new OnDamageDealt(
              combatOnly: true,
              useAttachedToAsTriggerSource: true,
              playerFilter: delegate { return true; }));

            p.Effect = () => new CompoundEffect(
              new OpponentPutsTopCardsFromLibraryToGraveyard(10),
              new CreateTokens(
                count: 1,
                token: Card
                  .Named("Wolf Token")
                  .FlavorText(
                    "No matter where we cat warriors go in the world, those stupid slobberers find us.")
                  .Power(2)
                  .Toughness(2)
                  .Type("Creature Token Wolf")
                  .Colors(CardColor.Green)));
          })
        .ActivatedAbility(p =>
          {
            p.Text = "{2}: Attach to target creature you control. Equip only as a sorcery.";
            p.Cost = new PayMana(2.Colorless(), ManaUsage.Abilities);
            p.Effect = () => new Attach(
              () => new AddPowerAndToughness(2, 2),
              () => new AddProtectionFromColors(L(CardColor.Green, CardColor.Blue)))
              {Category = EffectCategories.ToughnessIncrease | EffectCategories.Protector};

            p.TargetSelector.AddEffect(trg => trg.Is.ValidEquipmentTarget().On.Battlefield());
            p.ActivateAsSorcery = true;
            p.TimingRule(new AttachEquipment());
            p.TargetingRule(new CombatEquipment());
          });
    }
  }
}