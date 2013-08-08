﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Artifical.TargetingRules;
  using Gameplay.Costs;
  using Gameplay.Effects;
  using Gameplay.Misc;

  public class MotherOfRunes : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Mother of Runes")
        .ManaCost("{W}")
        .Type("Creature Human Cleric")
        .Text("{T}: Target creature you control gains protection from the color of your choice until end of turn.")
        .FlavorText("My family protects all families.")
        .Power(1)
        .Toughness(1)
        .ActivatedAbility(p =>
          {
            p.Text =
              "{T}: Target creature you control gains protection from the color of your choice until end of turn.";
            
            p.Cost = new Tap();
            p.Effect = () => new TargetGainsProtectionFromChosenColor();
            p.TargetSelector.AddEffect(trg => trg.Is.Creature(ControlledBy.SpellOwner).On.Battlefield());
                        
            p.TargetingRule(new GainProtection());
          });
    }
  }
}