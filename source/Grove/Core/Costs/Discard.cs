﻿namespace Grove.Core.Costs
{
  using Grove.Core.Targeting;

  public class Discard : Cost
  {
    public override bool CanPay(ref int? maxX)
    {
      return true;
    }

    protected override void Pay(ITarget target, int? x)
    {
      Card.Discard();
    }
  }
}