﻿namespace Grove.Tests.Cards
{
  using System.Linq;
  using Grove.Core;
  using Infrastructure;
  using Xunit;

  public class HypnoticSpecter
  {
    public class Predefined : PredifinedScenario
    {
      [Fact]
      public void PlayerDiscardsCard()
      {
        var specter = C("Hypnotic Specter");

        Battlefield(P1, specter);
        Hand(P2, "Forest");

        Exec(
          At(Step.DeclareAttackers)
            .DeclareAttackers(specter),
          At(Step.SecondMain)
            .Verify(() => Equal(0, P2.Hand.Count())));
      }
    }
  }
}