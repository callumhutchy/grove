﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Costs;
  using Grove.Effects;
  using Grove.AI.TimingRules;
  using Grove.Modifiers;

  public class SpawningPool : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Spawning Pool")
        .Type("Land")
        .Text(
          "Spawning Pool enters the battlefield tapped.{EOL}{T}: Add {B} to your mana pool.{EOL}{1}{B}: Spawning Pool becomes a 1/1 black Skeleton creature with '{B}': Regenerate this creature' until end of turn. It's still a land.")
        .Cast(p => p.Effect = () => new CastPermanent(tap: true))
        .ManaAbility(p =>
          {
            p.Text = "{T}: Add {B} to your mana pool.";
            p.ManaAmount(Mana.Black);
            p.Priority = ManaSourcePriorities.OnlyIfNecessary;
          })
        .ActivatedAbility(p =>
          {
            p.Text =
              "{1}{B}: Spawning Pool becomes a 1/1 black Skeleton creature with '{B}': Regenerate this creature' until end of turn. It's still a land.";

            p.Cost = new PayMana("{1}{B}".Parse());

            p.Effect = () => new ApplyModifiersToSelf(
              () => new ChangeToCreature(
                power: 1,
                toughness: 1,
                colors: L(CardColor.Black),
                type: t => t.Add(baseTypes: "creature", subTypes: "skeleton")) { UntilEot = true },
              () =>
                {
                  var ap = new ActivatedAbilityParameters
                    {
                      Text = "{B}: Regenerate this creature.",
                      Cost = new PayMana(Mana.Black),
                      Effect = () => new RegenerateOwner()
                    };

                  ap.TimingRule(new RegenerateSelfTimingRule());

                  return new AddActivatedAbility(new ActivatedAbility(ap)) {UntilEot = true};
                });

            p.TimingRule(new WhenStackIsEmpty());
            p.TimingRule(new WhenCardHas(c => !c.Is().Creature));
            p.TimingRule(new WhenYouHaveMana(3));
            p.TimingRule(new Any(new BeforeYouDeclareAttackers(), new AfterOpponentDeclaresAttackers()));
          });
    }
  }
}