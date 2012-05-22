﻿namespace Grove.Core.Controllers.Results
{
  using System.Collections.Generic;
  using Infrastructure;

  [Copyable]
  public class DamageAssignmentOrder
  {
    private readonly Dictionary<Blocker, int> _assignmentOrder = new Dictionary<Blocker, int>();

    public int this[Blocker blocker] { get { return _assignmentOrder[blocker]; } }

    public void Assign(Blocker blocker, int order)
    {
      _assignmentOrder[blocker] = order;
    }       
  }
}