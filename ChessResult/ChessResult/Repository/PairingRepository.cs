using ChessResult.DTO;
using ChessResult.Models;
using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessResult.Repository
{
    public interface IPairingRepository : IRepository<Pairing>
    {
        IEnumerable<PairingDTO> GetListParingByTourAndRound(int tourID, int roundID);
    }

    public class PairingRepository : IPairingRepository
    {
        public IEnumerable<Pairing> GetAll()
        {
            throw new NotImplementedException();
        }

        public Pairing GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PairingDTO> GetListParingByTourAndRound(int tourID, int roundID)
        {
            var parameter = new
            {
                RoundID = roundID.ToString(),
                TourID = tourID.ToString()
            };

            using (var objectDb = ObjectDbFactory.CreateInstance("GetListPairing"))
            {
                var y = objectDb.Query<PairingDTO>(parameter);
                return y;
            }
        }
    }
}