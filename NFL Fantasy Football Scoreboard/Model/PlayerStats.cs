using System.Collections.Generic;

namespace NFLFantasyFootballScoreboard.Model
{
    /// <summary>The outer object from the NFL Fantasy API</summary>
    public class FantasyStats
    {
        public string week { get; set; }
        public List<Player> players { get; set; }
    }

    /// <summary>The player object from the NFL Fantasy API</summary>
    public class Player
    {
        public string id { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public string teamAbbr { get; set; }
        public double weekPts { get; set; }
    }

    /// <summary>Response to the client containing the player data for both teams</summary>
    public class Response
    {
        public Response() { }

        public Response(List<Player> myPlayers, List<Player> oppPlayers)
        {
            this.myPlayers = myPlayers;
            this.oppPlayers = oppPlayers;
        }

        public List<Player> myPlayers { get; set; }
        public List<Player> oppPlayers { get; set; }
    }
}
