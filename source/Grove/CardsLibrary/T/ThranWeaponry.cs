﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Costs;
  using Grove.Effects;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;

  public class ThranWeaponry : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Thran Weaponry")
        .ManaCost("{4}")
        .Type("Artifact")
        .Text(
          "{Echo} {4}{EOL}You may choose not to untap Thran Weaponry during your untap step.{EOL}{2},{T}: All creatures get +2/+2 for as long as Thran Weaponry remains tapped.")
        .MayChooseToUntap()
        .Echo("{4}")
        .ActivatedAbility(p =>
          {
            p.Text = "{2},{T}: All creatures get +2/+2 for as long as Thran Weaponry remains tapped.";
            p.Cost = new AggregateCost(
              new PayMana(2.Colorless()),
              new Tap());

            p.Effect = () => new ApplyModifiersToPlayer(
              selector: e => e.Controller,
              modifiers: () =>
                {
                  var cp = new ContinuousEffectParameters
                  {
                    Modifier = () => new AddPowerAndToughness(2, 2),
                    Selector = (card, _) => card.Is().Creature
                  };
                  
                  var effect = new ContinuousEffect(cp);

                  var modifier = new AddContiniousEffect(effect);
                  modifier.AddLifetime(new ModifierSourceGetsUntapedLifetime());
                  
                  return modifier;
                });
            
            p.TimingRule(new OnYourTurn(Step.DeclareBlockers));
          });
    }
  }
}