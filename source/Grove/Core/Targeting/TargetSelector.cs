﻿namespace Grove
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public class TargetSelector : GameObject
  {
    public static readonly TargetSelector NullSelector = new TargetSelector();
    private readonly List<TargetValidator> _costValidators = new List<TargetValidator>();
    private readonly List<TargetValidator> _effectValidators = new List<TargetValidator>();

    public int Count { get { return _costValidators.Count + _effectValidators.Count; } }

    public bool RequiresTargets { get { return _costValidators.Any() || _effectValidators.Any(); } }
    public bool RequiresCostTargets { get { return _costValidators.Count > 0; } }
    public bool RequiresEffectTargets { get { return _effectValidators.Count > 0; } }

    public List<TargetValidator> Effect { get { return _effectValidators; } }
    public List<TargetValidator> Cost { get { return _costValidators; } }
    public Func<ValidateTargetDependenciesParam, bool> ValidateTargetDependencies = delegate { return true; };


    public TargetSelector AddEffect(
      Func<IsValidTargetBuilder, IsValidTargetBuilder> isValid,
      Action<TargetValidatorParameters> configure = null)
    {
      var validatorBuilder = isValid(new IsValidTargetBuilder());

      var validatorParameters = new TargetValidatorParameters(
        validatorBuilder.IsValidTarget,
        validatorBuilder.IsValidZone,
        validatorBuilder.MustBeTargetable);

      if (configure != null)
      {
        configure(validatorParameters);
      }

      _effectValidators.Add(new TargetValidator(validatorParameters));

      return this;
    }

    public TargetSelector AddBolsterEffect()
    {
      return AddEffect(trg => trg.Is.Card(c =>
              c.Is().Creature && c.Controller.Battlefield.Creatures.All(x => x.Toughness >= c.Toughness),
              ControlledBy.SpellOwner).On.Battlefield(),
            trg => trg.MustBeTargetable = false);
    }

    public TargetSelector AddCost(Func<IsValidTargetBuilder, IsValidTargetBuilder> isValid,
      Action<TargetValidatorParameters> configure = null)
    {
      var validatorBuilder = isValid(new IsValidTargetBuilder());

      var validatorParameters = new TargetValidatorParameters(
        validatorBuilder.IsValidTarget,
        validatorBuilder.IsValidZone,
        false);

      if (configure != null)
      {
        configure(validatorParameters);
      }

      _costValidators.Add(new TargetValidator(validatorParameters));
      return this;
    }

    public void Initialize(Card owningCard, Game game)
    {
      Game = game;

      foreach (var validator in _effectValidators)
      {
        validator.Initialize(game, owningCard.Controller, owningCard);
      }

      foreach (var validator in _costValidators)
      {
        validator.Initialize(game, owningCard.Controller, owningCard);
      }
    }

    public TargetsCandidates GenerateCandidates(object triggerMessage = null, ITarget excluded = null)
    {
      var all = new TargetsCandidates();

      foreach (var selector in _costValidators)
      {
        var candidates = new TargetCandidates();

        foreach (var target in GenerateTargets(selector.IsZoneValid))
        {
          if (target != excluded && selector.IsTargetValid(target, triggerMessage))
          {
            candidates.Add(target);
          }
        }

        all.AddCostCandidates(candidates);
      }

      foreach (var selector in _effectValidators)
      {
        var candidates = new TargetCandidates();

        foreach (var target in GenerateTargets(selector.IsZoneValid))
        {
          if (target != excluded && selector.IsTargetValid(target, triggerMessage))
          {
            candidates.Add(target);
          }
        }

        all.AddEffectCandidates(candidates);
      }

      return all;
    }

    public bool IsValidEffectTarget(ITarget target, object triggerMessage = null)
    {
      // Currently there is no way to figure out
      // to which validator the target belongs. 
      // All validators are tried therefore.
      // Currently there are no problems with this, if
      // there are problems in the future this must be 
      // changed, so the target will know to which
      // validator it belongs.            


      return _effectValidators.Any(
        validator =>
          {
            return validator.HasValidZone(target) &&
              validator.IsTargetValid(target, triggerMessage);
          });
    }
  }
}