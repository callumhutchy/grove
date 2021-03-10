﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Costs;
  using Grove.Effects;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;

  public class WeatherseedElf : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Weatherseed Elf")
        .Type("Creature Elf")
        .ManaCost("{G}")
        .Text("{T}: Target creature gains forestwalk until end of turn.")
        .FlavorText(
          "My grandmother once told me the future of our world was inside the Weatherseed. When I touched it, I knew she was right.")
        .Power(1)
        .Toughness(1)
        .ActivatedAbility(p =>
          {
            p.Text = "{T}: Target creature gains forestwalk until end of turn.";
            p.Cost = new Tap();
            p.Effect = () => new ApplyModifiersToTargets(
              () => new AddSimpleAbility(Static.Forestwalk) {UntilEot = true});

            p.TargetSelector.AddEffect(trg => trg.Is.Creature().On.Battlefield());
            
            p.TimingRule(new BeforeYouDeclareAttackers());
            p.TargetingRule(new EffectBigWithoutEvasions());
          });
    }
  }
}