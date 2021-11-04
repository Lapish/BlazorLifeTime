using Client.Infrastructure.Reactive;
using ReactiveUI.Fody.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private CancellationTokenSource _cts;
        [Reactive]
        public int Number { get; private set; }

        public DateTime CreatedAt { get; }

        public MainViewModel()
        {
            CreatedAt = DateTime.Now;
            _cts = new CancellationTokenSource();

            Task.Run(async () =>
            {
                Random rand = new ();
                while (!_cts.IsCancellationRequested)
                {
                    await Task.Delay(rand.Next(50, 500));
                    Number++;
                }
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cts.Cancel();
                GC.SuppressFinalize(this);
            }
        }
    }
}