using System.Web.Mvc;
using ChessResult.Repository;

namespace ChessResult.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ITournamentRepository _tournamentRepository = new TournamentRepository();

        public ActionResult TournamentDetail(int id)
        {
            var tournament = _tournamentRepository.GetById(id);
            return View(tournament);
        }

        public ActionResult GetListTournament(int id)
        {
            var listTournament = (id == 0) ? _tournamentRepository.GetTournamentInProgress() : _tournamentRepository.GetTournamentInProgressByFederation(id);
            return PartialView(listTournament);
        }

        public ActionResult GetStartingRank(int id)
        {
            var listTourRank = _tournamentRepository.GetTournamentRank(id);
            return PartialView(listTourRank);
        }

        public ActionResult GetListParing(int id)
        {
            var listTourRank = _tournamentRepository.GetTournamentRank(id);
            return PartialView(listTourRank);
        }
    }
}