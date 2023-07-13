namespace Grove
{
  using System;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Windows;
  using Castle.Core.Internal;
  using Grove.Infrastructure;
  using Grove.UserInterface;

  public class Match
  {
    private readonly MatchParameters _p;
    private int? _looser;
    private bool _playerLeftMatch;
    private bool _rematch;
    private bool _skipNextScoreUpdate;

    public Match(MatchParameters p)
    {
      _p = p;
      Application.Current.Exit += delegate { Stop(); };
    }

    public bool IsTournament
    {
      get { return _p.IsTournament; }
    }

    public bool WasStopped
    {
      get { return Game != null && Game.WasStopped; }
    }

    public Game Game { get; private set; }

    public bool IsFinished
    {
      get
      {
        foreach (int c in PlayerWinCounts)
          if (c == 2)
            return true;
        return false;
      }
    }

    public int[] PlayerWinCounts { get; private set; }

    protected Player Looser
    {
      get
      {
        if (_looser == null)
          return null;

        return Game.Players[_looser.Value];
      }
    }

    public bool InProgress
    {
      get { return Game != null && !IsFinished; }
    }

    public string Description
    {
      get
      {
        string desc = "";
        for(int i =0; i < Game.Players.PlayerList.Count(); i++)
        {
          desc += Game.Players.PlayerList[i].Name + " ";
          if (i < Game.Players.PlayerList.Count() - 1)
            desc += "vs ";
        }
        desc += " - " + Game.Turn.TurnCount + ". turn";
        return desc;
      }
    }

    public void Start()
    {
      Game game;

      if (_p.IsSavedMatch)
      {
        game = new Game(GameParameters.Load(
          new PlayerType[] { PlayerType.Human, PlayerType.Machine},
          savedGame: _p.SavedMatch.SavedGame,
          looser: _p.SavedMatch.Looser));

        PlayerWinCounts = _p.SavedMatch.PlayerWinCounts;

        _looser = _p.SavedMatch.Looser;

        if (game.IsFinished)
        {
          // if the game was saved when it was already finished
          // do not update the score again
          _skipNextScoreUpdate = true;
        }
      }
      else
      {
        game = new Game(GameParameters.Default(_p.PlayerParameters));
      }

      var shouldPlayAnotherGame = RunGame(game);

      while (shouldPlayAnotherGame || _rematch)
      {
        if (_rematch)
        {
          PlayerWinCounts.ForEach(x => x = 0);
          _rematch = false;
          _looser = null;
        }

        game = new Game(GameParameters.Default(_p.PlayerParameters));

        shouldPlayAnotherGame = RunGame(game);
      }
    }

    public void Rematch()
    {
      Stop();
      _rematch = true;
    }

    public void Stop()
    {
      _rematch = false;

      if (Game != null)
      {
        Game.Stop();
      }

      Ui.Shell.CloseAllDialogs();
    }

    public SavedMatch Save()
    {
      var savedMatch = new SavedMatch
        {
          PlayerWinCounts = PlayerWinCounts,
          SavedGame = Game.Save(),
          Looser = _looser
        };

      return savedMatch;
    }

    private void DisplayGameResults()
    {
      var viewModel = Ui.Dialogs.GameResults.Create();
      Ui.Shell.ShowModalDialog(viewModel);
      _playerLeftMatch = viewModel.PlayerLeftMatch;
    }

    private void DisplayMatchResults()
    {
      var viewModel = Ui.Dialogs.MatchResults.Create(canRematch: !IsTournament);
      Ui.Shell.ShowModalDialog(viewModel);
      _rematch = viewModel.ShouldRematch;
    }

    private bool RunGame(Game game)
    {
      Game = game;

      var playScreen = Ui.Dialogs.PlayScreen.Create();
      Ui.Shell.ChangeScreen(playScreen);

      var blocker = new ThreadBlocker();

      AggregateException exception = null;

      blocker.BlockUntilCompleted(() => Task.Factory
        .StartNew(() => Game.Start(looser: Looser), TaskCreationOptions.LongRunning)
        .ContinueWith(t => { exception = t.Exception; }, TaskContinuationOptions.OnlyOnFaulted)
        .ContinueWith(t => blocker.Completed()));

      if (exception != null)
        throw new AggregateException(exception.InnerExceptions);

      return ProcessGameResults();
    }

    private bool ProcessGameResults()
    {
      if (Game.WasStopped)
        return false;

      var looser = UpdateScore() ?? _looser;

      if (Game.WasStopped)
        return false;

      if (IsFinished)
      {
        DisplayMatchResults();

        if (Game.WasStopped)
          return false;

        if (_rematch && !_playerLeftMatch)
        {
          return false;
        }

        return false;
      }

      DisplayGameResults();

      if (Game.WasStopped)
        return false;

      if (_playerLeftMatch)
      {
        Player2WinCount = 2;
        return false;
      }

      _looser = looser;
      return true;
    }

    private int? UpdateScore()
    {
      if (Game.Players.BothHaveLost)
        return null;

      if (Game.Players.Player1.HasLost)
      {
        if (!_skipNextScoreUpdate)
          Player2WinCount++;

        _skipNextScoreUpdate = false;
        return 0;
      }

      if (!_skipNextScoreUpdate)
        Player1WinCount++;

      _skipNextScoreUpdate = false;
      return 1;
    }
  }
}