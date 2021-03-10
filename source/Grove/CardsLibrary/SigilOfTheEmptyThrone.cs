namespace Grove.CardsLibrary
{

  using System.Collections.Generic;
  using Grove.Effects;
  using Grove.Events;
  using Grove.AI.TimingRules;
  using Grove.Triggers;

  public class SigilOfTheEmptyThrone : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Sigil of the Empty Throne")
        .ManaCost("{3}{W}{W}")
        .Type("Enchantment")
        .Text("Whenever you cast an enchantment spell, create a 4/4 white Angel creature token with flying.")
        .TriggeredAbility(p =>
        {
          p.Text = "Whenever you cast and enchantment spell, create a 4/4 white Angel creature token with flying.";
          p.Trigger(new OnCastedSpell((c, ctx) =>
             c.Controller.Equals(ctx.You) && c.Is().Enchantment));
          p.Effect = () => new CreateTokens(
            count: 1,
            token: Card
              .Named("Angel")
              .Power(4)
              .Toughness(4)
              .Type("Token Creature - Angel")
              .Text("{Flying}")
              .Colors(CardColor.White)
              .SimpleAbilities(Static.Flying));
          p.TriggerOnlyIfOwningCardIsInPlay = true; 
        });
    }
  }
}