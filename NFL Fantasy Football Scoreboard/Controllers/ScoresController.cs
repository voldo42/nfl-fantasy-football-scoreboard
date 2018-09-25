using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NFLFantasyFootballScoreboard.Model;
using NFLFantasyFootballScoreboard.Settings;

namespace NFLFantasyFootballScoreboard.Controllers
{
    [Route("[controller]")]
    public class ScoresController : Controller
    {
        private readonly APISettings _apiSettings;
        private readonly MyTeamSettings _myTeamSettings;
        private readonly OppTeamSettings _oppTeamSettings;

        public ScoresController(IOptions<APISettings> apiSettings, IOptions<MyTeamSettings> myTeamSettings, IOptions<OppTeamSettings> oppTeamSettings)
        {
            _apiSettings = apiSettings.Value;
            _myTeamSettings = myTeamSettings.Value;
            _oppTeamSettings = oppTeamSettings.Value;
        }

        // GET scores/5
        [HttpGet("{week}")]
        public Response Get(int week)
        {

            return NFLFantasyAPIHelper.GetPlayerScores(
                _apiSettings.NFLFantasyAPIURL, 
                _myTeamSettings,
                _oppTeamSettings,
                week);
        }
    }
}
