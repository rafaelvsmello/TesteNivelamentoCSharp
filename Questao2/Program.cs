using Newtonsoft.Json;
using Questao2;
using System.Net;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        //Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        string url;
        int totalPages = GetTotalPages(team, year);
        int totalGolsHome = 0;
        int totalGolsAway = 0;

        for (int i = 1; i <= totalPages; i++)
        {
            int page = i;

            url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={page}";
            totalGolsHome += GetGoalsHome(url, team);

            url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team2={team}&page={page}";
            totalGolsAway += GetGoalsAway(url, team);
        }

        return totalGolsHome + totalGolsAway;
    }

    private static int GetTotalPages(string team, int year)
    {
        string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}";

        var request = WebRequest.CreateHttp(url);
        request.Method = "GET";
        request.UserAgent = "Default";

        using (var response = request.GetResponse())
        {
            var sd = response.GetResponseStream();
            var sr = new StreamReader(sd);
            var obj = sr.ReadToEnd();

            var result = JsonConvert.DeserializeObject<Application>(obj.ToString());
            return result.total_pages;
        }
    }

    private static int GetGoalsHome(string url, string team)
    {
        int gols = 0;
        var request = WebRequest.CreateHttp(url);
        request.Method = "GET";
        request.UserAgent = "Default";

        using (var response = request.GetResponse())
        {
            var sd = response.GetResponseStream();
            var sr = new StreamReader(sd);
            var obj = sr.ReadToEnd();

            var result = JsonConvert.DeserializeObject<Application>(obj.ToString());

            foreach (var d in result.data)
            {
                if (d.team1.Equals(team))
                {
                    gols += int.Parse(d.team1goals);
                }
            }
        }

        return gols;
    }

    private static int GetGoalsAway(string url, string team)
    {
        int gols = 0;
        var request = WebRequest.CreateHttp(url);
        request.Method = "GET";
        request.UserAgent = "Default";

        using (var response = request.GetResponse())
        {
            var sd = response.GetResponseStream();
            var sr = new StreamReader(sd);
            var obj = sr.ReadToEnd();

            var result = JsonConvert.DeserializeObject<Application>(obj.ToString());

            foreach (var d in result.data)
            {
                if (d.team2.Equals(team))
                {
                    gols += int.Parse(d.team2goals);
                }
            }
        }

        return gols;
    }

} 