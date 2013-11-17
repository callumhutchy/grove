﻿namespace Grove.UserInterface.Steps
{
  using System.Collections.Generic;
  using System.Linq;
  using Grove.Gameplay.Messages;
  using Grove.Gameplay.States;
  using Grove.Infrastructure;

  public class ViewModel : ViewModelBase, IReceive<TurnStarted>
  {
    private readonly List<UserInterface.Step.ViewModel> _steps = new List<UserInterface.Step.ViewModel>();

    public IEnumerable<UserInterface.Step.ViewModel> Steps { get { return _steps; } }

    public virtual int TurnNumber { get; protected set; }

    public void Receive(TurnStarted message)
    {
      TurnNumber = message.TurnCount;
    }

    public override void Initialize()
    {
      _steps.AddRange(CreateStepViewModels());
      TurnNumber = CurrentGame.Turn.TurnCount;

      SetCurrentStep();
    }

    private void SetCurrentStep() {
      var currentStep = _steps.SingleOrDefault(x => x.IsStep(CurrentGame.Turn.Step));
      
      if (currentStep != null)  // pre untap steps are not shown (e.g Mulligan step)
        currentStep.IsCurent = true;
    }

    private IEnumerable<UserInterface.Step.ViewModel> CreateStepViewModels()
    {
      yield return ViewModels.Step.Create("Untap", Step.Untap);
      yield return ViewModels.Step.Create("Upkeep", Step.Upkeep);
      yield return ViewModels.Step.Create("Draw", Step.Draw);
      yield return ViewModels.Step.Create("1st main", Step.FirstMain);
      yield return ViewModels.Step.Create("Beg. of combat", Step.BeginningOfCombat);
      yield return ViewModels.Step.Create("Dec. attackers", Step.DeclareAttackers);
      yield return ViewModels.Step.Create("Dec. blockers", Step.DeclareBlockers);
      yield return ViewModels.Step.Create("Combat damage", Step.CombatDamage, Step.FirstStrikeCombatDamage);
      yield return ViewModels.Step.Create("End of combat", Step.EndOfCombat);
      yield return ViewModels.Step.Create("2nd main", Step.SecondMain);
      yield return ViewModels.Step.Create("End of turn", Step.EndOfTurn);
      yield return ViewModels.Step.Create("Clean up", Step.CleanUp);
    }
  }
}