namespace Grove
{
  using System.Collections;
  using System.Collections.Generic;
  using System.Linq;
  using Castle.Core.Internal;
  using Infrastructure;

  public class Players : GameObject, IEnumerable<Player>, IHashable
  {
    private readonly TrackableList<Player> _extraTurns = new TrackableList<Player>(orderImpactsHashcode: true);
    private Player _starting;

    public Players(Player[] players)
    {

    }

    private Players() { }

    public Player Active
    {
      get
      {
        foreach (Player player in PlayerList)
          if (player.IsActive)
            return player;
        return null;
      }
    }

    public bool AnotherMulliganRound
    {
      get
      {
        bool canMulligan = false;
        foreach (Player player in PlayerList)
          if (player.CanMulligan)
            canMulligan = true;
        return canMulligan;
      }
    }

    public Player Searching { get; set; }
    public Player Attacking { get { return Active; } }

    public bool BothHaveLost
    {
      get
      {
        bool everyoneLost = true;
        foreach (Player player in PlayerList)
          if (!player.HasLost)
            everyoneLost = false;
        return everyoneLost;
      }
    }

    public Player[] Computers { get { return PlayerList.Where(x => !x.IsHuman).ToArray(); } }

    public Player Human { get { return PlayerList.First(x => x.IsHuman); } }
    public Player this[int num] { get { return PlayerList[num]; } }

    public Player[] Passive { get { return GetOpponents(Active); } }

    public Player[] PlayerList { get; private set; }

    public int Score
    {
      get
      {
        int score = 0;
        PlayerList.ForEach(x => score += x.Score);
        return score;
      }
    }

    public Player Starting
    {
      get { return _starting; }
      set
      {
        _starting = value;
        _starting.IsActive = true;
        _starting.Opponents.ForEach(x=>x.IsActive = false);
      }
    }

    public Player[] WithPriority { get { return PlayerList.Where(x => x.HasPriority).ToArray(); } }
    public Player[] WithoutPriority { get { return PlayerList.Where(x => !x.HasPriority).ToArray(); } }

    public IEnumerator<Player> GetEnumerator()
    {
      return (IEnumerator<Player>)PlayerList.AsEnumerable();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public int CalculateHash(HashCalculator calc)
    {
      List<int> hashes = new List<int>();
      foreach (Player p in PlayerList)
        hashes.Add(calc.Calculate(p));
      hashes.Add(calc.Calculate(Searching));
      hashes.Add(calc.Calculate(_extraTurns));

      return HashCalculator.Combine(hashes);
    }

    public void Initialize(Game game)
    {
      Game = game;

      _extraTurns.Initialize(ChangeTracker);
      foreach (Player player in PlayerList)
        player.Initialize(game);
    }

    public void ChangeActivePlayer()
    {
      if (_extraTurns.Count > 0)
      {
        var nextActive = _extraTurns.PopLast();
        var opponents = GetOpponents(nextActive);

        nextActive.IsActive = true;
        opponents.ForEach(x => x.IsActive = false);

        return;
      }

      for (int i = 0; i < PlayerList.Count(); i++)
      {
        if (PlayerList[i].IsActive)
        {
          PlayerList[i].IsActive = false;
          if (i + 1 > PlayerList.Count() - 1)
          {
            PlayerList[0].IsActive = true;
          }
          else
          {
            PlayerList[i + 1].IsActive = true;
          }
          break;
        }
      }
    }

    public Player[] GetOpponents(Player player)
    {
      return PlayerList.Where(x => !x.Equals(player)).ToArray();
    }

    public void ScheduleExtraTurns(Player player, int count)
    {
      for (var i = 0; i < count; i++)
      {
        _extraTurns.Add(player);
      }
    }

    public void SetPriority(Player player)
    {
      player.HasPriority = true;
      foreach (Player p in GetOpponents(player))
        p.HasPriority = false;
    }

    public bool AnyHasLost()
    {
      foreach (Player p in PlayerList)
        if (p.HasLost)
          return true;
      return false;
    }

    public IEnumerable<Card> Permanents()
    {
      return this.SelectMany(x => x.Battlefield);
    }

    public IEnumerable<Card> AllCards()
    {
      return this.SelectMany(x => x.Library.Concat(x.Hand).Concat(x.Battlefield).Concat(x.Graveyard).Concat(x.Exile));
    }

    public void RemoveDamageFromPermanents()
    {
      foreach (var player in this)
      {
        player.RemoveDamageFromPermanents();
      }
    }

    public void RemoveRegenerationFromPermanents()
    {
      foreach (var player in this)
      {
        player.RemoveRegenerationFromPermanents();
      }
    }
  }
}