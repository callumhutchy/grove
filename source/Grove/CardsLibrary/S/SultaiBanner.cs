﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Costs;
  using Effects;
  using Grove.AI;
  using Grove.AI.TimingRules;

  public class SultaiBanner : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Sultai Banner")
        .ManaCost("{3}")
        .Type("Artifact")
        .Text("{T}: Add {B}, {G}, or {U} to your mana pool.{EOL}{B}{G}{U}, {T}, Sacrifice Sultai Banner: Draw a card.")
        .FlavorText("Power to dominate, cruelty to rule.")
        .ManaAbility(p =>
        {
          p.Text = "{T}: Add {B}, {G}, or {U} to your mana pool.";
          p.ManaAmount(Mana.Colored(isBlack: true, isGreen: true, isBlue: true));
        })
        .ActivatedAbility(p =>
        {
          p.Text = "{B}{G}{U}, {T}, Sacrifice Sultai Banner: Draw a card.";
          
          p.Cost = new AggregateCost(
            new PayMana("{B}{G}{U}".Parse()),
            new Tap(),
            new Sacrifice());
          
          p.Effect = () => new DrawCards(1);
          
          p.TimingRule(new Any(
              new WhenOwningCardWillBeDestroyed(),
              new OnEndOfOpponentsTurn()));
        })
        // TODO scoring should depend on number of lands on battlefield
        .OverrideScore(p => p.Battlefield = Scores.ManaCostToScore[2]);
      
    }
  }
}
