using Client.Infrastructure.Reactive;
using Microsoft.AspNetCore.Components;
using ReactiveUI.Blazor;

namespace Client.Infrastructure.Components
{
    public class MyComponentBase<T> : ReactiveComponentBase<T> where T : ViewModelBase
    {
        [Inject]
        private T _viewModel
        {
            set => ViewModel = value;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ViewModel.Dispose();
            }
        }
    }
}