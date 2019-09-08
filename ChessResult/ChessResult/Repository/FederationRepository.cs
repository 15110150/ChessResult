using System.Collections.Generic;
using System.Linq;
using ChessResult.Models;
using Fanex.Data;

namespace ChessResult.Repository
{
    public interface IFederationRepository : IRepository<Federation>
    {
        IEnumerable<Federation> GetFederationInTournament(int id);
    }

    public class FederationRepository : IFederationRepository
    {
        public IEnumerable<Federation> GetAll()
        {
            using (var objectDb = new ObjectDb("GetAllFederation"))
            {
                return objectDb.Query<Federation>().OrderBy(x => x.Name);
            }
        }

        public Federation GetById(int id)
        {
            var parameter = new
            {
                TourId = id.ToString(),
            };

            using (IObjectDb db = new ObjectDb("GetSingle"))
            {
                return db.Query<Federation>(parameter).FirstOrDefault();
            }
        }

        public IEnumerable<Federation> GetFederationInTournament(int id)
        {
            var parameter = new
            {
                TourId = id.ToString(),
            };

            using (IObjectDb db = new ObjectDb("GetFederationParticipate"))
            {
                return db.Query<Federation>(parameter);
            }
        }
    }
}