using System;
using System.Collections.Generic;
using System.ServiceModel;
using Domain.Entities;

namespace Domain.Services
{
    [ServiceContract]
    public interface IMatchService
    {
        [OperationContract(Name = "AddMatch")]
        void Add(Match match);

        [OperationContract(Name = "AddMatchFromList")]
        void Add(IEnumerable<Match> matches);

        [OperationContract]
        void EditMatchLocation(string newArenaName, Guid matchId);

        [OperationContract]
        void EditMatchTime(DateTime dateTime, Guid matchId);

        [OperationContract]
        Match FindById(Guid id);

        [OperationContract]
        IEnumerable<Match> GetAll();
        [OperationContract]
        IEnumerable<Match> Search(string searchText, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase);

    }
}