using Business.Exceptions;
using Business.Extensions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Business.Services.Concretes;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _repository;
    private readonly IWebHostEnvironment _env;

    public TeamService(ITeamRepository repository, IWebHostEnvironment env)
    {
        _repository = repository;
        _env = env;
    }

    public async Task AddTeam(Team team)
    {
        if (team.ImageFile == null) throw new FileRequiredException("Image is required");

        team.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\teams", team.ImageFile, "team");

        await _repository.AddEntityAsync(team);
        await _repository.CommitAsync();
    }

    public void DeleteTeam(int id)
    {
       var existTeam = _repository.GetEntity(x => x.Id == id);
       if (existTeam == null) throw new EntityNotFoundException("Team tapilmadi!");

        Helper.DeleteFile(_env.WebRootPath, @"uploads\teams", existTeam.ImageUrl);

        _repository.DeleteEntity(existTeam);
        _repository.Commit();

    }

    public List<Team> GetAllTeams(Func<Team, bool>? func = null)
    {
        return _repository.GetAllEntities(func);
    }

    public async Task<IPagedList<Team>> GetPagedTeamsAsync(int pageIndex, int pageSize)
    {
        return await _repository.GetPagedTeamsAsync(pageIndex, pageSize);
    }

    public Team GetTeam(Func<Team, bool>? func = null)
    {
        return _repository.GetEntity(func);
    }

    public void UpdateTeam(Team team)
    {
        var oldTeam = _repository.GetEntity(x => x.Id == team.Id);
        if (oldTeam == null) throw new EntityNotFoundException("Team tapilmadi!");

        if(team.ImageFile != null)
        {
            team.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\teams", team.ImageFile, "team");
            Helper.DeleteFile(_env.WebRootPath, @"uploads\teams", oldTeam.ImageUrl);

            oldTeam.ImageUrl = team.ImageUrl;


        }

        oldTeam.FullName = team.FullName;
        oldTeam.Designation = team.Designation;
        oldTeam.Description = team.Description;
        oldTeam.TwittlerLink = team.TwittlerLink;
        oldTeam.FacebookLink = team.FacebookLink;
        oldTeam.InstagramLink = team.InstagramLink;
        oldTeam.LinkedinLink = team.LinkedinLink;

        _repository.Commit();
    }
}
