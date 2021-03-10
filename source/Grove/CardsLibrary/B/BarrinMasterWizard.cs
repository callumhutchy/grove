﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI;
  using AI.TargetingRules;
  using AI.TimingRules;
  using Costs;

  public class BarrinMasterWizard : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Barrin, Master Wizard")
        .ManaCost("{1}{U}{U}")
        .Type("Legendary Creature Human Wizard")
        .Text("{2}, Sacrifice a permanent: Return target creature to its owner's hand.")
        .FlavorText(
          "Knowledge is no more expensive than ignorance, and at least as satisfying.")
        .Power(1)
        .Toughness(1)
        .ActivatedAbility(p =>
          {
            p.Text = "{2}, Sacrifice a permanent: Return target creature to its owner's hand.";
            p.Cost = new AggregateCost(
              new PayMana(2.Colorless()),
              new Sacrifice());
            p.Effect = () => new Effects.ReturnToHand();
            p.TargetSelector
              .AddCost(
                trg => trg.Is.Card(controlledBy: ControlledBy.SpellOwner).On.Battlefield(),
                trg => trg.Message = "Select a permanent to sacrifice.")
              .AddEffect(
                trg => trg.Is.Card(c => c.Is().Creature).On.Battlefield(),
                trg => trg.Message = "Select a creature to bounce.");

            p.TargetingRule(new CostSacrificeEffectBounce());
            p.TimingRule(new TargetRemovalTimingRule().RemovalTags(EffectTag.Bounce, EffectTag.CreaturesOnly));
          });
    }
  }
}