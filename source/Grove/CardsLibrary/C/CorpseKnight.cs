namespace Grove.CardsLibrary.C
{
  using Grove.Effects;
  using Grove.Triggers;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class CorpseKnight : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Corpse Knight")
        .ManaCost("{B}{W}")
        .Type("Creature - Zombie Knight")
        .Power(2)
        .Toughness(2)
        .Text("Whenever another creature enters the battlefield under your control, each opponent loses 1 life.")
        .TriggeredAbility(p =>
        {
          p.Text = "Whenever another creature enters the battlefield under your control, each opponent loses 1 life.";
          p.Trigger(new OnZoneChanged(
            to: Zone.Battlefield,
            selector: (c, ctx) => c.Is().Creature && c.Controller == ctx.You));
          p.Effect = () => new ChangeLife(-1, P(e => e.Controller.Opponent));
          p.TriggerOnlyIfOwningCardIsInPlay = true;
        });
    }
  }
}
