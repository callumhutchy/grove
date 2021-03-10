﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Costs;
  using Grove.Effects;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;

  public class EliteArchers : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Elite Archers")
        .ManaCost("{5}{W}")
        .Type("Creature Human Soldier Archer")
        .Text("{T}: Elite Archers deals 3 damage to target attacking or blocking creature.")
        .FlavorText("Arrows fletched with the feathers of angels seldom miss their mark.")
        .Power(3)
        .Toughness(3)
        .ActivatedAbility(p =>
          {
            p.Text = "{T}: Elite Archers deals 3 damage to target attacking or blocking creature.";
            p.Cost = new Tap();
            p.Effect = () => new DealDamageToTargets(3);
            p.TargetSelector.AddEffect(trg => trg.Is.AttackerOrBlocker().On.Battlefield());

            p.TimingRule(new OnStep(Step.DeclareBlockers));
            p.TargetingRule(new EffectDealDamage(3));            
          }
        );
    }
  }
}