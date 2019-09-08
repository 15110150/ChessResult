using System.Collections.Generic;

namespace ChessResult.Repository
{
    public interface IRepository<out T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);
    }
}