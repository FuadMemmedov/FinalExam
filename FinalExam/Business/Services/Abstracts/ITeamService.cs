using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Business.Services.Abstracts;

public interface ITeamService
{
    Task AddTeam(Team team);
    void DeleteTeam(int id);
    void UpdateTeam(Team team);
    Team GetTeam(Func<Team, bool>? func = null);
    List<Team> GetAllTeams(Func<Team, bool>? func = null);
    Task<IPagedList<Team>> GetPagedTeamsAsync(int pageIndex, int pageSize);
}
