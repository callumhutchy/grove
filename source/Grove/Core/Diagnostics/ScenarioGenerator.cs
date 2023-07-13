namespace Grove.Diagnostics
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;

  public static class ScenarioGenerator
  {
    private const string ScenarioTemplate =
@"
[Fact]
public void Scenario()
{{        
{0}   
}}
";

    public static void WriteScenario(Game game)
    {
      var fileName = String.Format("generated-scenario-{0}.txt", Guid.NewGuid());
      using (var writer = new StreamWriter(fileName, append: true))
      {
        writer.WriteLine(WriteScenarioToString(game));
      }
    }

    public static string WriteScenarioToString(Game game)
    {
      var inner = new StringBuilder();
      for (int i = 0; i < game.Players.Count(); i++)
      {
        Player player = game.Players[i];
        inner = inner.AppendLine(CreateZone("Hand", player.Hand, (i + 1)));
        inner = inner.AppendLine(CreateBattlefield(player.Battlefield, (i + 1)));
        inner = inner.AppendLine(CreateZone("Graveyard", player.Graveyard, (i + 1)));
        inner = inner.AppendLine(CreateZone("Library", player.Library, (i + 1)));
        inner = inner.AppendLine(String.Format("P"+(i+1)+".Life={0};", player.Life));
      }

      inner = inner.AppendLine("RunGame(1);");

      return String.Format(ScenarioTemplate, inner);
    }

    private static string CreateBattlefield(IEnumerable<Card> cards, int player)
    {
      var sb = new StringBuilder();
      sb.AppendFormat("Battlefield(P{0}, ", player);

      foreach (var card in cards)
      {
        if (card.AttachedTo != null)
          continue;

        if (card.HasAttachments)
        {
          sb.AppendFormat("C({0})", CreateCard(card));

          foreach (var attachedCard in card.Attachments)
          {
            if (attachedCard.Is().Enchantment)
            {
              sb.AppendFormat(".IsEnchantedWith({0})", CreateCard(attachedCard));
              continue;
            }

            sb.AppendFormat(".IsEquipedWith({0})", CreateCard(attachedCard));
          }

          sb.Append(", ");
          continue;
        }

        sb.AppendFormat("{0}, ", CreateCard(card));
      }

      sb.Remove(sb.Length - 2, 2);
      sb.Append(");");

      return sb.ToString();
    }

    private static string CreateCard(Card card)
    {
      return String.Format("\"{0}\"", card.Name);
    }

    private static string CreateZone(string zone, IEnumerable<Card> cards, int player)
    {
      var sb = new StringBuilder();
      sb.AppendFormat("{0}(P{1}, ", zone, player);

      foreach (var card in cards)
      {
        sb.AppendFormat("{0}, ", CreateCard(card));
      }

      sb.Remove(sb.Length - 2, 2);
      sb.Append(");");

      return sb.ToString();
    }
  }
}