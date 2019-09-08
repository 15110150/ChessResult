using System.Collections.Generic;
using ChessResult.Models;
using Fanex.Data;

namespace ChessResult.Repository
{
    public interface IPlayerRepository : IRepository<Player>
    {
    }

    public class PlayerRepository : IPlayerRepository
    {
        public void Add(Player entity)
        {
            using (IObjectDb db = new ObjectDb("CreatePlayer"))
            {
                var param = new
                {
                    Name = entity.Name,
                    BirthDate = entity.BirthDate,
                    Image = entity.Image,
                    Sex = entity.Sex,
                    FederationID = entity.FederationID,
                };

                db.ExecuteNonQuery(param);
            }
        }

        public IEnumerable<Player> GetAll()
        {
            using (IObjectDb db = new ObjectDb("GetAllPlayer"))
            {
                return db.Query<Player>();
            }
        }

        public Player GetById(int id)
        {
            using (IObjectDb db = new ObjectDb("GetPlayer"))
            {
                var player = db.QueryEntity<Player>(id);
                return player;
            }
        }
    }
}