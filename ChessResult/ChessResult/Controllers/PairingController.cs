using ChessResult.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChessResult.Controllers
{
    public class PairingController : Controller
    {
        private readonly IPairingRepository _pairingRepository = new PairingRepository();

        public ActionResult ListPairing(int tourID, int roundID)
        {
            var listPairing = _pairingRepository.GetListParingByTourAndRound(tourID, roundID);
            return PartialView(listPairing);
        }
    }
}