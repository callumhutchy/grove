﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI;
  using AI.RepetitionRules;
  using AI.TargetingRules;
  using AI.TimingRules;
  using Costs;
  using Effects;

  public class ShivanHellkite : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Shivan Hellkite")
        .ManaCost("{5}{R}{R}")
        .Type("Creature Dragon")
        .Text("{Flying}{EOL}{1}{R}: Shivan Hellkite deals 1 damage to target creature or player.")
        .FlavorText(
          "A dragon's scale can be carved into a mighty shield, provided you can procure a dragontooth to cut it.")
        .Power(5)
        .Toughness(5)
        .SimpleAbilities(Static.Flying)
        .ActivatedAbility(p =>
          {
            p.Text = "{1}{R}: Shivan Hellkite deals 1 damage to target creature or player.";
            p.Cost = new PayMana("{1}{R}".Parse(), supportsRepetitions: true);
            p.Effect = () => new DealDamageToTargets(1);
            p.TargetSelector.AddEffect(trg => trg.Is.CreatureOrPlayer().On.Battlefield());

            p.TargetingRule(new EffectDealDamage(p1 => p1.MaxRepetitions));
            p.TimingRule(new TargetRemovalTimingRule(removalTag: EffectTag.DealDamage));
            p.RepetitionRule(new RepeatForEachLifepointTargetHasLeft());
          });
    }
  }
}