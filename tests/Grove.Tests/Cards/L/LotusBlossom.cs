﻿namespace Grove.Tests.Cards
{
  using Infrastructure;
  using Xunit;

  public class LotusBlossom
  {
    public class Ai : AiScenario
    {
      [Fact]
      public void PlayForce()
      {
        var force = C("Verdant Force");

        Battlefield(P1, "Forest", "Forest", "Forest", "Forest", "Lotus Blossom");
        Hand(P1, force);

        RunGame(8);

        Equal(Zone.Battlefield, C(force).Zone);
      }       
    }
  }
}