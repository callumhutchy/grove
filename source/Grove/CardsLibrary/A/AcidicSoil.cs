﻿namespace Grove.CardsLibrary
{
  using System.Collections.Generic;
  using System.Linq;
  using AI.TimingRules;
  using Effects;

  public class AcidicSoil : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Acidic Soil")
        .ManaCost("{3}{R}")
        .Type("Sorcery")
        .Text("Acidic Soil deals damage to each player equal to the number of lands he or she controls.")
        .FlavorText("Phyrexia had tried to take Urza's soul. He was relieved that Shiv tried to claim only his soles.")
        .Cast(p =>
          {
            p.TimingRule(new OnFirstMain());
            p.Effect = () => new DealDamageToCreaturesAndPlayers(
              amountPlayer: (e, player) => player.Battlefield.Lands.Count());
          });
    }
  }
}