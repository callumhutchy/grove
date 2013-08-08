﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Artifical.TargetingRules;
  using Artifical.TimingRules;
  using Gameplay;
  using Gameplay.Characteristics;
  using Gameplay.Costs;
  using Gameplay.Effects;
  using Gameplay.ManaHandling;
  using Gameplay.Misc;

  public class RuneOfProtectionGreen : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Rune of Protection: Green")
        .ManaCost("{1}{W}")
        .Type("Enchantment")
        .Text(
          "{W}: The next time a green source of your choice would deal damage to you this turn, prevent that damage.{EOL}Cycling {2} ({2}, Discard this card: Draw a card.)")
        .Cast(p => p.TimingRule(new FirstMain()))
        .Cycling("{2}")
        .ActivatedAbility(p =>
          {
            p.Text =
              "{W}: The next time a green source of your choice would deal damage to you this turn, prevent that damage.";
            p.Cost = new PayMana(Mana.White, ManaUsage.Abilities);
            p.Effect = () => new Gameplay.Effects.PreventDamageFromSourceToController();

            p.TargetSelector.AddEffect(trg =>
              {
                trg.Is.Card(c => c.HasColor(CardColor.Green)).On.BattlefieldOrStack();
                trg.Message = "Select damage source.";
              });

            p.TargetingRule(new Artifical.TargetingRules.PreventDamageFromSourceToController());
          });
    }
  }
}