using ReactiveUI;
using System;

namespace Client.Infrastructure.Reactive
{
    public class ViewModelBase : ReactiveObject, IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}