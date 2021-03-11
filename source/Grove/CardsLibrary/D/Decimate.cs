namespace Grove.CardsLibrary.D
{

  using System.Collections.Generic;
  using AI;
  using AI.TargetingRules;
  using AI.TimingRules;
  using Effects;

  public class Decimate : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Decimate")
        .ManaCost("{2}{R}{G}")
        .Type("Sorcery")
        .Text("Destroy target artifact, target creature, target enchantment, and target land.")
        .Cast(p =>
        {
          p.Effect = () => new DestroyTargetPermanents();

          p.TargetSelector.AddEffect(
            trg => trg.Is.Creature().On.Battlefield(),
            trg =>
            {
              trg.Message = "Select a Creature.";
              trg.MinCount = 1;
              trg.MaxCount = 1;
            })
          .AddEffect(
            trg => trg.Is.Artifact().On.Battlefield(),
          trg =>
            {
              trg.Message = "Select a Artifact.";
              trg.MinCount = 1;
              trg.MaxCount = 1;
            })
          .AddEffect(
            trg => trg.Is.Land().On.Battlefield(),
            trg =>
            {
              trg.Message = "Select a Land.";
              trg.MinCount = 1;
              trg.MaxCount = 1;
            })
          .AddEffect(
            trg => trg.Is.Enchantment().On.Battlefield(),
            trg =>
            {
              trg.Message = "Select a Enchantment.";
              trg.MinCount = 1;
              trg.MaxCount = 1;
            });

          p.TargetingRule(new EffectDestroy());
        });
    }
  }
}
