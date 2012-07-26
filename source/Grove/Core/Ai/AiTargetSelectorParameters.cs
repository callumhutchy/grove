﻿namespace Grove.Core.Ai
{
  using System.Collections.Generic;
  using System.Linq;
  using Infrastructure;
  using Targeting;
  using Zones;

  public delegate List<Targets> AiTargetSelectorDelegate(AiTargetSelectorParameters parameters);

  public class AiTargetSelectorParameters
  {
    public AiTargetSelectorParameters(TargetsCandidates candidates, Card source, int? maxX, bool forceOne, Game game)
    {
      AllCandidates = candidates;
      Source = source;
      MaxX = maxX;
      ForceOne = forceOne;
      Game = game;
    }

    public Game Game { get; private set; }
    public TargetsCandidates AllCandidates { get; private set; }
    public Card Source { get; private set; }
    public int? MaxX { get; private set; }
    public bool ForceOne { get; private set; }
    public Player Opponent { get { return Game.Players.GetOpponent(Source.Controller); } }
    public Player Controller { get { return Source.Controller; } }
    public Combat Combat { get { return Game.Combat; } }
    public Step Step { get { return Game.Turn.Step; } }
    public Stack Stack { get { return Game.Stack; } }

    public IEnumerable<ITarget> Candidates(int index = 0)
    {
      return AllCandidates.HasCost ? AllCandidates.Cost(index) : AllCandidates.Effect(index);
    }

    public List<Targets> MultipleTargetsOneChoice(IDamageDistributor distributor, List<ITarget> candidates)
    {
      var targets = new Targets();

      foreach (var candidate in candidates)
      {
        targets.AddEffect(candidate);
      }

      targets.DamageDistributor = distributor;
      return targets.ToEnumerable().ToList();
    }

    public List<Targets> MultipleTargetsManyChoices(params List<ITarget>[] candidates)
    {
      var targetsList = new List<Targets>();

      for (var i = 0; i < candidates[0].Count; i++)
      {
        var targets = new Targets();

        for (var j = 0; j < candidates.Length; j++)
        {
          targets.AddEffect(candidates[j][i]);
        }

        targetsList.Add(targets);
      }
      return targetsList;
    }

    public List<Targets> NoTargets()
    {
      return new List<Targets>();
    }

    public List<Targets> Targets(IEnumerable<ITarget> candidates)
    {
      var targets = AllCandidates.HasCost
        ? candidates.Select(x => new Targets().AddCost(x))
        : candidates.Select(x => new Targets().AddEffect(x));

      return targets.ToList();
    }
  }
}