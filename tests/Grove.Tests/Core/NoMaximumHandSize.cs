namespace Grove.Tests.Core
{
  using Grove.Tests.Infrastructure;
  using System.Linq;
  using Xunit;

  public class NoMaximumHandSize
  {
    public class Ai : AiScenario
    {
      [Fact]
      public void MultipleEffects()
      {
        Hand(P1, "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears");
        Battlefield(P1, "Thought Vessel", "Spellbook");
        RunGame(1);
        Equal(12, P1.Hand.Count);
      }

      [Fact]
      public void MultipleEffectsButOneIsDestroyed()
      {
        Library(P1, "Grizzly Bears", "Grizzly Bears");
        Hand(P1, "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears");
        Battlefield(P1, "Reliquary Tower", "Spellbook");
        Hand(P2, "Forest",  "Naturalize");
        Battlefield(P2, "Forest");
        RunGame(3);
        Equal(13, P1.Hand.Count);
      }

      [Fact]
      public void MultipleEffectsBothDestroyed()
      {
        Library(P1, "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears");
        Hand(P1, "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears");
        Battlefield(P1, "Spellbook", "Spellbook");
        Hand(P2, "Forest", "Naturalize", "Naturalize");
        Battlefield(P2, "Forest");
        RunGame(5);
        Equal(7, P1.Hand.Count);
      }

      [Fact]
      public void SingleEffectAndDestroyed()
      {
        Library(P1, "Grizzly Bears", "Grizzly Bears");
        Hand(P1, "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears");
        Battlefield(P1, "Spellbook");
        Hand(P2, "Forest", "Naturalize");
        Battlefield(P2, "Forest");
        RunGame(3);
        Equal(7, P1.Hand.Count);
      }
    }
  }
}
