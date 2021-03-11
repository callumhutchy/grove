namespace Grove.Modifiers
{
  public class NoMaximumHandSize : Modifier, IPlayerModifier
  {
    private HandLimit handLimit;
    private readonly IntegerSetter _integerSetter = new IntegerSetter(9999);
    public override void Apply(HandLimit _handLimit)
    {
      handLimit = _handLimit;
      _integerSetter.Initialize(ChangeTracker);
      handLimit.AddModifier(_integerSetter);
    }

    protected override void Unapply()
    {
      handLimit.RemoveModifier(_integerSetter);
    }
  }
}
