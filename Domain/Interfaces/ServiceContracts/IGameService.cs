using System;
using System.Collections.Generic;
using System.ServiceModel;
using Domain.Entities;

namespace Domain.Services
{
    [ServiceContract]
    public interface IGameService
    {
        [OperationContract]
        void Add(IEnumerable<Game> games);
        [OperationContract]
        Guid Add(Guid matchId);
        [OperationContract]
        void Add(Game game);
        [OperationContract]
        void AddAssistToGame(Guid gameId, Guid playerId, int matchMinute);
        [OperationContract]
        void AddGoalToGame(Guid gameId, Guid playerId, int matchMinute);
        [OperationContract]
        IEnumerable<Guid> AddList(IEnumerable<Guid> matchIds);
        [OperationContract]
        void AddPenaltyToGame(Guid gameId, Guid playerId, int matchMinute, bool isGoal);
        [OperationContract]
        void AddRedCardToGame(Guid gameId, Guid playerId, int matchMinute);
        [OperationContract]
        void AddYellowCardToGame(Guid gameId, Guid playerId, int matchMinute);
        [OperationContract]
        Game FindById(Guid id);
        [OperationContract]
        IEnumerable<Game> GetAll();
        [OperationContract]
        void RemoveAssistFromGame(Guid gameId, Guid playerId, int matchMinute);
        [OperationContract]
        void RemoveGoalFromGame(Guid gameId, Guid playerId, int matchMinute);
        [OperationContract]
        void RemovePenaltyFromGame(Guid gameId, Guid playerId, int matchMinute);
        [OperationContract]
        void RemoveRedCardFromGame(Guid gameId, Guid playerId, int matchMinute);
        [OperationContract]
        void RemoveYellowCardFromGame(Guid gameId, Guid playerId, int matchMinute);
        [OperationContract]
        IEnumerable<Game> Search(string searchText, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase);
    }
}