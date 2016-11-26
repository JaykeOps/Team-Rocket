using System;
using System.Collections.Generic;
using System.ServiceModel;
using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Services
{
    [ServiceContract]
    public interface IPlayerService
    {
        [OperationContract]
        void Add(IExposablePlayer player);
        [OperationContract]
        void Add(IEnumerable<Player> players);
        [OperationContract]
        Player FindById(Guid playerId);
        [OperationContract]
        IEnumerable<IExposablePlayer> FreeTextSearchForPlayers(string searchText, StringComparison comp);
        [OperationContract]
        IEnumerable<Player> GetAllExposablePlayers();
        [OperationContract]
        IEnumerable<Player> GetAllPlayers();
        [OperationContract]
        IEnumerable<PlayerStats> GetTopAssistsForSeries(Guid seriesId);
        [OperationContract]
        IEnumerable<PlayerStats> GetTopRedCardsForSeries(Guid seriesId);
        [OperationContract]
        IEnumerable<PlayerStats> GetTopScorersForSeries(Guid seriesId);
        [OperationContract]
        IEnumerable<PlayerStats> GetTopYellowCardsForSeries(Guid seriesId);
        [OperationContract]
        void SetShirtNumber(Guid playerId, ShirtNumber newShirtNumber);
    }
}