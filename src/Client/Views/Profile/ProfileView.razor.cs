using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Views.Profile
{
    public partial class ProfileView
    {
        private CancellationTokenSource _cts;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await ViewModel.LoadProfileCommand.Execute().ToTask();
                _cts = new CancellationTokenSource();
                Task.Run(async () =>
                {
                    while (!_cts.IsCancellationRequested)
                    {
                        await ViewModel.StartFakeUpdateCommand.Execute().ToTask();
                    }
                });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cts.Cancel();
                ViewModel.Dispose();
            }
        }
    }
}