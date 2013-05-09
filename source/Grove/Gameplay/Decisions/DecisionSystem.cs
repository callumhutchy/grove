﻿namespace Grove.Gameplay.Decisions
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;
  using Infrastructure;
  using Misc;
  using Scenario;
  using UserInterface.Decisions;

  public class DecisionSystem
  {
    private static readonly HashSet<Type> ScenarioDecisions = new HashSet<Type>(GetScenarioDecisions());
    private static readonly Dictionary<Type, ParameterlessCtor> MachineDecisions = LoadMachineDecisions();

    private readonly PrerecordedDecisions _prerecordedDecisions = new PrerecordedDecisions();
    private readonly IUiDecisionFactory _uiDecisionFactory;

    public DecisionSystem(IUiDecisionFactory uiDecisionFactory)
    {
      _uiDecisionFactory = uiDecisionFactory;
    }

    private static IEnumerable<Type> GetScenarioDecisions()
    {
      return Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(x => x.Implements<IDecision>())
        .Where(x => x.Namespace.EndsWith("Scenario"))
        .Select(x => x.BaseType);
    }

    private static Dictionary<Type, ParameterlessCtor> LoadMachineDecisions()
    {
      return Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(x => x.Implements<IDecision>())
        .Where(x => x.Namespace.Equals(typeof (Artifical.Decisions.PlaySpellOrAbility).Namespace))
        .Select(x => new
          {
            Type = x.BaseType,
            Ctor = x.GetParameterlessCtor()
          })
        .ToDictionary(x => x.Type, x => x.Ctor);
    }

    public void AddScenarioDecisions(IEnumerable<DecisionsForOneStep> decisions)
    {
      _prerecordedDecisions.AddDecisions(decisions);
    }

    public IDecision Create<TDecision>(Player player, Action<TDecision> setParameters, Game game)
      where TDecision : class, IDecision
    {
      IDecision decision;

      var controller = game.Ai.IsSearchInProgress
        ? ControllerType.Machine
        : player.Controller;

      if (controller == ControllerType.Scenario && ExistsScenarioDecision<TDecision>())
      {
        decision =
          _prerecordedDecisions.GetNext<TDecision>(game.Turn.TurnCount, game.Turn.Step) ??
            new NopScenarioDecision();
      }
      else
      {
        decision = controller == ControllerType.Human
          ? _uiDecisionFactory.Create<TDecision>()
          : CreateMachine<TDecision>();
      }


      decision.Initialize(player, game);
      SetParameters(decision, setParameters);

      return decision;
    }

    private IDecision CreateMachine<TDecision>()
    {
      return (IDecision) MachineDecisions[typeof (TDecision)]();
    }

    private static bool ExistsScenarioDecision<TDecision>()
    {
      return ScenarioDecisions.Contains(typeof (TDecision));
    }

    private static void SetParameters<TDecision>(IDecision decision, Action<TDecision> setParameters)
      where TDecision : class
    {
      var settable = decision as TDecision;
      if (settable != null)
      {
        setParameters(settable);
      }
    }
  }
}