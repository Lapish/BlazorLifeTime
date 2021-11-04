using Client.Infrastructure.Reactive;
using ReactiveUI.Fody.Helpers;
using System;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        [Reactive]
        public int Number { get; private set; }

        public DateTime CreatedAt { get; }

        public MainViewModel()
        {
            CreatedAt = DateTime.Now;

            Task.Run(async () =>
            {
                Random rand = new ();
                while (true)
                {
                    await Task.Delay(rand.Next(50, 500));
                    Number++;
                }
            });
        }
    }
}