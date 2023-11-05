using Borrow.Data.Repositories.Interfaces;
using Borrow.Models.Backend;

namespace Borrow.Data.Repositories.Implementations
{
    public class NeighborhoodRepository : IRepository<Neighborhood>
    {
        public IQueryable<Neighborhood> Query => throw new NotImplementedException();

        public void Add(Neighborhood entity)
        {
            throw new NotImplementedException();
        }

        public void Add(List<Neighborhood> entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Neighborhood entity)
        {
            throw new NotImplementedException();
        }

        public List<Neighborhood> FetchAll()
        {
            throw new NotImplementedException();
        }

        public Neighborhood GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
