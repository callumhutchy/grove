﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;
  using Grove.Triggers;

  public class AuraFlux : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Aura Flux")
        .ManaCost("{2}{U}")
        .Type("Enchantment")
        .Text("Other enchantments have 'At the beginning of your upkeep, sacrifice this enchantment unless you pay {2}.'")
        .FlavorText("To some, the Tolarian sunrise was a blinding flash; to others, a lingering glow.")
        .Cast(p => p.TimingRule(new OnSecondMain()))        
        .ContinuousEffect(p =>
          {
            p.Modifier = () =>
              {
                var tp = new TriggeredAbility.Parameters
                  {
                    Text =
                      "At the beginning of your upkeep, sacrifice this enchantment unless you pay {2}.",
                    Effect = () => new PayManaThen(2.Colorless(), 
                      effect: new SacrificeOwner(),
                      parameters: new PayThen.Parameters()
                      {
                        ExecuteIfPaid = false,
                        Message = "Pay upkeep?",
                      })
                  };

                tp.Trigger(new OnStepStart(Step.Upkeep));

                return new AddTriggeredAbility(new TriggeredAbility(tp));
              };

            p.Selector = (card, ctx) => card.Is().Enchantment && card != ctx.Source;
          });
    }
  }
}