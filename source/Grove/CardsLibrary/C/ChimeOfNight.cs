﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI.TargetingRules;
  using AI.TimingRules;
  using Effects;
  using Triggers;

  public class ChimeOfNight : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Chime of Night")
        .ManaCost("{1}{B}")
        .Type("Enchantment Aura")
        .Text("When Chime of Night is put into a graveyard from the battlefield, destroy target nonblack creature.")
        .FlavorText(
          "Many sent to serve Davvol carried such instruments, as if to remind him who their true masters were.")
        .Cast(p =>
          {
            p.Effect = () => new Attach();
            p.TargetSelector.AddEffect(trg => trg.Is.Creature().On.Battlefield());
            p.TargetingRule(new EffectOrCostRankBy(c => c.Toughness.GetValueOrDefault(), ControlledBy.Opponent));
            p.TimingRule(new OnFirstMain());
          })
        .TriggeredAbility(p =>
          {
            p.Text =
              "When Chime of Night is put into a graveyard from the battlefield, destroy target nonblack creature.";
            p.Trigger(new OnZoneChanged(@from: Zone.Battlefield, to: Zone.Graveyard));

            p.Effect = () => new DestroyTargetPermanents();

            p.TargetSelector.AddEffect(trg => trg
              .Is.Card(c => c.Is().Creature && !c.HasColor(CardColor.Black))
              .On.Battlefield());

            p.TargetingRule(new EffectDestroy());
          });
    }
  }
}