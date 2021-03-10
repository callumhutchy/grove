namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using System.Linq;
  using AI;
  using AI.TargetingRules;
  using Effects;
  using Modifiers;
  using Triggers;

  public class AbzanSkycaptain : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Abzan Skycaptain")
        .ManaCost("{3}{W}")
        .Type("Creature - Bird Soldier")
        .Text(
          "{Flying}{EOL}When Abzan Skycaptain dies, bolster 2.{I}(Choose a creature with the least toughness among creatures you control and put two +1/+1 counters on it.){/I}")
        .FlavorText("\"A tempest is coming. Turn your faces toward the wind!\"")
        .Power(2)
        .Toughness(2)
        .SimpleAbilities(Static.Flying)
        .TriggeredAbility(p =>
        {
          p.Text = "When Abzan Skycaptain dies, bolster 2.";
          p.Trigger(new OnZoneChanged(
            from:Zone.Battlefield,
            to:Zone.Graveyard));
          p.Effect = () => new Put11CountersOnTargets(2);
          p.TargetSelector.AddBolsterEffect();

          p.TargetingRule(new EffectOrCostRankBy(rank: c => -c.Score, controlledBy: ControlledBy.SpellOwner));
        });
    }
  }
}
