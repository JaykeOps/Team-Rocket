using System;
using System.Collections.Generic;
using System.ServiceModel;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Value_Objects;

namespace Domain.Services
{
    [ServiceContract]
    public interface ITeamService
    {
        [OperationContract]
        void Add(Team team);

        [OperationContract]
        void Add(IExposableTeam team);

        [OperationContract]
        void Add(IEnumerable<Team> teams);

        [OperationContract]
        void Add(IEnumerable<IExposableTeam> teams);

        [OperationContract]
        Team FindById(Guid teamId);

        [OperationContract]
        IEnumerable<Team> GetAll();

        [OperationContract]
        TeamEvents GetTeamEventsInSeries(Guid teamId, Guid seriesId);

        [OperationContract]
        IEnumerable<Match> GetTeamSchedule(Guid teamId, Guid seriesId);

        [OperationContract]
        IEnumerable<Team> GetTeamsOfSerie(Guid sereisId);

        [OperationContract]
        TeamStats GetTeamStatsInSeries(Guid seriesId, Guid teamId);

        [OperationContract]
        TeamStats GetTeamStatsInseries(Guid teamId, Guid seriesId);

        [OperationContract]
        IEnumerable<Team> Search(string searchText, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase);
    }
}