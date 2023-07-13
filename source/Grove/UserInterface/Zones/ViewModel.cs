namespace Grove.UserInterface.Zones
{
  using Castle.Core.Internal;
  using System;

  public class ViewModel : ViewModelBase, IDisposable
  {
    public Graveyard.ViewModel[] OpponentsGraveyard { get; private set; }
    public Hand.ViewModel[] OpponentsHand { get; private set; }
    public Graveyard.ViewModel YourGraveyard { get; private set; }
    public Hand.ViewModel YourHand { get; private set; }
    public Library.ViewModel YourLibrary { get; private set; }
    public Library.ViewModel[] OpponentsLibrary { get; private set; }

    public Exile.ViewModel YourExile { get; private set; }
    public Exile.ViewModel[] OpponentsExile { get; private set; }

    public override void Initialize()
    {
      OpponentsHand = new Hand.ViewModel[Players.Computers.Length];
      OpponentsGraveyard = new Graveyard.ViewModel[Players.Computers.Length];
      OpponentsLibrary = new Library.ViewModel[Players.Computers.Length];
      OpponentsExile = new Exile.ViewModel[Players.Computers.Length];

      for(int i =0; i < Players.Computers.Length; i++)
      {
        OpponentsHand[i] = ViewModels.Hand.Create(Players.Computers[i]);
        OpponentsGraveyard[i] = ViewModels.Graveyard.Create(Players.Computers[i]);
        OpponentsLibrary[i] = ViewModels.Library.Create(Players.Computers[i]);
        OpponentsExile[i] = ViewModels.Exile.Create(Players.Computers[i]);
      }


      YourHand = ViewModels.Hand.Create(Players.Human);
      YourGraveyard = ViewModels.Graveyard.Create(Players.Human);
      YourLibrary = ViewModels.Library.Create(Players.Human);
      YourExile = ViewModels.Exile.Create(Players.Human);
    }

    public void Dispose()
    {
      YourHand.Dispose();
      OpponentsHand.ForEach(x=>x.Dispose());
      YourGraveyard.Dispose();
      OpponentsGraveyard.ForEach(x => x.Dispose());
      YourLibrary.Dispose();
      OpponentsLibrary.ForEach(x => x.Dispose());
      YourExile.Dispose();
      OpponentsExile.ForEach(x => x.Dispose());
    }
  }
}