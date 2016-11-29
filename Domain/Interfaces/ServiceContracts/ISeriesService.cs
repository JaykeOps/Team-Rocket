using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Domain.Entities;
using Domain.Value_Objects;

namespace Domain.Services
{
    [ServiceContract]
    public interface ISeriesService
    {
        [OperationContract]
        void Add(Series series);

        [OperationContract]
        void AddTeamToSeries(Guid seriesId, Guid teamId);

        [OperationContract]
        void DeleteSeries(Guid seriesId);

        [OperationContract]
        Series FindById(Guid seriesId);

        [OperationContract]
        IEnumerable<Series> GetAll();

        [OperationContract]
        IOrderedEnumerable<TeamStats> GetLeagueTablePlacement(Guid seriesId);

        [OperationContract]
        void RemoveTeamFromSeries(Guid seriesId, Guid teamId);

        [OperationContract]
        void ScheduleGenerator(Guid seriesId);
        [OperationContract]
        IEnumerable<Series> Search(string searchText, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase);
    }
}