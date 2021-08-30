namespace Grove.Tests.Cards.T
{
  using Grove.Effects;
  using Grove.Triggers;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using Xunit;
  using Infrastructure;

  public class ThunderbreakRegent
  {
    public class Ai : AiScenario
    {
      [Fact]
      public void PingOpponent()
      {
        var regent = C("Thunderbreak Regent");
        var plummet = C("Plummet");
        Hand(P1, plummet);
        Battlefield(P1, "Forest", "Forest");
        Battlefield(P2, regent);
        P1.Life = 4;

        RunGame(maxTurnCount: 2);

        Equal(1, P1.Life);
      }
    }
  }
}
