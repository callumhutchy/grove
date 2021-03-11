namespace Grove.CardsLibrary.T
{
  using System.Collections.Generic;
  using Modifiers;

  public class ThoughtVessel : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Thought Vessel")
        .Type("Artifact")
        .ManaCost("{2}")
        .Text("You have no maximum hand size.{EOL}{T}: Add {1} to your mana pool.")
        .ManaAbility(p =>
        {
          p.Text = "{T}: Add {1} to your mana pool.";
          p.ManaAmount(Mana.Any);
        })
        .StaticAbility(p => p.Modifier(() => new NoMaximumHandSize()));
        
    }
  }
}
