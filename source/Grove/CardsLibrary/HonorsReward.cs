namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using System.Linq;
  using AI;
  using AI.TargetingRules;
  using AI.TimingRules;
  using Effects;
  using Modifiers;

  public class HonorsReward : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Honor's Reward")
        .ManaCost("{2}{W}")
        .Type("Instant")
        .Text("You gain 4 life. Bolster 2.{I}(Choose a creature with the least toughness among creatures you control and put two +1/+1 counters on it.){/I}")
        .FlavorText("It seldom rains in Abzan lands. When it does, the khan marks the occasion by honoring the families of the fallen.")
        .Cast(p =>
        {
          p.Effect = () => new CompoundEffect(
            new ChangeLife(amount: 4, whos: P(e => e.Controller)),
            new Put11CountersOnTargets(2));
          
          p.TargetSelector.AddBolsterEffect();
          
          p.TargetingRule(new EffectOrCostRankBy(rank: c => -c.Score, controlledBy: ControlledBy.SpellOwner));
          
          p.TimingRule(new Any(
            new OnEndOfOpponentsTurn(),
            new WhenYourLifeCanBecomeZero()));
        });
    }
  }
}
