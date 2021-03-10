namespace Grove.Tests.Cards
{
  using System.Linq;
  using Infrastructure;
  using Xunit;

  public class DragonscaleGeneral
  {
    public class Ai : AiScenario
    {
      [Fact]
      public void PutCountersOnBear()
      {
        var bear = C("Grizzly Bears");
        var dragonscaleGeneral = C("Dragonscale General");
        Battlefield(P1, dragonscaleGeneral, bear);

        RunGame(1);

        Equal(4, C(bear).Power);
      }

      [Fact]
      public void PutCountersOnSelf()
      {
        var dragonscaleGeneral = C("Dragonscale General");
        Battlefield(P1, dragonscaleGeneral);

        RunGame(1);

        Equal(3, C(dragonscaleGeneral).Power);
      }

      [Fact]
      public void PutCountersOnBoth()
      {
        var bear = C("Grizzly Bears");
        var dragonscaleGeneral = C("Dragonscale General");
        Battlefield(P1, dragonscaleGeneral, bear);

        RunGame(3);

        Equal(4, C(bear).Power);
        Equal(4, C(dragonscaleGeneral).Power);
      }
    }
  }
}
