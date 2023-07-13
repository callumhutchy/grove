namespace Grove
{
  using System;

  [Serializable]
  public class SavedMatch
  {
    public int[] PlayerWinCounts;
    public SavedGame SavedGame;
    public int? Looser;
  }
}