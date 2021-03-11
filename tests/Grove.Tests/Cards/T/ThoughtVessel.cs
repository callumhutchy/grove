namespace Grove.Tests.Cards.T
{
  using Grove.Tests.Infrastructure;
  using System.Linq;
  using Xunit;

  public class ThoughtVessel
  {
    public class Ai : AiScenario
    {
      [Fact]
      public void NoMaximumHandSize()
      {
        Hand(P1, "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears");
        Battlefield(P1, "Thought Vessel");
        RunGame(1);
        Equal(12, P1.Hand.Count);
      }

      [Fact]
      public void DestroyedAndDiscard()
      {
        Library(P1, "Grizzly Bears", "Grizzly Bears");
        Hand(P1, "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears");
        Hand(P2, "Forest", "Forest", "Naturalize");
        Battlefield(P1, "Thought Vessel");
        RunGame(5);
        Equal(7, P1.Hand.Count);
      }

      [Fact]
      public void PlayForNoMaximumHandSize()
      {
        Library(P1, "Grizzly Bears", "Grizzly Bears");
        Hand(P1,"Forest", "Thought Vessel","Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears");
        Equal(14, P1.Hand.Count());
        RunGame(1);
        Equal(7, P1.Hand.Count());
      }

      [Fact]
      public void Tap()
      {
        Hand(P1, "Grizzly Bears");
        Battlefield(P1, "Forest","Thought Vessel");
        P2.Life = 2;
        RunGame(3);
        Equal(0, P2.Life);
      }

    }
  }
}
