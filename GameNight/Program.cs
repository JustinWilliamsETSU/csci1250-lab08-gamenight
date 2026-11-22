// Constants
const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;

// Look for the data files next to Program.cs, however the program is started.
string projectFolder = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory,"..","..",".."));
Directory.SetCurrentDirectory(projectFolder);

/* New Stuff
File.ReadAllLines(path)
File.WriteAllLines(path, lines)
File.AppendAllLines(path, lines)
*/

// Part 1: The Standings
string[] players = File.ReadAllLines(PLAYERS_FILE); // Reads all the stuff from players.txt into the array
string[] points = File.ReadAllLines(POINTS_FILE); // Read all of points.txt into teh array
int leader = LeaderIndex(points); // Uses the method LeaderIndex to find the highest score
int total = TotalPoints(points); // USes the method TotalPoints to get the total of all the points together

System.Console.WriteLine("PLAYER".PadRight(11) + "POINTS");

for (int i = 0; i < players.Length; i++)
{
    System.Console.WriteLine(players[i].PadRight(11) + points[i]);
}

System.Console.WriteLine("Leader: " + players[leader] + " with " + points[leader]);
System.Console.WriteLine("Season total: " + total);

// Part 2: Game Night
int bestTonight = -1;
int bestPlayer = 0;

for (int i = 0; i < players.Length; i++)
{
    System.Console.Write("Points tonight for " + players[i] + ": ");
    int tonight = Convert.ToInt32(Console.ReadLine());

    while (tonight < 0 || tonight > MAX_POINTS)
    {
        System.Console.WriteLine("Points must be from 0 to " + MAX_POINTS + ".");
        System.Console.Write("Points tonight for " + players[i] + ": ");
        tonight = Convert.ToInt32(Console.ReadLine());
    }

    int season = Convert.ToInt32(points[i]) + tonight;
    points[i] = season.ToString();

    if (tonight > bestTonight)
    {
        bestTonight = tonight;
        bestPlayer = i;
    }
}

System.Console.WriteLine("Tonight's winner: " + players[bestPlayer] + " with " + bestTonight);
File.WriteAllLines(POINTS_FILE, points);
System.Console.WriteLine("Saved points.txt.");

// Methods
static int LeaderIndex(string[] points)
{
    int leader = 0;

    for (int i = 1; i < points.Length; i++)
    {
        if (Convert.ToInt32(points[i]) > Convert.ToInt32(points[leader]))
        {
            leader = i;
        }
    }

    return leader;
}

static int TotalPoints(string[] points)
{
    int total = 0;

    foreach (string p in points)
    {
        total += Convert.ToInt32(p);
    }

    return total;
}