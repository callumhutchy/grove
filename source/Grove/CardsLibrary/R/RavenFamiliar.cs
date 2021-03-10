﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.Triggers;

  public class RavenFamiliar : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Raven Familiar")
        .ManaCost("{2}{U}")
        .Type("Creature Bird")
        .Text(
          "{Flying}, {Echo} {2}{U}{EOL}When Raven Familiar enters the battlefield, look at the top three cards of your library. Put one of them into your hand and the rest on the bottom of your library in any order.")
        .Power(1)
        .Toughness(2)
        .Echo("{2}{U}")
        .SimpleAbilities(Static.Flying)
        .TriggeredAbility(p =>
          {
            p.Text =
              "When Raven Familiar enters the battlefield, look at the top three cards of your library. Put one of them into your hand and the rest on the bottom of your library in any order.";
            p.Trigger(new OnZoneChanged(to: Zone.Battlefield));
            p.Effect = () => new LookAtTopCardsPutPartInHandRestOnBottom(3);
          });
    }
  }
}