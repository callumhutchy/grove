namespace Grove.Tests.Cards
{
  using Infrastructure;
  using Xunit;
  using System.Linq;


  class SigilOfTheEmptyThrone
  {
    public class Ai : AiScenario
    {

      [Fact]
      public void Spawn1Angel()
      {
        Hand(P1, "Sigil of the Empty Throne");
        Battlefield(P1, "Sigil of the Empty Throne", "Plains", "Plains","Plains","Plains","Plains");

        RunGame(1);

        Equals(1, P1.Battlefield.Creatures.Count());
      }
    }
  }
}
