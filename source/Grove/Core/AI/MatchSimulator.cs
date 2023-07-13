namespace Grove.AI
{
  using System;
  using System.Diagnostics;
  using System.Linq;

  public static class MatchSimulator
  {
    public static SimulationResult Simulate(Deck[] decks, int maxTurnsPerGame = 100,
      int maxSearchDepth = 16, int maxTargetsCount = 2)
    {
      var stopwatch = new Stopwatch();
      stopwatch.Start();

      var result = new SimulationResult(decks.Count());

      bool haveWinner = false;

      while (!haveWinner)
      {
        SimulateGame(decks, result, maxTurnsPerGame, maxSearchDepth, maxTargetsCount);

        for (int i = 0; i < decks.Length; i++)
        {
          if (result.DeckWinCounts[i] >= 2)
            haveWinner = true;
        }
      }

      stopwatch.Stop();

      result.Duration = stopwatch.Elapsed;

      return result;
    }

    private static void SimulateGame(Deck[] decks, SimulationResult result, int maxTurnsPerGame,
      int maxSearchDepth, int maxTargetsCount)
    {
      var stopwatch = new Stopwatch();

      var game = new Game(GameParameters.Simulation(decks,
        new SearchParameters(maxSearchDepth, maxTargetsCount, SearchPartitioningStrategies.SingleThreaded)));

      game.Ai.SearchStarted += delegate
        {
          result.TotalSearchCount++;
          stopwatch.Start();
        };

      game.Ai.SearchFinished += delegate
        {
          stopwatch.Stop();

          if (stopwatch.Elapsed > result.MaxSearchTime)
          {
            result.MaxSearchTime = stopwatch.Elapsed;
          }

          stopwatch.Reset();
        };

      game.Start(numOfTurns: maxTurnsPerGame);

      result.TotalTurnCount += game.Turn.TurnCount;

      if (game.Players.BothHaveLost)
        return;

      int winningIndex = -1;
      int greatestScore = 0;

      for(int i =0; i < game.Players.PlayerList.Count(); i++)
      {
        if (game.Players.PlayerList[i].Score > greatestScore)
        {
          greatestScore = game.Players.PlayerList[i].Score;
          winningIndex = i;
        }
      }

      result.DeckWinCounts[winningIndex]++;
      return;

    }

    public class SimulationResult
    {
      public int[] DeckWinCounts { get; set; }
      public TimeSpan Duration { get; set; }
      public int TotalTurnCount { get; set; }
      public int TotalSearchCount { get; set; }
      public TimeSpan MaxSearchTime { get; set; }

      public SimulationResult(int count)
      {
        DeckWinCounts = new int[count];
        for(int i =0; i < count; i++)
          DeckWinCounts[i] = 0;
      }
    }
  }
}