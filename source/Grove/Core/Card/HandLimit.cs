namespace Grove
{
  using Modifiers;

  public class HandLimit : Characteristic<int?>, IAcceptsPlayerModifier
  {
    private HandLimit() { }
    public HandLimit(int value) : base(value) { }
    public void Accept(IPlayerModifier modifier)
    {
      modifier.Apply(this);
    }
  }
}
