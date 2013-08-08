﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Artifical.TimingRules;
  using Gameplay.Effects;
  using Gameplay.Misc;
  using Gameplay.Zones;

  public class Exhume : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Exhume")
        .ManaCost("{1}{B}")
        .Type("Sorcery")
        .Text("Each player puts a creature card from his or her graveyard onto the battlefield.")
        .FlavorText("Death—an outmoded concept. We sleep, and we change.")
        .Cast(p =>
          {
            p.Effect = () => new EachPlayerReturnsCardFromZoneToBattlefield(Zone.Graveyard, c => c.Is().Creature);
            p.TimingRule(new ControllerGravayardCountIs(1, selector: c => c.Is().Creature));
          });
    }
  }
}