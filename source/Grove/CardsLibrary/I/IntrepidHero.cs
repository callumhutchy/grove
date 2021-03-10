﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Costs;
  using Grove.Effects;
  using Grove.AI;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;

  public class IntrepidHero : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Intrepid Hero")
        .ManaCost("{2}{W}")
        .Type("Creature Human Soldier")
        .Text("{T} : Destroy target creature with power 4 or greater.")
        .FlavorText(
          "'We each have our own strengths, Radiant,' Serra said with a sly smile. 'If all of my people were like this one, who would carry your scrolls?'")
        .Power(1)
        .Toughness(1)
        .ActivatedAbility(p =>
          {
            p.Text = "{T} : Destroy target creature with power 4 or greater.";
            p.Cost = new Tap();
            p.Effect = () => new DestroyTargetPermanents();
            p.TargetSelector.AddEffect(trg => trg
              .Is.Card(c => c.Is().Creature && c.Power >= 4)
              .On.Battlefield());

            p.TargetingRule(new EffectDestroy());
            p.TimingRule(new TargetRemovalTimingRule().RemovalTags(EffectTag.Destroy, EffectTag.CreaturesOnly));
          });
    }
  }
}