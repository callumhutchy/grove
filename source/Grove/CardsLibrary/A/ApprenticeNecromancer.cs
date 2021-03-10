﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI.TimingRules;
  using Costs;
  using Effects;
  using Modifiers;
  using Triggers;

  public class ApprenticeNecromancer : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Apprentice Necromancer")
        .ManaCost("{1}{B}")
        .Type("Creature Zombie Wizard")
        .Text(
          "{B},{T}, Sacrifice Apprentice Necromancer: Return target creature card from your graveyard to the battlefield. That creature gains haste. At the beginning of the next end step, sacrifice it.")
        .Power(1)
        .Toughness(1)
        .ActivatedAbility(p =>
          {
            p.Text =
              "{B},{T},Sacrifice Apprentice Necromancer: Return target creature card from your graveyard to the battlefield. That creature gains haste. At the beginning of the next end step, sacrifice it.";

            p.Cost = new AggregateCost(
              new PayMana(Mana.Black),
              new Tap(),
              new Sacrifice());

            p.Effect = () => new PutSelectedCardsToBattlefield(
              fromZone: Zone.Graveyard,
              text: "Select a creature card in your graveyard.",
              validator: c => c.Is().Creature,              
              modifiers: L( 
              () => new AddSimpleAbility(Static.Haste) {UntilEot = true},
              () =>
                {
                  var tp = new TriggeredAbility.Parameters
                    {
                      Text = "Sacrifice the creature at the beginning of the next end step.",
                      Effect = () => new SacrificeOwner(),
                    };

                  tp.Trigger(new OnStepStart(
                    step: Step.EndOfTurn,
                    passiveTurn: true,
                    activeTurn: true));

                  tp.UsesStack = false;
                  return new AddTriggeredAbility(new TriggeredAbility(tp));
                }));

            p.TimingRule(new OnYourTurn(Step.BeginningOfCombat));
            p.TimingRule(new WhenYourGraveyardCountIs(minCount: 1, selector: c => c.Is().Creature));
          });
    }
  }
}