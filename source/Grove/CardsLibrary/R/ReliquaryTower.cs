namespace Grove.CardsLibrary.R
{
  using System.Collections.Generic;
  using Modifiers;

  public class ReliquaryTower : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Reliquary Tower")
        .Type("Land")
        .Text("You have no maximum hand size.{EOL}{T}: Add {1}.")
        .ManaAbility(p =>
         {
           p.Text = "{T}: Add {1} to your mana pool.";
           p.ManaAmount(Mana.Any);
         })
        .StaticAbility(p => p.Modifier(() => new NoMaximumHandSize()));
    }
  }
}
