﻿namespace Grove.Tests.Cards
{
  using System.Linq;
  using Grove.Core;
  using Infrastructure;
  using Xunit;

  public class MarshCasualties
  {
    public class Ai : AiScenario
    {
      [Fact]
      public void PayKickerCost()
      {
        Hand(P1, "Marsh Casualties");
        Battlefield(P1, "Swamp", "Swamp", "Swamp", "Swamp", "Swamp");
        Battlefield(P2, "Grizzly Bears");

        RunGame(maxTurnCount: 2);

        Equal(1, P2.Graveyard.Count());
      }
    }

    public class Predefined : PredifinedScenario
    {
      [Fact]
      public void KickerTargetPlayerCreaturesGetM2M2()
      {
        var marsh = C("Marsh Casualties");
        var bear1 = C("Grizzly Bears");
        var bear2 = C("Grizzly Bears");

        Hand(P1, marsh);
        Battlefield(P2, bear1, bear2);

        Exec(
          At(Step.FirstMain)
            .Cast(marsh, target: P2, payKicker: true)
            .Verify(() => Equal(2, P2.Graveyard.Count())));
      }

      [Fact]
      public void TargetPlayerCreaturesGetM1M1()
      {
        var marsh = C("Marsh Casualties");
        var bear1 = C("Grizzly Bears");
        var bear2 = C("Grizzly Bears");

        Hand(P1, marsh);
        Battlefield(P2, bear1, bear2);

        Exec(
          At(Step.FirstMain)
            .Cast(marsh, target: P2)
            .Verify(() => {
              Equal(1, C(bear1).Toughness);
              Equal(1, C(bear2).Power);
            }));
      }
    }
  }
}