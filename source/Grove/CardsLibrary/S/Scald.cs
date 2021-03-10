﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using Effects;
  using Events;
  using Triggers;

  public class Scald : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Scald")
        .ManaCost("{1}{R}")
        .Type("Enchantment")
        .Text("Whenever a player taps an Island for mana, Scald deals 1 damage to that player.")
        .FlavorText("Shiv may be surrounded by water, but the mountains go far deeper.")
        .TriggeredAbility(p =>
          {
            p.Text = "Whenever a player taps an Island for mana, Scald deals 1 damage to that player.";
            p.Trigger(new OnPermanentGetsTapped((a, c) => c.Is("island")));

            p.Effect = () => new DealDamageToPlayer(
              amount: 1,
              player: P(e => e.TriggerMessage<PermanentTappedEvent>().Card.Controller));

            p.TriggerOnlyIfOwningCardIsInPlay = true;
          });
    }
  }
}