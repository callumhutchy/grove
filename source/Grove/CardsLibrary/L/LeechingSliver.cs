﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Effects;
  using Triggers;

  public class LeechingSliver : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Leeching Sliver")
        .ManaCost("{1}{B}")
        .Type("Creature — Sliver")
        .Text("Whenever a Sliver you control attacks, defending player loses 1 life.")
        .FlavorText("\"Seeing one slowly devour one of my party begged the question—do they know what cruelty is?\"{EOL}—Hastric, Thunian scout")
        .Power(1)
        .Toughness(1)
        .TriggeredAbility(p =>
        {
          p.Text = "Whenever a Sliver you control attacks, defending player loses 1 life.";

          p.Trigger(new WhenACreatureAttacks(t => t.Opponent && t.AttackerHas(c => c.Is("sliver"))));                    
          p.TriggerOnlyIfOwningCardIsInPlay = true;
          p.Effect = () => new ChangeLife(-1, P(e => e.Controller.Opponent));
        });
    }
  }
}
