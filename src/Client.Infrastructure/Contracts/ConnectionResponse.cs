using ReactiveUI.Fody.Helpers;
using System;

namespace Client.Infrastructure.Contracts
{
    public class ConnectionResponse
    {
        public int Id { get; set; }

        [Reactive]
        public string Country { get; set; }

        [Reactive]
        public DateTime CreatedAt { get; set; }

        [Reactive]
        public DateTime UpdatedAt { get; set; }

        [Reactive]
        public int UpdatedNumberTimes { get; set; }
    }
}