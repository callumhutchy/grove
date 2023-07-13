namespace Grove
{
  public class MatchParameters
  {
    public PlayerParameters[] PlayerParameters { get; private set; }
    public bool IsTournament { get; private set; }
    public SavedMatch SavedMatch { get; private set; }
    public bool IsSavedMatch { get { return SavedMatch != null; } }

    public static MatchParameters Default(PlayerParameters[] playerParameters, bool isTournament = false)
    {
      return new MatchParameters
      {
        PlayerParameters = playerParameters,
        IsTournament = isTournament
      };
    }

    public static MatchParameters Load(SavedMatch savedMatch, bool isTournament)
    {
      return new MatchParameters
      {
        PlayerParameters = savedMatch.SavedGame.PlayerParameters,
        IsTournament = isTournament,
        SavedMatch = savedMatch
      };
    }
  }
}