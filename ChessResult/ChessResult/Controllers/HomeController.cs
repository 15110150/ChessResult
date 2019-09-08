using System.Web.Mvc;
using ChessResult.Repository;

namespace ChessResult.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITournamentRepository _tournamentRepository = new TournamentRepository();

        public ActionResult Index()
        {
            var listTournament = _tournamentRepository.GetTournamentInProgress();
            return View(listTournament);
        }
    }
}