﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.Triggers;

  public class HypnoticSpecter : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Hypnotic Specter")
        .ManaCost("{1}{B}{B}")
        .Type("Creature - Specter")
        .Text(
          "{Flying}{EOL}Whenever Hypnotic Specter deals damage to an opponent, that player discards a card at random.")
        .FlavorText("'Its victims are known by their eyes shattered vessels leaking broken dreams.")
        .Power(2)
        .Toughness(2)
        .SimpleAbilities(Static.Flying)
        .TriggeredAbility(p =>
          {
            p.Text = "Whenever Hypnotic Specter deals damage to an opponent, that player discards a card at random.";
            p.Trigger(new OnDamageDealt(dmg => dmg.IsDealtByOwningCard && dmg.IsDealtToOpponent));              
            p.Effect = () => new OpponentDiscardsCards(randomCount: 1);
          });
    }
  }
}