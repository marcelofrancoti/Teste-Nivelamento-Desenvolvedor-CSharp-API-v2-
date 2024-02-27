using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year).Result;

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year).Result;

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

    }

    public static async Task<int> getTotalScoredGoals(string team, int year)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}";
            int page = 1;
            int totalGoals = 0;

            while (true)
            {
                HttpResponseMessage response = await client.GetAsync($"{url}&page={page}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to get data from API. Status code: {response.StatusCode}");
                }

                string responseData = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(responseData);

                if (page > data["total_pages"].Value<int>())
                {
                    break;
                }

                var matches = ((JArray)data["data"])
                    .Where(match => (match["team1"].Value<string>() == team || match["team2"].Value<string>() == team))
                    .ToList();

                totalGoals += matches.Sum(match =>
                {
                    if (match["team1"].Value<string>() == team)
                    {
                        return match["team1goals"].Value<int>();
                    }
                    else
                    {
                        return match["team2goals"].Value<int>();
                    }
                });

                page++;
            }

            return totalGoals;
        }
    }

}