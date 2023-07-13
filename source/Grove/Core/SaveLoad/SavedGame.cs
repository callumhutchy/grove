namespace Grove
{
  using System;
  using System.IO;

  [Serializable]
  public class SavedGame
  {
    public MemoryStream Decisions;
    public PlayerParameters[] PlayerParameters;
    public int RandomSeed;
    public int StateCount;
  }
}