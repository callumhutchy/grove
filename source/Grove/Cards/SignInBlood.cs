﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai.TargetingRules;
  using Core.Ai.TimingRules;
  using Core.Dsl;
  using Core.Effects;

  public class SignInBlood : CardsSource
  {
    public override IEnumerable<CardFactory> GetCards()
    {
      yield return Card
        .Named("Sign in Blood")
        .ManaCost("{B}{B}")
        .Type("Sorcery")
        .Text("Target player draws two cards and loses 2 life.")
        .FlavorText(
          "'You know I accept only one currency here, and yet you have sought me out. Why now do you hesitate?'{EOL}—Xathrid demon")
        .Cast(p =>
          {
            p.Effect = () => new TargetPlayerDrawsCards(2, lifeLoss: 2);
            p.TargetSelector.AddEffect(trg => trg.Is.Player());
            p.TimingRule(new FirstMain());
            p.TargetingRule(new SpellOwner());
          });
    }
  }
}