using Client.Infrastructure.Contracts;
using DynamicData;
using System;
using System.Threading.Tasks;

namespace Client.Infrastructure.Services
{
    public interface IProfileService : IDisposable
    {
        IObservable<DateTime> CreatedAt { get; }
        IObservable<DateTime> UpdatedAt { get; }
        IObservable<string> DisplayName { get; }
        IObservable<IChangeSet<ConnectionResponse, int>> Connections { get; }

        Task LoadProfileAsync();
        Task StartFakeUpdateAsync();
    }
}