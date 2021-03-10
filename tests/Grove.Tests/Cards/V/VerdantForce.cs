﻿namespace Grove.Tests.Cards
{
  using System.Linq;
  using Infrastructure;
  using Xunit;

  public class VerdantForce
  {
    public class Predefined : PredefinedScenario
    {
      [Fact]
      public void CreateASaprolingTokenEveryUpkeep()
      {
        var force = C("Verdant Force");
        Battlefield(P1, force);

        Exec(
          At(Step.FirstMain)
            .Verify(() =>
              Equal(1, P1.Battlefield.Count(x => x.Is().Token))),
          At(Step.FirstMain, turn: 2)
            .Verify(() =>
              Equal(2, P1.Battlefield.Count(x => x.Is().Token)))
          );
      }
    }
  }
}