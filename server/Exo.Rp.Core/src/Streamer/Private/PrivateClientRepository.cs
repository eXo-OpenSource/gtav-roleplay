using System;
using System.Collections.Generic;
using AltV.Net.EntitySync;

namespace Exo.Rp.Core.Streamer.Private
{
    public class PrivateClientRepository : IClientRepository
    {
        private readonly IDictionary<string, IClient> clients = new Dictionary<string, IClient>();

        private readonly PrivateClientThreadRepository[] clientThreadRepositories;

        public PrivateClientRepository(PrivateClientThreadRepository[] clientThreadRepositories)
        {
            this.clientThreadRepositories = clientThreadRepositories;
        }

        public void Add(IClient client)
        {
            if (client == null)
            {
                throw new ArgumentException("Client must not be null.");
            }
            lock (clients)
            {
                foreach (var clientThreadRepository in clientThreadRepositories)
                {
                    clientThreadRepository.Add(client);
                }

                clients[client.Token] = client;
            }
        }

        public IClient Remove(IClient client)
        {
            if (client == null)
            {
                throw new ArgumentException("Client must not be null.");
            }
            return Remove(client.Token);
        }

        public IClient Remove(string token)
        {
            lock (clients)
            {
                if (clients.Remove(token, out var client))
                {
                    foreach (var clientThreadRepository in clientThreadRepositories)
                    {
                        clientThreadRepository.Remove(client);
                    }

                    return client;
                }
            }

            return null;
        }

        public bool TryGet(string token, out IClient client)
        {
            lock (clients)
            {
                return clients.TryGetValue(token, out client);
            }
        }
    }
}