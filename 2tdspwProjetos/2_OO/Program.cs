// See https://aka.ms/new-console-template for more information

using _2_OO;

Console.WriteLine("Hello, World!");

var card = new Card()
{
    Name = "Lightning Bolt",
    Text = "Deal 3 damage to any target",
    ManaCost = "{R}",
    Cmc = 1,
    //Types = new List<string> { "Instant" }
};

card.Types.Add("Mox Opal");
Console.WriteLine(card.Name);