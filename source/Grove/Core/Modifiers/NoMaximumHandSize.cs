namespace Grove.Modifiers
{
  public class NoMaximumHandSize : Modifier, IPlayerModifier
  {
    private HandLimit handLimit;
    public override void Apply(HandLimit _handLimit)
    {
      handLimit = _handLimit;
      handLimit.ChangeBaseValue(9999);
    }

    protected override void Unapply()
    {
      handLimit.ChangeBaseValue(7);
    }
  }
}
