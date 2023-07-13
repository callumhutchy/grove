namespace Grove.UserInterface.MatchResults
{
  using System;
  using Infrastructure;

  public class ViewModel : ViewModelBase
  {
    public ViewModel(bool canRematch)
    {
      CanRematch = canRematch;
    }

    public string OpponentsResult
    {
      get
      {
        return string.Format("{0} won {1}",
          Players.Player2,
          GetWinCountText(Match.Player2WinCount));
      }
    }

    public string ResultText
    {
      get
      {
        return Players.Player2.HasLost
          ? "Congratulations, you won the match!"
          : "Tough luck, you lost the match!";
      }
    }

    public bool ShouldRematch { get; set; }

    public int WinningAvatar
    {
      get
      {
        foreach (Player p in Players.PlayerList)
          if (!p.HasLost)
            return p.AvatarId;
        return -1;
      }
    }

    public string YourResult
    {
      get
      {
        return string.Format("{0} won {1}",
          Players.PlayerList[0],
          GetWinCountText(Match.PlayerWinCounts[0]));
      }
    }

    public bool CanRematch { get; private set; }

    public void Quit()
    {
      this.Close();
    }


    public void Rematch()
    {
      ShouldRematch = true;
      this.Close();
    }

    private static string GetWinCountText(int winCount)
    {
      if (winCount == 1)
        return "1 game";

      return String.Format("{0} games", winCount);
    }

    public interface IFactory
    {
      ViewModel Create(bool canRematch);
    }
  }
}