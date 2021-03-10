﻿namespace Grove.Tests.Cards
{
  using System.Linq;
  using Infrastructure;
  using Xunit;

  public class SatyrWayfinder
  {
    public class Ai : AiScenario
    {
      [Fact]
      public void GetAndPlayALand()
      {
        Library(P1, "Forest", "Grizzly Bears", "Grizzly Bears", "Grizzly Bears");
        Hand(P1, "Satyr Wayfinder");
        Battlefield(P1, "Forest", "Forest");

        RunGame(1);

        Equal(3, P1.Graveyard.Count);
        Equal(3, P1.Battlefield.Lands.Count());
      }
    }
  }
}