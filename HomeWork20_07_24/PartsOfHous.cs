using System.Collections.Generic;


abstract class Item
{
    protected bool status;
}
class House
{
    List<IPart> ListOfParts;
    public House()
    {
        ListOfParts = new List<IPart>
            {
                new Basement(), new Wall(), new Wall(), new Wall(),
                new Wall(), new Door(), new Window(), new Window(),
                new Window(), new Window(), new Roof()
            };
    }
    public List<IPart> GetList() => ListOfParts;
}

class Basement : Item, IPart
{
    public Basement() { Status = false; }
    public bool Status
    {
        get => status;
        set { status = value; }
    }
    public string ShowPart() => "Фундамент";
}

class Wall : Item, IPart
{
    public Wall() { Status = false; }
    public bool Status
    {
        get => status;
        set { status = value; }
    }
    public string ShowPart() => "Стена";
}

class Door : Item, IPart
{
    public Door() { Status = false; }
    public bool Status
    {
        get => status;
        set { status = value; }
    }
    public string ShowPart() => "Дверь";
}

class Window : Item, IPart
{
    public Window() { Status = false; }
    public bool Status
    {
        get => status;
        set { status = value; }
    }
    public string ShowPart() => "Окно";
}

class Roof : Item, IPart
{
    public Roof() { Status = false; }
    public bool Status
    {
        get => status;
        set { status = value; }
    }
    public string ShowPart() => "Крыша";
}