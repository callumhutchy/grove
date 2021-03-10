﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using System.Linq;
  using Costs;
  using Effects;

  public class SoulOfRavnica : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Soul of Ravnica")
        .ManaCost("{4}{U}{U}")
        .Type("Creature — Avatar")
        .Text("{Flying}{EOL}{5}{U}{U}: Draw a card for each color among permanents you control.{EOL}{5}{U}{U}, Exile Soul of Ravnica from your graveyard: Draw a card for each color among permanents you control.")
        .Power(6)
        .Toughness(6)
        .SimpleAbilities(Static.Flying)
        .ActivatedAbility(p =>
        {
          p.Text = "{5}{U}{U}: Draw a card for each color among permanents you control.";
          p.Cost = new PayMana("{5}{U}{U}".Parse());

          p.Effect = () => new DrawCards(
            P(e => e.Controller.Battlefield.PermanentsColors.Count()));
        })
        .ActivatedAbility(p =>
        {
          p.Text = "{5}{U}{U}, Exile Soul of Ravnica from your graveyard: Draw a card for each color among permanents you control.";
          
          p.Cost = new AggregateCost(
            new PayMana("{5}{U}{U}".Parse()),
            new ExileOwnerCost());

          p.ActivationZone = Zone.Graveyard;

          p.Effect = () => new DrawCards(P(e => e.Controller.Battlefield.PermanentsColors.Count()));
        });
    }
  }
}
