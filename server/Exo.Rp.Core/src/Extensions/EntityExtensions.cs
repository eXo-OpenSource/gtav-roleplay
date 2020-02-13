using System.Collections.Generic;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Args;
using AltV.Net.Elements.Entities;
using Newtonsoft.Json;

namespace server.Extensions
{
    internal static class EntityExtensions
    {
        private static readonly Dictionary<int, SyncedEntity> SyncedElementDatas = new Dictionary<int, SyncedEntity>();

        public static void SetElementData(this Entity entity, string key, dynamic value, bool synced = true)
        {
            entity.SetData(key, value);

            if (!synced) return;

            if (!SyncedElementDatas.ContainsKey(entity.Id))
                SyncedElementDatas.Add(entity.Id, new SyncedEntity(entity));

            if (SyncedElementDatas[entity.Id].Data.ContainsKey(key))
                SyncedElementDatas[entity.Id].Data[key] = value;
            else
                SyncedElementDatas[entity.Id].Data.Add(key, value);
            var type = entity.Type;
            // Workarround for new Rage issue
            foreach (var player in Alt.GetAllPlayers())
                player.Emit("onEntityDataChangedInternal", (int) type, entity.Id, key, value);
            // Alt.EmitAllClients("onEntityDataChangedInternal", (int) type, entity.Id, key, value);
        }

        public static dynamic GetElementData(this Entity entity, string key)
        {
            entity.GetSyncedMetaData(key, out var data); // TODO: Not sure if my fix was correct!
            return data;
        }

        public static void RefreshElementData(this Entity entity, string key)
        {
            if (entity.GetElementData(key) != null) entity.SetData(key, entity.GetElementData(key));
        }

        public static void AttachToTrigger(this Entity entity, Entity target, int boneIndex, Position offset,
            Position offsetRot)
        {
            foreach (var player in Alt.GetAllPlayers())
                player.Emit("attachObject", (int) entity.Type, entity.Id, (int) target.Type, target.Id,
                    boneIndex, offset, offsetRot);
            // Alt.EmitAllClients("attachObject", (int)entity.Type, entity.Id, (int)target.Type, target.Value, boneIndex, offset, offsetRot);
        }

        public static void AttachObject(this IPlayer player, Entity mapObject, int boneIndex, Position offset,
            Position offsetRot, bool useSoftPinning = false, bool collision = true, bool fixedRot = true, int vertexIndex = 0)
        {
            foreach (var playerItem in Alt.GetAllPlayers())
                playerItem.Emit("attachObjectToPlayer",
                    mapObject, player, boneIndex, offset, offsetRot, useSoftPinning, collision, fixedRot, vertexIndex);

            // Alt.EmitAllClients("attachObject", (int)entity.Type, entity.Id, (int)target.Type, target.Value, boneIndex, offset, offsetRot);
        }

        public static void DetachToTrigger(this Entity entity)
        {
            foreach (var player in Alt.GetAllPlayers())
                player.Emit("detachObject", (int) entity.Type, entity.Id);
            // Alt.EmitAllClients("detachObject", (int)entity.Type, entity.Id);
        }

        public static void TriggerElementDatas(IPlayer player)
        {
            foreach (var syncedEntity in SyncedElementDatas.Values)
            {
                var data = JsonConvert.SerializeObject(syncedEntity.Data);
                player.Emit("syncEntityData", syncedEntity, syncedEntity.Entity, data);
                // StaticLogger.Debug(typeof(EntityExtensions).Name ,"Trigger to " + player.name + " Entity: " + entity.Key.Value + " Key: " + datas.Key);
                // Console.Write("Trigger to " + player.name + " Entity: " + entity.Key.Value + " Key: " + datas.Key);
                // player.Emit("outputIPlayerConsole", "NEW NEW", false);
                // player.Emit("outputIPlayerConsole", data, false);
            }
        }

        private class SyncedEntity
        {
            public SyncedEntity(Entity entityObject)
            {
                Entity = entityObject.Id;
                EntityType = entityObject.Type;
                Data = new Dictionary<string, dynamic>();
            }

            public int Entity { get; }
            public BaseObjectType EntityType { get; }
            public Dictionary<string, dynamic> Data { get; }
        }
    }
}