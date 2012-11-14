﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Cards.Costs;
  using Core.Cards.Effects;
  using Core.Dsl;
  using Core.Targeting;

  public class WizardMentor : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return Card
        .Named("Wizard Mentor")
        .ManaCost("{2}{U}")
        .Type("Creature Human Wizard")
        .Text("{T}: Return Wizard Mentor and target creature you control to their owner's hand.")
        .FlavorText(
          "Although some of the students quickly grasped the concept, the others could summon only blackboards.")
        .Power(2)
        .Toughness(2)
        .Timing(Timings.Creatures())
        .Abilities(
          ActivatedAbility(
            "{T}: Return Wizard Mentor and target creature you control to their owner's hand.",
            Cost<TapOwnerPayMana>(c => c.TapOwner = true),
            Effect<ReturnToHand>(e =>
              {
                e.ReturnTarget = true;
                e.ReturnOwner = true;
              }), 
            Validator(Validators.Creature(controller: Controller.SpellOwner)),
            selectorAi: TargetSelectorAi.BounceSelfAndTargetCreatureYouControl(),
            timing: Timings.NoRestrictions()
            )
        );
    }
  }
}