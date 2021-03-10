﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Effects;
  using Triggers;

  public class SkitteringSkirge : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Skittering Skirge")
        .ManaCost("{B}{B}")
        .Type("Creature Imp")
        .Text("{Flying}{EOL}When you cast a creature spell, sacrifice Skittering Skirge.")
        .FlavorText(
          "The imps' warbling cries echo through Phyrexia's towers like those of mourning doves in a cathedral.")
        .Power(3)
        .Toughness(2)
        .SimpleAbilities(Static.Flying)
        .TriggeredAbility(p =>
          {
            p.Text = "When you cast a creature spell, sacrifice Skittering Skirge.";
            p.Trigger(new OnCastedSpell((c, ctx) =>
              ctx.You == c.Controller && c.Is().Creature));

            p.Effect = () => new SacrificeOwner();
            p.TriggerOnlyIfOwningCardIsInPlay = true;
          });
    }
  }
}