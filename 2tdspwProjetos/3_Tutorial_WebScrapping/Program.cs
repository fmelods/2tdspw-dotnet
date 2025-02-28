// See https://aka.ms/new-console-template for more information

using HtmlAgilityPack;

//foreach (var i in Enumerable.Range(1,7067))

Parallel.ForEach(Enumerable.Range(1, 7067), i =>
{
    var web = new HtmlWeb();
    var doc = web.Load(
        $"https://www.metacritic.com/browse/game/all/all/all-time/new/?releaseYearMin=1958&releaseYearMax=2025&page={i}");
    var gameLinks = doc.DocumentNode.SelectNodes("//a").ToList()
        .Where(link => link.Attributes["href"] != null && link.Attributes["href"].Value.Contains("/game")).ToList();
    foreach (var link in gameLinks)
        Console.WriteLine(link.Attributes["href"].Value);


});

static MetacriticGame getMetacriticData(string url)
{

    Console.WriteLine("Web scrapping in inicializando...");
    var web = new HtmlWeb();
    var doc = web.Load("https://www.metacritic.com/game/super-smash-bros-ultimate/");

    var name = doc.DocumentNode.SelectSingleNode("//h1").InnerText;
    var metascore = doc.DocumentNode
        .SelectSingleNode(
            "/html/body/div[1]/div/div/div[2]/div[1]/div[1]/div/div/div[2]/div[3]/div[4]/div[1]/div/div[1]/div[2]/div/div/span")
        .InnerText;

    var userscore = doc.DocumentNode
        .SelectSingleNode(
            "/html/body/div[1]/div/div/div[2]/div[1]/div[1]/div/div/div[2]/div[3]/div[4]/div[2]/div[1]/div[2]/div/div/span")
        .InnerText;

    var metacriticGame = new MetacriticGame(name, metascore, userscore);
    Console.WriteLine(metacriticGame);
    Console.WriteLine("O jogo é: "+name+". O metascore dele é: "+metascore+" e a userscore é: "+userscore);
    return metacriticGame;
};
public record MetacriticGame(string Name, string MetaScore, string UserScore);