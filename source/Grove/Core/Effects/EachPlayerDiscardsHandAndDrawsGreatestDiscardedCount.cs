namespace Grove.Effects
{
  using System;

  public class EachPlayerDiscardsHandAndDrawsGreatestDiscardedCount : Effect
  {
    protected override void ResolveEffect()
    {
      int greatestDiscardedCount = 0;
      foreach(Player p in Players.PlayerList)
        if (p.Hand.Count > greatestDiscardedCount)
          greatestDiscardedCount = p.Hand.Count;

      Players.Active.DiscardHand();
      Players.Active.DrawCards(greatestDiscardedCount);

      //TODO make sure this is done in the correct order starting from the active player

      foreach(Player p in Players.Passive)
      {
        p.DiscardHand();
        p.DrawCards(greatestDiscardedCount);
      }

    }
  }
}