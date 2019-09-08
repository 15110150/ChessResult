using System.Collections.Generic;
using System.Linq;
using ChessResult.Models;
using Fanex.Data;

namespace ChessResult.Repository
{
    public interface ITournamentRepository : IRepository<Tournament>
    {
        IEnumerable<Tournament> GetTournamentInProgress();

        IEnumerable<Tournament> GetTournamentInProgressByFederation(int id);

        IEnumerable<TourRank> GetTournamentRank(int id);
    }

    public class TournamentRepository : ITournamentRepository
    {
        private readonly IFederationRepository _federationRepository = new FederationRepository();
        private readonly IRoundRepository _roundRepository = new RoundRepository();

        public IEnumerable<Tournament> GetAll()
        {
            using (var objectDb = ObjectDbFactory.CreateInstance("GetAllTournament"))
            {
                return objectDb.Query<Tournament, Federation, Tournament>(
                  (tournament, federation) =>
                  {
                      tournament.Federation = federation;
                      return tournament;
                  },
                  "FederationID");
            }
        }

        public Tournament GetById(int id)
        {
            var parameter = new
            {
                Id = id.ToString(),
            };

            using (var objectDb = ObjectDbFactory.CreateInstance("GetSingle"))
            {
                var tournament = objectDb.Query<Tournament, Federation, Tournament>(
                  (tournaments, federation) =>
                  {
                      tournaments.Federation = federation;
                      return tournaments;
                  },
                  "FederationID",
                  parameter).FirstOrDefault();

                tournament.FederationPaticipate = _federationRepository.GetFederationInTournament(id);
                tournament.Rounds = _roundRepository.GetListRoundByTournament(id);

                return tournament;
            }
        }

        public IEnumerable<Tournament> GetTournamentInProgress()
        {
            using (var objectDb = ObjectDbFactory.CreateInstance("GetTournamentInProgress"))
            {
                var x = objectDb.Query<Tournament, Federation, Tournament>(
                  (tournament, federation) =>
                  {
                      tournament.Federation = federation;
                      return tournament;
                  },
                  "FederationID");
                return x;
            }
        }

        public IEnumerable<Tournament> GetTournamentInProgressByFederation(int id)
        {
            var parameter = new
            {
                FederationId = id.ToString(),
            };

            using (var objectDb = ObjectDbFactory.CreateInstance("GetTournamentInProgressByFederation"))
            {
                return objectDb.Query<Tournament, Federation, Tournament>(
                  (tournament, federation) =>
                  {
                      tournament.Federation = federation;
                      return tournament;
                  },
                  "FederationID",
                  parameter);
            }
        }

        public IEnumerable<TourRank> GetTournamentRank(int id)
        {
            var parameter = new
            {
                TourID = id.ToString(),
            };

            using (var objectDb = ObjectDbFactory.CreateInstance("GetStartingRank"))
            {
                var tourRanks = objectDb.Query<TourRank>(parameter).OrderByDescending(x => x.TotalMark);

                return tourRanks;
            }
        }
    }
}