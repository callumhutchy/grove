namespace Grove.Tests.Cards.D
{
  using Grove.Tests.Infrastructure;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using Xunit;

  public class Decimate
  {
    public class Ai : AiScenario
    {
      [Fact]
      public void DestroyAll4()
      {
        var decimate = C("Decimate");
        Hand(P1, decimate);
        Battlefield(P1, "Forest", "Forest", "Mountain", "Mountain","Grizzly Bears");
        Battlefield(P2, "Grizzly Bears", "Forest", "Sigil of the Empty Throne", "Spellbook");
        P2.Life = 2;
        RunGame(1);
        Equal(0, P2.Battlefield.Count());
        Equal(4, P2.Graveyard.Count());

      }
    }
  }
}
