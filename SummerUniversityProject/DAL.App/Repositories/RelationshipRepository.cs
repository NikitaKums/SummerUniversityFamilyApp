using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.Repositories;
using Domain;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.Repositories
{
    public class RelationshipRepository : BaseRepository<Relationship>, IRelationshipRepository
    {
        public RelationshipRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<RelationshipDTO>> AllAsyncApi()
        {
            return await RepositoryDbSet
                .Select(e => new RelationshipDTO()
                {
                    Id = e.Id,
                    Relation = e.Relation.ToString()
                })
                .ToListAsync();
        }

        public async Task<int> FindIdByString(Relation relation)
        {
            return await RepositoryDbSet
                .Where(w => w.Relation == relation)
                .Select(e => e.Id).FirstOrDefaultAsync();
        }
    }
}