﻿namespace Grove.Tests.Infrastructure
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;
  using Artifical;
  using Gameplay;
  using Gameplay.Counters;
  using Gameplay.Decisions.Scenario;
  using Gameplay.Misc;
  using Gameplay.Modifiers;
  using Gameplay.States;
  using Gameplay.Zones;
  using Grove.Infrastructure;
  using log4net.Config;
  using Persistance;
  using Xunit;

  public abstract class Scenario : IDisposable
  {
    protected static readonly IoC Container = new IoC(IoC.Configuration.Test);

    protected Scenario(bool player1ControlledByScript = true, bool player2ControlledByScript = true)
    {
      var player1Controller = player1ControlledByScript ? ControllerType.Scenario : ControllerType.Machine;
      var player2Controller = player2ControlledByScript ? ControllerType.Scenario : ControllerType.Machine;

      var p = GameParameters.Scenario(player1Controller, player2Controller);
      Game = GameFactory.Create(p);
      Game.Players.Starting = Game.Players.Player1;
    }

    protected CardFactory CardFactory { get { return Container.Resolve<CardFactory>(); } }
    protected CardDatabase CardDatabase { get { return Container.Resolve<CardDatabase>(); } }
    protected MatchSimulator MatchSimulator { get { return Container.Resolve<MatchSimulator>(); } }
    protected Game.IFactory GameFactory { get { return Container.Resolve<Game.IFactory>(); } }    

    protected Game Game { get; private set; }
    protected Player P1 { get { return Game.Players.Player1; } }
    protected Player P2 { get { return Game.Players.Player2; } }
    protected Combat Combat { get { return Game.Combat; } }

    public virtual void Dispose() {}

    protected void EnableChangeTrackerChecks()
    {
      NullTracker.EnableChangeTrackerChecks();
    }

    protected Deck GetDeck(string name)
    {
      return DeckFile.Read(@".\" + name);
    }

    protected DecisionsForOneStep At(Step step, int turn = 1)
    {
      return new DecisionsForOneStep(step, turn, Game);
    }

    protected void Library(Player player, params ScenarioCard[] cards)
    {
      var library = (Library) player.Library;

      foreach (var scenarioCard in cards.Reverse())
      {
        scenarioCard.Initialize(name =>
          {
            var card = CardFactory.CreateCard(name);
            card.Initialize(player, Game);
            library.AddToFront(card);

            return card;
          });
      }
    }

    protected void ExileLibrary(Player player)
    {
      var library = (Library)player.Library;

      foreach (var card in library.ToList())
      {
        card.Exile();
      }
    }

    protected void Battlefield(Player player, params ScenarioCard[] cards)
    {
      foreach (var scenarioCard in cards)
      {
        scenarioCard.Initialize(name =>
          {
            var card = CardFactory.CreateCard(name);
            card.Initialize(player, Game);

            player.PutCardToBattlefield(card);
            card.HasSummoningSickness = false;

            if (scenarioCard.IsTapped)
              card.Tap();

            foreach (var enchantment in scenarioCard.Enchantments)
            {
              enchantment.Initialize(enchantmentName =>
                {
                  var enchantmentCard = CardFactory.CreateCard(enchantmentName);
                  enchantmentCard.Initialize(player, Game);
                  EnchantCard(card, enchantmentCard);
                  return enchantmentCard;
                });
            }

            foreach (var equipment in scenarioCard.Equipments)
            {
              equipment.Initialize(equipmentName =>
                {
                  var equipmentCard = CardFactory.CreateCard(equipmentName);
                  equipmentCard.Initialize(player, Game);
                  player.PutCardToBattlefield(equipmentCard);
                  EquipCard(card, equipmentCard);
                  return equipmentCard;
                });
            }

            foreach (var tracked in scenarioCard.TrackedBy)
            {
              tracked.Initialize(trackerName =>
                {
                  var tracker = CardFactory.CreateCard(trackerName);
                  tracker.Initialize(player, Game);
                  player.PutCardToBattlefield(tracker);
                  TrackCard(card, tracker);
                  return tracker;
                });
            }

            if (scenarioCard.HasCounters)
            {
              var p = new ModifierParameters
                {
                  SourceCard = card,                  
                };

              var counters = new AddCounters(() => new SimpleCounter(scenarioCard.Counters.Type),
                scenarioCard.Counters.Count);
              
              card.AddModifier(counters, p);
            }

            return card;
          });
      }
    }

    protected IEnumerable<Card> Permanents(Player controller, params string[] cardNames)
    {
      foreach (var cardName in cardNames)
      {
        var battlefield = (Battlefield) controller.Battlefield;
        var card = CardFactory.CreateCard(cardName);
        card.Initialize(controller, Game);
        battlefield.Add(card);
        yield return card;
      }
    }

    protected ScenarioCard C(string name)
    {
      return name;
    }

    protected Card C(ScenarioCard scenarioCard)
    {
      return scenarioCard;
    }

    protected ScenarioEffect E(ScenarioCard scenarioCard)
    {
      return new ScenarioEffect {Effect = () => Game.Stack.First(x => x.Source.OwningCard == scenarioCard.Card)};
    }

    protected void EnableLogging(string level)
    {
      var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
        String.Format("Grove.Tests.Infrastructure.Logger.{0}.xml", level));

      XmlConfigurator.Configure(stream);
    }


    protected void Equal<T>(T expected, T actual)
    {
      Assert.Equal(expected, actual);
    }

    protected void EquipCard(Card card, Card equipment)
    {
      equipment.EquipWithoutPayingCost(card);
    }

    protected void TrackCard(Card card, Card tracker)
    {
      card.Attach(tracker);
    }

    protected void Exec(params DecisionsForOneStep[] decisions)
    {
      const int untilTurn = 5;
      Game.AddScenarioDecisions(decisions);
      RunGame(untilTurn);
      AssertAllCommandsHaveRun(decisions);
    }

    protected void False(bool condition, string message = null)
    {
      Assert.False(condition, message);
    }

    protected void Graveyard(Player player, params ScenarioCard[] cards)
    {
      var graveyard = (Graveyard) player.Graveyard;

      foreach (var scenarioCard in cards)
      {
        scenarioCard.Initialize(name =>
          {
            var card = CardFactory.CreateCard(name);
            card.Initialize(player, Game);
            graveyard.Add(card);

            return card;
          });
      }
    }

    protected void Hand(Player player, params ScenarioCard[] cards)
    {
      var hand = (Hand) player.Hand;

      foreach (var scenarioCard in cards)
      {
        scenarioCard.Initialize(name =>
          {
            var card = CardFactory.CreateCard(name);
            card.Initialize(player, Game);
            hand.Add(card);

            return card;
          });
      }
    }

    protected void Null(object obj)
    {
      Assert.Null(obj);
    }

    protected virtual void RunGame(int maxTurnCount)
    {
      Game.Start(maxTurnCount, skipPreGame: true);
    }

    protected void True(bool condition, string message = null)
    {
      Assert.True(condition, message);
    }

    protected void UnEquipCard(Card card, Card equipment)
    {
      card.Detach(equipment);
    }

    private static void AssertAllCommandsHaveRun(IEnumerable<DecisionsForOneStep> commands)
    {
      foreach (var stepCommands in commands)
      {
        stepCommands.AssertAllWereExecuted();
      }
    }

    private static void EnchantCard(Card card, Card enchantment)
    {
      enchantment.EnchantWithoutPayingCost(card);
    }
  }
}