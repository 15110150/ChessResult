using ChessResult.Models;
using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessResult.Repository
{
    public interface IRoundRepository : IRepository<Round>
    {
        IEnumerable<Round> GetListRoundByTournament(int id);

    }

    public class RoundRepository : IRoundRepository
    {
        public IEnumerable<Round> GetAll()
        {
            throw new NotImplementedException();
        }

        public Round GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Round> GetListRoundByTournament(int id)
        {
            var parameter = new
            {
                TourID = id.ToString(),
            };

            using (IObjectDb db = new ObjectDb("GetListRoundByTournament"))
            {
                return db.Query<Round>(parameter);
            }
        }
    }
}