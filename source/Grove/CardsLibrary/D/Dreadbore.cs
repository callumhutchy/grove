namespace Grove.CardsLibrary.D
{
  using Grove.AI;
  using Grove.AI.TargetingRules;
  using Grove.AI.TimingRules;
  using Grove.Effects;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class Dreadbore : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Dreadbore")
        .ManaCost("{B}{R}")
        .Type("Sorcery")
        .Text("Destroy target creature or planeswalker.")
        .Cast(p =>
        {
          p.Effect = () => new DestroyTargetPermanents();
          p.TargetSelector.AddEffect(trg => trg
            .Is.Creature()
            .On.Battlefield());
          p.TargetingRule(new EffectDestroy());
        });
    }
  }
}
