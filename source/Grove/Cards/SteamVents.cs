﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Dsl;
  using Core.Effects;
  using Core.Mana;
  using Core.Triggers;
  using Core.Zones;

  public class SteamVents : CardsSource
  {
    public override IEnumerable<CardFactory> GetCards()
    {
      yield return Card
        .Named("Steam Vents")
        .Type("Land - Island Mountain")
        .Text(
          "{T}: Add {U} or {R} to your mana pool.{EOL}As Steam Vents enters the battlefield, you may pay 2 life. If you don't, Steam Vents enters the battlefield tapped.")
        .TriggeredAbility(p =>
          {
            p.Trigger(new OnZoneChanged(to: Zone.Battlefield));
            p.Effect = () => new PayLifeOrTap(2);
            p.UsesStack = false;
          })
        .ManaAbility(p =>
          {
            p.Text = "{T}: Add {U} or {R} to your mana pool.";
            p.ManaAmount(new ManaUnit(ManaColors.Blue | ManaColors.Red));
          });
    }
  }
}