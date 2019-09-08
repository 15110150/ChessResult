using System.Web.Mvc;
using ChessResult.Repository;

namespace ChessResult.Controllers
{
    public class FederationController : Controller
    {
        private readonly IFederationRepository _federationRepository = new FederationRepository();

        [ChildActionOnly]
        public ActionResult ListFederation()
        {
            var federations = _federationRepository.GetAll();
            return PartialView(federations);
        }
    }
}