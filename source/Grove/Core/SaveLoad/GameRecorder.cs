namespace Grove
{
  using System.IO;

  public class GameRecorder
  {
    private readonly DecisionLog _decisionLog;
    private readonly Game _game;
    private readonly IdentityManager _identityManager;

    public GameRecorder(Game game, MemoryStream savedDecisions = null)
    {
      _game = game;
      _identityManager = new IdentityManager();
      _decisionLog = new DecisionLog(game, savedDecisions);
    }

    public bool IsPlayback { get { return !_decisionLog.IsAtTheEnd; } }

    public int CreateId(object obj)
    {
      if (_game.Ai.IsSearchInProgress)
        return -1;

      return _identityManager.GetId(obj);
    }

    public object GetObject(int id)
    {
      if (_game.Ai.IsSearchInProgress)
        return null;

      return _identityManager.GetObject(id);
    }

    public void SaveDecisionResult(object result)
    {
      if (_game.Ai.IsSearchInProgress)
        return;

      _decisionLog.SaveResult(result);
    }

    public object LoadDecisionResult()
    {
      return _decisionLog.LoadResult();
    }

    public SavedGame SaveGame()
    {
      var decisions = new MemoryStream();
      _decisionLog.WriteTo(decisions);

      var players = _game.Players.PlayerList;

      PlayerParameters[] pps = new PlayerParameters[players.Length];
      for (int i = 0;i < pps.Length; i++)
      {
        pps[i] = new PlayerParameters()
        {
          Name = pps[i].Name,
          AvatarId = pps[i].AvatarId,
          Deck = pps[i].Deck
        };
      }

      var savedGame = new SavedGame
      {
        PlayerParameters = pps,
          RandomSeed = _game.Random.Seed,
          Decisions = decisions,
          StateCount = _game.Turn.StateCount
        };

      return savedGame;
    }

    public void DiscardUnloadedResults()
    {
      _decisionLog.DiscardUnloadedResults();
    }
  }
}