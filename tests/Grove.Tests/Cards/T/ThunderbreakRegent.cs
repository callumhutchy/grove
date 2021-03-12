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
        Battlefield(P1, "Forest","Forest");
        Battlefield(P2, regent);
        P1.Life = 25;
        Exec(
          At(Step.FirstMain)
            .Cast(plummet, target: regent)
            .Verify(() =>Equal(22, P1.Life))
        );
      }
    }
  }
}
