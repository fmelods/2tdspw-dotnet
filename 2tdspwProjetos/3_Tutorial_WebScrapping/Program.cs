using HtmlAgilityPack;

Console.WriteLine("Web scrapping inicializando...");
var web = new HtmlWeb();
var doc = web.Load
    ("https://www.metacritic.com/game/avowed/");
Console.WriteLine(doc.DocumentNode.InnerHtml);