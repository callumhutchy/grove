﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using AI;
  using Effects;
  using Triggers;

  public class Kingfisher : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Kingfisher")
        .ManaCost("{3}{U}")
        .Type("Creature - Bird")
        .Text("{Flying}{EOL}When Kingfisher dies, draw a card.")
        .FlavorText("It's tastiest when served with the fish it stole.")
        .OverrideScore(p => p.Battlefield = Scores.ManaCostToScore[3])
        .Power(2)
        .Toughness(2)
        .SimpleAbilities(Static.Flying)
        .TriggeredAbility(p =>
          {
            p.Text = "When Kingfisher dies, draw a card.";
            p.Trigger(new OnZoneChanged(@from: Zone.Battlefield, to: Zone.Graveyard));
            p.Effect = () => new DrawCards(1);
          });
    }
  }
}