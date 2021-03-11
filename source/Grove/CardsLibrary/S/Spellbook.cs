namespace Grove.CardsLibrary.S
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class Spellbook : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Spellbook")
        .Type("Artifact")
        .ManaCost("{0}")
        .Text("You have no maximum hand size.")
        .StaticAbility(p => p.Modifier(() => new Modifiers.NoMaximumHandSize()));
    }
  }
}
