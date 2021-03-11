namespace Grove.Tests.Cards
{
  using Infrastructure;
  using Xunit;
  using System.Linq;

  public class GarrukApexPredator
  {
    public class Ai : AiScenario
    {

      [Fact]
      public void Plus1()
      {
        var garruk = C("Garruk, Apex Predator");
        Battlefield(P1, garruk.AddCounters(1, CounterType.Loyalty));
        Battlefield(P2, "Grizzly Bears");

        RunGame(3);

        Equal(3, C(garruk).Loyalty);
        Equal(2, P1.Battlefield.Creatures.Count());
      }

      [Fact]
      public void Minus3()
      {
        var garruk = C("Garruk, Apex Predator");
        Battlefield(P1, garruk.AddCounters(4, CounterType.Loyalty));
        Battlefield(P2, "Grizzly Bears");
        P1.Life = 1;
        RunGame(2);

        Equal(1, C(garruk).Loyalty);
        Equal(3, P1.Life);
      }


      [Fact]
      public void Minus8Ultimate ()
      {
        var garruk = C("Garruk, Apex Predator");
        var bear1 = C("Grizzly Bears");
        var bear2 = C("Grizzly Bears");

        Battlefield(P1, garruk.AddCounters(9, CounterType.Loyalty), bear1, bear2);

        Battlefield(P2, "Grizzly Bears", "Grizzly Bears");

        P2.Life = 10;
        
        RunGame(1);
        
        Equal(1, C(garruk).Loyalty);
        Equal(7, C(bear1).Power);
        Equal(7, C(bear2).Toughness);
        Equal(0, P2.Life);
      }
    }
  }
}