﻿namespace Grove.Core
{
  using System.Collections;
  using System.Collections.Generic;
  using Infrastructure;

  [Copyable]
  public class Players : IEnumerable<Player>, IHashable
  {
    private readonly TrackableList<Player> _extraTurns;
    private Player _player1;
    private Player _player2;
    private Player _starting;

    public Players(ChangeTracker changeTracker)
    {
      _extraTurns = new TrackableList<Player>(changeTracker, orderImpactsHashcode: true);
    }

    private Players() {}

    public Player Active { get { return Player1.IsActive ? Player1 : Player2; } }

    public bool AnotherMulliganRound
    {
      get
      {
        return
          (Player1.CanMulligan) ||
            (Player2.CanMulligan);
      }
    }

    public Player Attacking { get { return Active; } }
    public bool BothHaveLost { get { return Player1.HasLost && Player2.HasLost; } }
    public Player Computer { get { return Player1.IsHuman ? Player2 : Player1; } }
    public Player Defending { get { return Passive; } }
    public Player Human { get { return Player1.IsHuman ? Player1 : Player2; } }
    public Player this[int num] { get { return num == 0 ? Player1 : Player2; } }
    public Player Max { get { return Player1.IsMax ? Player1 : Player2; } }
    public Player Min { get { return GetOpponent(Max); } }
    public Player Passive { get { return GetOpponent(Active); } }

    public Player Player1
    {
      get { return _player1; }
      set
      {
        _player1 = value;
        _player1.IsMax = true;
      }
    }

    public Player Player2
    {
      get { return _player2; }
      set
      {
        _player2 = value;
        _player2.IsMax = false;
      }
    }

    public int Score { get { return Player1.Score + Player2.Score; } }

    public Player Starting
    {
      get { return _starting; }
      set
      {
        _starting = value;

        value.IsActive = true;
        GetOpponent(value).IsActive = false;
      }
    }

    public Player WithPriority { get { return _player1.HasPriority ? _player1 : _player2; } }
    public Player WithoutPriority { get { return _player1.HasPriority ? _player2 : _player1; } }

    public IEnumerator<Player> GetEnumerator()
    {
      yield return Player1;
      yield return Player2;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public int CalculateHash(HashCalculator hashCalculator)
    {
      return hashCalculator.Calculate(
        Player1,
        Player2,
        _extraTurns
        );
    }

    public void SetAiVisibility(Player player)
    {
      Player1.SetAiVisibility(player);
      Player2.SetAiVisibility(player);
    }

    public void ChangeActivePlayer()
    {
      if (_extraTurns.Count > 0)
      {
        var nextActive = _extraTurns.PopLast();
        var opponent = GetOpponent(nextActive);

        nextActive.IsActive = true;
        opponent.IsActive = false;

        return;
      }

      Player1.IsActive = !Player1.IsActive;
      Player2.IsActive = !Player2.IsActive;
    }

    public Player GetOpponent(Player player)
    {
      return player == Player1 ? Player2 : Player1;
    }

    public void ScheduleExtraTurns(Player player, int count)
    {
      for (int i = 0; i < count; i++)
      {
        _extraTurns.Add(player);
      }
    }

    public void SetPriority(Player player)
    {
      player.HasPriority = true;
      GetOpponent(player).HasPriority = false;
    }
  }
}