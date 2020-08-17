using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.ServerEvent;
using server.Streamer.Grid;

namespace server.Streamer.Private
{
    public class PrivateEntityThread
    {
        private bool running = true;

        private readonly ulong threadIndex;

        private readonly Thread thread;

        private readonly PrivateEntityThreadRepository entityThreadRepository;

        private readonly PrivateClientThreadRepository clientThreadRepository;

        private readonly LimitedPrivateGrid3 spatialPartition;

        private readonly int syncRate;

        private readonly LinkedList<IEntity> entitiesToRemoveFromClient = new LinkedList<IEntity>();
        private readonly LinkedList<IEntity> entitiesToResetFromClient = new LinkedList<IEntity>();

        private readonly LinkedList<string> changedEntityDataKeys = new LinkedList<string>();

        private readonly Action<IClient, IEntity, LinkedList<string>> onEntityCreate;

        private readonly Action<IClient, IEntity> onEntityRemove;

        private readonly Action<IClient, IEntity, LinkedList<string>> onEntityDataChange;

        private readonly Action<IClient, IEntity, Vector3> onEntityPositionChange;

        private readonly Action<IClient, IEntity> onEntityClearCache;

        private Vector3 clientPosition = Vector3.Zero;

        public PrivateEntityThread(ulong threadIndex, PrivateEntityThreadRepository entityThreadRepository,
            PrivateClientThreadRepository clientThreadRepository,
            LimitedPrivateGrid3 spatialPartition, int syncRate,
            Action<IClient, IEntity, LinkedList<string>> onEntityCreate, Action<IClient, IEntity> onEntityRemove,
            Action<IClient, IEntity, LinkedList<string>> onEntityDataChange,
            Action<IClient, IEntity, Vector3> onEntityPositionChange, Action<IClient, IEntity> onEntityClearCache)
        {
            this.threadIndex = threadIndex;

            this.entityThreadRepository = entityThreadRepository ??
                                        throw new ArgumentException("entityThreadRepository should not be null.");
            this.clientThreadRepository = clientThreadRepository;
            this.spatialPartition =
                spatialPartition ?? throw new ArgumentException("spatialPartition should not be null.");
            this.syncRate = syncRate;
            this.onEntityCreate = onEntityCreate ?? throw new ArgumentException("onEntityCreate should not be null.");
            this.onEntityRemove = onEntityRemove ?? throw new ArgumentException("onEntityRemove should not be null.");
            this.onEntityDataChange = onEntityDataChange ??
                                    throw new ArgumentException("onEntityDataChange should not be null.");
            this.onEntityPositionChange = onEntityPositionChange ??
                                        throw new ArgumentException("onEntityPositionChange should not be null.");
            this.onEntityClearCache = onEntityClearCache ??
                                    throw new ArgumentException("onEntityPositionChange should not be null.");

            thread = new Thread(OnLoop) {IsBackground = true};
            thread.Start();
        }

        public void OnLoop()
        {
            while (running)
            {
                try
                {
                    while (entityThreadRepository.EntitiesChannelReader.TryRead(out var entityQueueResult))
                    {
                        var (entityToChange, state) = entityQueueResult;
                        switch (state)
                        {
                            case 0:
                                spatialPartition.Add(entityToChange);
                                foreach (var (key, _) in entityToChange.ThreadLocalData)
                                {
                                    entityToChange.DataSnapshot.Update(key);
                                }

                                break;
                            case 1:
                                spatialPartition.Remove(entityToChange);
                                foreach (var client in entityToChange.GetClients())
                                {
                                    client.RemoveEntity(threadIndex, entityToChange);
                                    onEntityRemove(client, entityToChange);
                                }

                                // We don't have to do this, but we do for for consistency because entity gets garbage collected anyway
                                //entityToChange.GetClients().Clear();

                                foreach (var client in entityToChange.DataSnapshot.GetLastClients())
                                {
                                    onEntityClearCache(client, entityToChange);
                                }

                                break;
                            case 2:
                                // Check if position state is new position so we can set the new position to the entity internal position
                                var (hasNewPosition, hasNewRange, hasNewDimension) =
                                    entityToChange.TrySetPropertiesComputing(
                                        out var newPosition,
                                        out var newRange, out var newDimension);

                                if (hasNewPosition)
                                {
                                    spatialPartition.UpdateEntityPosition(entityToChange, newPosition);
                                    foreach (var entityClient in entityToChange.GetClients())
                                    {
                                        onEntityPositionChange(entityClient, entityToChange, newPosition);
                                    }
                                }

                                if (hasNewRange)
                                {
                                    spatialPartition.UpdateEntityRange(entityToChange, newRange);
                                }

                                if (hasNewDimension)
                                {
                                    spatialPartition.UpdateEntityDimension(entityToChange, newDimension);
                                }

                                break;
                        }
                    }

                    while (entityThreadRepository.EntitiesDataChannelReader.TryRead(out var entityDataQueueResult))
                    {
                        var (entityWithChangedData, changedDataKey, changedDataValue, notDeleted) =
                            entityDataQueueResult;
                        entityWithChangedData.DataSnapshot.Update(changedDataKey);
                        if (notDeleted)
                        {
                            entityWithChangedData.SetThreadLocalData(changedDataKey, changedDataValue);
                        }
                        else
                        {
                            entityWithChangedData.ResetData(changedDataKey);
                        }
                    }

                    //TODO: when the id provider add / remove doesn't work use the idprovider inside this loop only

                    lock (clientThreadRepository.Mutex)
                    {
                        if (clientThreadRepository.ClientsToRemove.Count != 0)
                        {
                            while (clientThreadRepository.ClientsToRemove.TryDequeue(out var clientToRemove))
                            {
                                clientToRemove.Snapshot.CleanupEntities(threadIndex, clientToRemove);
                                foreach (var entityFromRemovedClient in clientToRemove.GetEntities(threadIndex))
                                {
                                    entityFromRemovedClient.RemoveClient(clientToRemove);
                                }
                            }
                        }

                        foreach (var (_, client) in clientThreadRepository.Clients)
                        {
                            if (!client.TryGetDimensionAndPosition(out var dimension, ref clientPosition))
                            {
                                continue;
                            }

                            var foundEntities = spatialPartition.Find(clientPosition, dimension, ((PlayerClient)client).GetPlayer().Id);

                            var lastCheckedEntities = client.GetLastCheckedEntities(threadIndex);
                            if (lastCheckedEntities.Count != 0)
                            {
                                foreach (var (lastCheckedEntity, lastChecked) in lastCheckedEntities)
                                {
                                    if (lastChecked)
                                    {
                                        entitiesToResetFromClient.AddLast(lastCheckedEntity);
                                    }
                                    else
                                    {
                                        entitiesToRemoveFromClient.AddLast(lastCheckedEntity);
                                        onEntityRemove(client, lastCheckedEntity);
                                    }
                                }

                                if (entitiesToResetFromClient.Count != 0)
                                {
                                    var currEntity = entitiesToResetFromClient.First;

                                    while (currEntity != null)
                                    {
                                        client.RemoveCheck(threadIndex, currEntity.Value);
                                        currEntity = currEntity.Next;
                                    }

                                    entitiesToResetFromClient.Clear();
                                }

                                if (entitiesToRemoveFromClient.Count != 0)
                                {
                                    var currEntity = entitiesToRemoveFromClient.First;

                                    while (currEntity != null)
                                    {
                                        client.RemoveEntity(threadIndex, currEntity.Value);
                                        currEntity = currEntity.Next;
                                    }

                                    entitiesToRemoveFromClient.Clear();
                                }
                            }

                            if (foundEntities != null)
                            {
                                for (int i = 0, length = foundEntities.Count; i < length; i++)
                                {
                                    var foundEntity = foundEntities[i];
                                    client.AddCheck(threadIndex, foundEntity);
                                    foundEntity.DataSnapshot.CompareWithClient(threadIndex, changedEntityDataKeys,
                                        client);
                                    // We add client to entity here so we can remove it from the client when the entity got removed
                                    foundEntity.TryAddClient(client);
                                    if (client.TryAddEntity(threadIndex, foundEntity))
                                    {
                                        if (changedEntityDataKeys.Count == 0)
                                        {
                                            onEntityCreate(client, foundEntity, null);
                                        }
                                        else
                                        {
                                            onEntityCreate(client, foundEntity, changedEntityDataKeys);
                                            changedEntityDataKeys.Clear();
                                        }
                                    }
                                    else if (changedEntityDataKeys.Count != 0)
                                    {
                                        onEntityDataChange(client, foundEntity, changedEntityDataKeys);
                                        changedEntityDataKeys.Clear();
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }

                Thread.Sleep(syncRate);
            }
        }

        public void Stop()
        {
            running = false;
            thread.Join();
        }
    }
}