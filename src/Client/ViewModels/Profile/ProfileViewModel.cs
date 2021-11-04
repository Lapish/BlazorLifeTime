using Client.Infrastructure.Reactive;
using Client.Infrastructure.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive;
using System.Reactive.Disposables;

namespace Client.ViewModels.Profile
{
    public class ProfileViewModel : ViewModelBase
    {
        private readonly IDisposable _cleanUp;
        private readonly IProfileService _profileService;
        private ObservableAsPropertyHelper<bool> _isBusy;

        public DateTime ViewCreatedAt { get; private set; }
        public DateTime ServiceCreatedAt { get; private set; }

        [Reactive]
        public DateTime ServiceUpdatedAt { get; private set; }

        public bool IsBusy => _isBusy.Value;
        public ReactiveCommand<Unit, Unit> LoadProfileCommand { get; }
        public ReactiveCommand<Unit, Unit> StartFakeUpdateCommand { get; }

        public ProfileViewModel(IProfileService profileService)
        {
            _profileService = profileService;

            ViewCreatedAt = DateTime.Now;

            LoadProfileCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                await _profileService.LoadProfileAsync();
            });

            StartFakeUpdateCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                await _profileService.StartFakeUpdateAsync();
            }, null, RxApp.MainThreadScheduler);

            LoadProfileCommand
                .IsExecuting
                .ToProperty(this, x => x.IsBusy, out _isBusy, scheduler: RxApp.MainThreadScheduler);

            var serviceCreatedAt = profileService
                .CreatedAt
                .Subscribe(x => ServiceCreatedAt = x);

            var serviceUpdatedAt = profileService
                .UpdatedAt
                .Subscribe(x => ServiceUpdatedAt = x);

            _cleanUp = new CompositeDisposable(serviceCreatedAt, serviceUpdatedAt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cleanUp?.Dispose();
                _profileService.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}