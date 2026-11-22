/*
 *  Name:       Justin Williams
 *  Course:     CSCI 1250, Section 002
 *  Assignment: Lab 08, Game Night
 *  Date:       November 18, 2026
 *  Description: Keeps a game night leaderboard in three text files. Shows the season so far, records tonight's points, saves the new totals, and adds tonight's result to a history that grows every week.
*/

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


// Part 3's leaderboard stuff
Console.WriteLine("=== GAME NIGHT LEADERBOARD ===");
string[] history = File.ReadAllLines(HISTORY_FILE);

if (history.Length == 0)
{
    Console.WriteLine("No nights played yet.");
}
else
{
    Console.WriteLine("Nights played: " + history.Length);
    // foreach: each previous night is just a string from the history.txt file, so it doesn't need to be fancy like a while loop, but still needs to go through each value in the history file.
    foreach (string line in history)
    {
        Console.WriteLine("  " + line);
    }
}

int tonightNumber = history.Length + 1;
Console.WriteLine("Tonight is Night " + tonightNumber + ".\n");

// Part 1: The Standings
string[] players = File.ReadAllLines(PLAYERS_FILE); // Reads all the stuff from players.txt into the array
string[] points = File.ReadAllLines(POINTS_FILE); // Read all of points.txt into teh array
int leader = LeaderIndex(points); // Uses the method LeaderIndex to find the highest score
int total = TotalPoints(points); // USes the method TotalPoints to get the total of all the points together

System.Console.WriteLine("PLAYER".PadRight(11) + "POINTS");

// for: the name and the points live in two- yeah its the same thing that you said in the lab document :D
// for: okay but ACTUALLY for has its own index that both players and points uses to make sure it iterates through each value, and since there's the same amount of players and points, it goes through them all.
for (int i = 0; i < players.Length; i++)
{
    System.Console.WriteLine(players[i].PadRight(11) + points[i]);
}

System.Console.WriteLine("Leader: " + players[leader] + " with " + points[leader]);
System.Console.WriteLine("Season total: " + total + "\n");

// Part 2: Game Night
int bestTonight = -1;
int bestPlayer = 0;

// for: the arrays line up again, but i also need the index to keep track of the position of who's got the highest score. foreach might work (i genuinely dont remember) for getting an index but for made more sense because of the syntax
// second comment on this: i'd probably need to make an index manually inside of it if it was a foreach loop, i think, which would be confusing af to use here
for (int i = 0; i < players.Length; i++)
{
    System.Console.Write("Points tonight for " + players[i] + ": ");
    int tonight = Convert.ToInt32(Console.ReadLine());

    // while: don't know how many times a person types in a bad number, so it needs a conditional loop instead of a counted loop
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
System.Console.WriteLine("\nSaved points.txt.");

// Part 3: The History
string winnerTonight = players[bestPlayer];
string[] entry = { HistoryEntry(tonightNumber, winnerTonight, bestTonight) };
File.AppendAllLines(HISTORY_FILE, entry);
Console.WriteLine("Added to history.txt: " + entry[0]);

// Methods
static int LeaderIndex(string[] points)
{
    int leader = 0;

    // for: again, we need the index, so a while loop doesn't work, and (if my above comment is right) id need to manually make an index inside of a foreach, since i dont think there's a way to get an index from a foreach by itself
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

    // We don't need an index in this case, as we're getting the actual points amount, not the position where its stored.
    foreach (string p in points)
    {
        total += Convert.ToInt32(p);
    }

    return total;
}

static string HistoryEntry(int night, string winner, int points)
{
    return "Night " + night + ": " + winner + " won with " + points + " points";
}