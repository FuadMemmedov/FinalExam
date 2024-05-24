using Core.Models;
using Core.RepositoryAbstracts;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Data.RepositoryConcretes;

public class TeamRepository : GenericRepository<Team>, ITeamRepository
{
    private readonly AppDbContext _appDbContext;

    public TeamRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IPagedList<Team>> GetPagedTeamsAsync(int pageIndex,int pageSize)
    {
        var query = _appDbContext.Teams.AsQueryable();
        return await query.ToPagedListAsync(pageIndex, pageSize);
    }
}
