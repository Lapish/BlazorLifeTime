using Client.Infrastructure.Contracts;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Client.Infrastructure.Services
{
    public class ProfileService : IProfileService
    {
        #region Fields

        private readonly ISubject<DateTime> _createdAt;
        private readonly ISubject<DateTime> _updatedAt;
        private readonly ISubject<string> _displayName;
        private readonly SourceCache<ConnectionResponse, int> _connections;
        private readonly Random _rand;
        private readonly string[] _countries = new[]
        {
            "Russia", "Andorra", "USA", "Germany", "Belgium"
        };

        #endregion

        public IObservable<DateTime> CreatedAt => _createdAt.AsObservable();
        public IObservable<DateTime> UpdatedAt => _updatedAt.AsObservable();
        public IObservable<string> DisplayName => _displayName.AsObservable();
        public IObservable<IChangeSet<ConnectionResponse, int>> Connections => _connections.Connect();

        public ProfileService()
        {
            _createdAt = new ReplaySubject<DateTime>(1);
            _updatedAt = new ReplaySubject<DateTime>(1);
            _displayName = new ReplaySubject<string>(1);
            _connections = new (x => x.Id);
            _rand = new Random();

            _createdAt.OnNext(DateTime.Now);
        }

        public async Task LoadProfileAsync()
        {
            //Fake delay
            await Task.Delay(_rand.Next(500, 3000));
            List<ConnectionResponse> connections = new ();

            string name = $"XD-{DateTime.Now.Millisecond}";
            _displayName.OnNext(name);

            for(int i = 0; i < _rand.Next(5, 10); i++)
            {
                connections.Add(new ConnectionResponse
                {
                    Id = _rand.Next(1, 100),
                    Country = _countries[_rand.Next(_countries.Length)],
                    CreatedAt = DateTime.Now                    
                });
            }

            _connections.Edit(x =>
            {
                x.Clear();
                x.AddOrUpdate(connections);
            });

            _updatedAt.OnNext(DateTime.Now);
        }

        public async Task StartFakeUpdateAsync()
        {
            if (_connections.Count == 0)
            {
                return;
            }

            int countFakeUpdates = _rand.Next(_connections.Count);
            List<int> ids = _connections.Items.Select(x => x.Id).ToList();
            for(int i = 0; i < countFakeUpdates; i++)
            {
                int id = ids[_rand.Next(ids.Count)];
                ConnectionResponse connection = _connections.Items.First(x => x.Id == id);
                connection.Country = _countries[_rand.Next(_countries.Length)];
                connection.UpdatedAt = DateTime.Now;
                connection.UpdatedNumberTimes++;
                _connections.AddOrUpdate(connection);
                _updatedAt.OnNext(DateTime.Now);
                await Task.Delay(_rand.Next(100, 500));
            }
        }

        public void Dispose()
        {
            _createdAt.OnCompleted();
            _updatedAt.OnCompleted();
            _displayName.OnCompleted();
            _connections.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}