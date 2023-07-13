namespace Grove
{
  using AI;

  public class GameParameters
  {
    private GameParameters() { }

    public PlayerParameters[] PlayerParameters { get; private set; }
    public SearchParameters SearchParameters { get; private set; }
    public SavedGame SavedGame { get; private set; }
    public PlayerType[] PlayerControllers { get; private set; }
    public int RollBack { get; private set; }
    public int? Looser { get; private set; }

    public bool IsSavedGame { get { return SavedGame != null; } }

    public Settings Settings { get; private set; }

    public static GameParameters Default(PlayerParameters[] players)
    {
      var settings = Settings.Load();

      return new GameParameters
      {
        PlayerParameters = players,
        SearchParameters = settings.GetSearchParameters(),
        Settings = settings
      };
    }

    public static GameParameters Scenario(PlayerType[] playeControllers,
      SearchParameters searchParameters)
    {
      return new GameParameters
      {
        PlayerParameters = new PlayerParameters[]
          {
            new PlayerParameters {Name = "Player1", Deck = Deck.CreateUncastable()},
            new PlayerParameters {Name = "Player2", Deck = Deck.CreateUncastable()}
          },
        PlayerControllers = playeControllers,
        SearchParameters = searchParameters,
        Settings = Settings.Load()
      };
    }

    public static GameParameters Simulation(Deck[] playerDecks, SearchParameters searchParameters)
    {
      GameParameters gp = new GameParameters();
      gp.PlayerParameters = new PlayerParameters[playerDecks.Length];
      gp.PlayerControllers = new PlayerType[playerDecks.Length];
      for (int i = 0; i < playerDecks.Length; i++)
      {
        gp.PlayerParameters[i] = new PlayerParameters { Name = "Player" + (i + 1), Deck = playerDecks[i] };
        gp.PlayerControllers[i] = PlayerType.Machine;
      }
      gp.SearchParameters = searchParameters;
      gp.Settings = Settings.Load();

      return gp;
    }

    public static GameParameters Load(PlayerType[] playerControllers,
      SavedGame savedGame, int? looser = null, int rollback = 0, SearchParameters searchParameters = null)
    {
      var settings = Settings.Load();
      return new GameParameters()
      {
        PlayerParameters = savedGame.PlayerParameters,
        PlayerControllers = playerControllers,
        SearchParameters = searchParameters ?? settings.GetSearchParameters(),
        SavedGame = savedGame,
        RollBack = rollback,
        Looser = looser,
        Settings = settings
      };

    }
  }
}