namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using System.Linq;
  using AI.TargetingRules;
  using Effects;
  using Triggers;

  public class DragonscaleGeneral : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Dragonscale General")
        .ManaCost("{3}{W}")
        .Type("Creature - Human Warrior")
        .Text("At the beginning of your end step, bolster X, where X is the number of tapped creatures you control.{I}(Choose a creature with the least toughness among creatures you control and put X +1/+1 counters on it.){/I}")
        .FlavorText("\"Dragons seek war. I bring it to them.\"")
        .Power(2)
        .Toughness(3)
        .TriggeredAbility(p =>
        {
          p.Text = "At the beginning of your end step, bolster X, where X is the number of tapped creatures you control.";
          p.Trigger(new OnStepStart(Step.EndOfTurn)
          {
            Condition = ctx =>
            {
              return ctx.You.Battlefield.Creatures.Count(x => x.IsTapped) > 0;
            }
          });
          p.Effect = () => new Put11CountersOnTargets(P(e => e.Controller.Battlefield.Creatures.Count(x => x.IsTapped)));
          p.TargetSelector.AddBolsterEffect();

          p.TargetingRule(new EffectOrCostRankBy(rank: c => -c.Score, controlledBy: ControlledBy.SpellOwner));
          p.TriggerOnlyIfOwningCardIsInPlay = true;
        });
    }
  }
}
