using System;
public interface IMediator
{
    void Notify(string eventCode, Character sender);
}

public class FestivalMediator : IMediator
{
    private List<Character> _characters = new List<Character>();

    public void RegisterCharacter(Character character)
    {
        _characters.Add(character);
        character.SetMediator(this);
    }

    public void Notify(string eventCode, Character sender)
    {
        if (eventCode == "DestinationChosen")
        {
            for (int i = 0; i < _characters.Count; i++)
            {
                if (_characters[i] != sender)
                    _characters[i].ConfirmDestination(sender.Destination);
            }
        }
    }
}

public abstract class Character
{
    protected IMediator _mediator;
    public string Name { get; }
    public string Destination { get; set; }

    protected Character(string name)
    {
        Name = name;
    }

    public void SetMediator(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void ChooseDestination(string destination)
    {
        Destination = destination;
        Console.WriteLine(Name + " chose the destination: " + destination);
        _mediator.Notify("DestinationChosen", this);
    }

    public abstract void ConfirmDestination(string destination);
}

public class Fox : Character
{
    public Fox() : base("Fox") { }

    public override void ConfirmDestination(string destination)
    {
        Console.WriteLine(Name + " confirms: " + destination + " - ready for adventure!");
    }
}

public class Wolf : Character
{
    public Wolf() : base("Wolf") { }

    public override void ConfirmDestination(string destination)
    {
        Console.WriteLine(Name + " confirms: " + destination + " - I'm already on my way!");
    }
}

public class Program
{
    public static void Main()
    {
        var mediator = new FestivalMediator();

        var fox = new Fox();
        var wolf = new Wolf();

        mediator.RegisterCharacter(fox);
        mediator.RegisterCharacter(wolf);

        fox.ChooseDestination("Food Festival");
    }
}