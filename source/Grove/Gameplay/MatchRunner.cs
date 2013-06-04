﻿namespace Grove.Gameplay
{
  public class MatchRunner
  {
    private readonly Match.IFactory _matchFactory;
    private MatchParameters _matchParameters;

    public MatchRunner(Match.IFactory matchFactory)
    {
      _matchFactory = matchFactory;
    }

    public Match Current { get; private set; }

    public void ForceRematch()
    {
      Current.Rematch();      
    }

    public void Start(MatchParameters matchParameters)
    {
      _matchParameters = matchParameters;

      Current = _matchFactory.Create(_matchParameters);
      Current.Start();
    }    

    public void Start(PlayerParameters player1, PlayerParameters player2, bool isTournament)
    {
      var matchParameters = MatchParameters.Default(
        player1: player1,
        player2: player2,
        isTournament: isTournament);

      Start(matchParameters);
    }
  }
}