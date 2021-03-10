﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI.TargetingRules;
  using AI.TimingRules;
  using Costs;
  using Effects;

  public class ClawsOfGix : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Claws of Gix")
        .ManaCost("{0}")
        .Type("Artifact")
        .Text("{1}, Sacrifice a permanent: You gain 1 life.")
        .FlavorText(
          "When the Brotherhood of Gix dug out the cave of Koilos they found their master's severed hand. They enshrined it, hoping that one day it would point the way to Phyrexia.")
        .Cast(p =>
          {
            p.TimingRule(new OnFirstMain());
            p.TimingRule(new WhenYouDontControlSamePermanent());
          })
        .ActivatedAbility(p =>
          {
            p.Text = "{1}, Sacrifice a permanent: You gain 1 life.";
            p.Cost = new AggregateCost(
              new PayMana(1.Colorless()),
              new Sacrifice());
            p.Effect = () => new ChangeLife(amount: 1, whos: P(e => e.Controller));
            p.TargetSelector.AddCost(
              trg => trg.Is.Card(controlledBy: ControlledBy.SpellOwner).On.Battlefield(),
              trg => trg.Message = "Select a permanent to sacrifice.");
            p.TargetingRule(new CostSacrificeToGainLife());
          }
        );
    }
  }
}