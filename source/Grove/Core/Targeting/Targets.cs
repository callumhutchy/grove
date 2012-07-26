﻿namespace Grove.Core.Targeting
{
  using System.Collections;
  using System.Collections.Generic;
  using System.Linq;
  using Infrastructure;

  [Copyable]
  public class Targets : IEnumerable<ITarget>, IHashable
  {
    private static readonly NoDamageDistribution NoDistribution = new NoDamageDistribution();

    private readonly List<ITarget> _costTargets = new List<ITarget>();
    private readonly List<ITarget> _effectTargets = new List<ITarget>();
    private IDamageDistributor _damageDistributor = NoDistribution;
    public int Count { get { return _effectTargets.Count + _costTargets.Count; } }
    public IList<ITarget> Effect { get { return _effectTargets; } }
    public IList<ITarget> Cost { get { return _costTargets; } }

    public IDamageDistributor DamageDistributor
    {
      get { return _damageDistributor; }
      set
      {
        if (value == null)
          return;

        _damageDistributor = value;
      }
    }

    public IEnumerator<ITarget> GetEnumerator()
    {
      foreach (var costTarget in _costTargets)
      {
        yield return costTarget;
      }

      foreach (var effectTarget in _effectTargets)
      {
        yield return effectTarget;
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public int CalculateHash(HashCalculator calc)
    {
      return HashCalculator.Combine(
        HashCalculator.Combine(_effectTargets.Select(calc.Calculate)),
        HashCalculator.Combine(_costTargets.Select(calc.Calculate)));
    }

    public Targets AddCost(ITarget target)
    {
      _costTargets.Add(target);
      return this;
    }

    public Targets AddEffect(ITarget target)
    {
      _effectTargets.Add(target);
      return this;
    }
  }
}