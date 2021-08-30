namespace Grove.CardsLibrary.D
{
  using Grove.Effects;
  using Grove.Triggers;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class ThunderbreakRegent : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Thunderbreak Regent")
        .ManaCost("{2}{R}{R}")
        .Type("Creature Dragon")
        .Text("{Flying}{EOL}Whenever a Dragon you control becomes the target of a spell or ability an opponent controls, Thunderbreak Regent deals 3 damage to that player.")
        .Power(4)
        .Toughness(4)
        .SimpleAbilities(Static.Flying)
        .TriggeredAbility(p =>
        {
          p.Text = "Whenever a Dragon you control becomes the target of a spell or ability an opponent controls, Thunderbreak Regent deals 3 damage to that player.";
          p.Trigger(new OnBeingTargetedBySpellOrAbility((target, effect, trigger) =>
          {
            if (effect.Controller == trigger.Controller)
              return false;
            return target.IsCard() && target.Card().Is("dragon") && target.Card().Controller == trigger.Controller;
          }));
          p.TriggerOnlyIfOwningCardIsInPlay = true;
          p.Effect = () => new DealDamageToPlayer(amount: 3, player: P(e => e.TriggerMessage<Events.EffectPutOnStackEvent>().Effect.Controller));
        });
    }
  }
}
