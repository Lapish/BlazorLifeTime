using Client.Infrastructure.Contracts;
using Client.Infrastructure.Reactive;
using Client.Infrastructure.Services;
using DynamicData;
using System;
using System.Collections.ObjectModel;

namespace Client.ViewModels.Profile
{
    public class SecondTabViewModel : ViewModelBase
    {
        private IDisposable _cleanUp;
        private readonly ReadOnlyObservableCollection<ConnectionResponse> _connections;

        public ReadOnlyObservableCollection<ConnectionResponse> Connections => _connections;
        public event Action ConnectionChanged;

        public SecondTabViewModel(IProfileService profileService)
        {
            _cleanUp = profileService
                .Connections
                .Bind(out _connections)
                .DisposeMany()
                .Subscribe(x => ConnectionChanged?.Invoke());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cleanUp?.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}