using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using NFLFantasyFootballScoreboard.Model;
using NFLFantasyFootballScoreboard.Settings;

namespace NFLFantasyFootballScoreboard
{
    public static class NFLFantasyAPIHelper
    {
        /// <summary>Gets data from the NFL Fantasy API and returns a response to the
        /// client containing the fantasy team and opposition team</summary>
        /// <returns>The response to the client</returns>
        public static Response GetPlayerScores(string url, MyTeamSettings myTeamSettings, OppTeamSettings oppTeamSettings)
        {
            // get all player stats from NFL Fantasy API
            string data = GetDataFromNFL(url);
            FantasyStats stats = JsonConvert.DeserializeObject<FantasyStats>(data);

            // get players from settings
            (List<string> myPlayers, List<string> oppPlayers) = GetPlayersFromSettings(myTeamSettings, oppTeamSettings);

            // return both fantasy teams to the client
            return CreateResponse(myPlayers, oppPlayers, stats);
        }

        private static string GetDataFromNFL(string url)
        {
            return new WebClient().DownloadString(url);
        }

        private static (List<string> myPlayers, List<string> oppPlayers) GetPlayersFromSettings(MyTeamSettings myTeamSettings, OppTeamSettings oppTeamSettings)
        {
            return (new List<string>()
                {
                    myTeamSettings.QB,
                    myTeamSettings.RB1,
                    myTeamSettings.RB2,
                    myTeamSettings.WR1,
                    myTeamSettings.WR2,
                    myTeamSettings.TE,
                    myTeamSettings.FLEX,
                    myTeamSettings.K,
                    myTeamSettings.DEF,
                },
                new List<string>()
                {
                    oppTeamSettings.QB,
                    oppTeamSettings.RB1,
                    oppTeamSettings.RB2,
                    oppTeamSettings.WR1,
                    oppTeamSettings.WR2,
                    oppTeamSettings.TE,
                    oppTeamSettings.FLEX,
                    oppTeamSettings.K,
                    oppTeamSettings.DEF,
                });
        }

        /// <summary>Searches in the NFL Fantasy data for the details of the players in the request</summary>
        /// <param name="myPlayerNames">The lists of my players</param>
        /// <param name="oppPlayerNames">The lists of my opponent's players</param>
        /// <param name="stats">The data returned from the NFL Fantasy API</param>
        /// <returns>The response to the client</returns>
        private static Response CreateResponse(List<string> myPlayerNames, List<string> oppPlayerNames, FantasyStats stats)
        {
            List<Player> myPlayers = new List<Player>();
            List<Player> oppPlayers = new List<Player>();

            foreach (string player in myPlayerNames)
            {
                Player p = stats.players.FirstOrDefault(m => m.name.Equals(player));

                // If the player cannot be found, a null object will cause scoreboard to fail
                // Create unknown player
                if (p == null)
                {
                    Player unknown = new Player
                    {
                        name = player,
                        weekPts = 0
                    };

                    myPlayers.Add(unknown);
                }
                else
                {
                    myPlayers.Add(p);
                }
            }

            foreach (string player in oppPlayerNames)
            {
                Player p = stats.players.FirstOrDefault(m => m.name.Equals(player));

                // If the player cannot be found, a null object will cause scoreboard to fail
                // Create unknown player
                if (p == null)
                {
                    Player unknown = new Player
                    {
                        name = player,
                        weekPts = 0
                    };

                    oppPlayers.Add(unknown);
                }
                else
                {
                    oppPlayers.Add(p);
                }
            }

            return new Response(myPlayers, oppPlayers);
        }
    }
}
