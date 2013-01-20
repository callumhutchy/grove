﻿namespace Grove.Core.Decisions
{
  using Effects;
  using Results;
  using Targeting;

  public abstract class SetTriggeredAbilityTarget : Decision<ChosenTargets>
  {
    public TriggeredAbility Source { get; set; }
    public object Trigger { get; set; }
    public EffectFactory Factory { get; set; }
    public TargetSelector TargetSelector { get; set; }

    public override void Init()
    {
      TargetSelector.SetTrigger(Trigger);
    }

    public override void ProcessResults()
    {
      if (!Result.HasTargets)
        return;

      var effectParameters = new EffectParameters(
        Source,
        Source.EffectCategories,
        new ActivationParameters{Targets = Result.Targets},
        Trigger);

      var effect = Factory.CreateEffect(effectParameters, Game);

      Game.Stack.Push(effect);
    }
  }
}