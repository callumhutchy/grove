﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI.TargetingRules;
  using Costs;
  using Effects;

  public class WallOfMulch : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Wall of Mulch")
        .ManaCost("{1}{G}")
        .Type("Creature — Wall")
        .Text("{Defender}{I}(This creature can't attack.){/I}{EOL}{G}, Sacrifice a Wall: Draw a card.")
        .FlavorText(
          "Mulch is the fabric of life in the forest. Plants live in it, they die in it, and then they become part of it, feeding countless generations to come.")
        .Power(0)
        .Toughness(4)
        .SimpleAbilities(Static.Defender)
        .ActivatedAbility(p =>
          {
            p.Text = "{G}, Sacrifice a Wall: Draw a card.";

            p.Cost = new AggregateCost(
              new PayMana(Mana.Green),
              new Sacrifice());

            p.TargetSelector.AddCost(trg => trg.Is.Card(c => c.Is("wall"), ControlledBy.SpellOwner)
              .On.Battlefield());
            
            p.Effect = () => new DrawCards(1);
            p.TargetingRule(new CostSacrificeToDrawCards());
          });
    }
  }
}