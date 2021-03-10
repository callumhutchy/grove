﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI;
  using AI.TargetingRules;
  using AI.TimingRules;
  using Effects;
  using Triggers;

  public class ManOWar : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Man-o'-War")
        .ManaCost("{2}{U}")
        .Type("Creature Jellyfish")
        .Text("When Man-o'-War enters the battlefield, return target creature to its owner's hand.")
        .FlavorText("Beauty to the eye does not always translate to the touch.")
        .OverrideScore(p => p.Battlefield = Scores.ManaCostToScore[2])
        .Power(2)
        .Toughness(2)
        .Cast(p =>
          {
            p.TimingRule(new WhenOpponentControllsPermanents(
              card => card.Is().Creature && card.CanBeTargetBySpellsWithColor(CardColor.Blue)));
            p.TimingRule(new OnFirstMain());
          })
        .TriggeredAbility(p =>
          {
            p.Text = "When Man-o'-War enters the battlefield, return target creature to its owner's hand.";
            p.Trigger(new OnZoneChanged(to: Zone.Battlefield));
            p.Effect = () => new ReturnToHand();
            p.TargetSelector.AddEffect(trg => trg.Is.Creature().On.Battlefield());
            p.TargetingRule(new EffectBounce());
          });
    }
  }
}