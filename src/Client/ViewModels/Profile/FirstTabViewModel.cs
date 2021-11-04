using Client.Infrastructure.Reactive;
using Client.Infrastructure.Services;
using ReactiveUI.Fody.Helpers;
using System;

namespace Client.ViewModels.Profile
{
    public class FirstTabViewModel : ViewModelBase
    {
        private readonly IDisposable _cleanUp;

        [Reactive]
        public string DisplayName { get; private set; }

        public FirstTabViewModel(IProfileService profileService)
        {
            _cleanUp = profileService
                .DisplayName
                .Subscribe(x => DisplayName = x);
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