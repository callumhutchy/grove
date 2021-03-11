namespace Grove.Tests.Cards.C
{
  using Grove.Tests.Infrastructure;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using Xunit;
  using System.Threading.Tasks;

  public class CorpseKnight
  {
    public class Ai : AiScenario
    {
      [Fact]
      public void SingleCopyPlayCreature()
      {
        Hand(P1, "Grizzly Bears");
        Battlefield(P1, "Forest", "Forest", "Corpse Knight");
        Battlefield(P2, "Grizzly Bears", "Grizzly Bears");
        P2.Life = 1;
        RunGame(1);
        Equal(0, P2.Life);
      }
      [Fact]
      public void TwoCopiesPlayCreature()
      {
        Hand(P1, "Grizzly Bears");
        Battlefield(P1, "Forest", "Forest", "Corpse Knight", "Corpse Knight");
        Battlefield(P2, "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears");
        P2.Life = 2;
        RunGame(1);
        Equal(0, P2.Life);
      }
    }
  }
}
