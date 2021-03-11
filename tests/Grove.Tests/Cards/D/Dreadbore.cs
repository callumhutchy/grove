namespace Grove.Tests.Cards.D
{
  using Grove.Tests.Infrastructure;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using Xunit;

  public class Dreadbore
  {
    public class Ai : AiScenario
    {
      [Fact]
      public void DestroyCreature()
      {
        Hand(P1, "Dreadbore");
        Battlefield(P1, "Swamp", "Mountain","Forest","Plains","Island","Grizzly Bears");
        Battlefield(P2, "Grizzly Bears");
        P2.Life = 2;
        RunGame(1);
        Equal(0, P2.Battlefield.Count());
        Equal(1, P2.Graveyard.Count());
      }
    }
  }
}
