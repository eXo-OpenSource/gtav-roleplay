using System.Collections.Generic;
using System.Linq;
using System.Timers;
using AltV.Net;
using AltV.Net.Elements.Entities;
using server.Util.Log;
using server.Util.Settings;

//using shared.Serialization;

namespace server.Players.Characters
{
    using ParameterMapping = Dictionary<string, object>;
    using ParameterUpdatedMapping = Dictionary<string, bool>;

    internal enum CharacterSyncType
    {
        Public, Private
    }

    public class CharacterSyncHandler
    {
        private static readonly Logger<CharacterSyncHandler> Logger = new Logger<CharacterSyncHandler>();

        private static readonly Dictionary<Interfaces.IPlayer, ParameterMapping> PublicSync = new Dictionary<Interfaces.IPlayer, ParameterMapping>();
        private static readonly Dictionary<Interfaces.IPlayer, ParameterUpdatedMapping> PublicSyncUpdate = new Dictionary<Interfaces.IPlayer, ParameterUpdatedMapping>();
        private static readonly Dictionary<Interfaces.IPlayer, ParameterMapping> PrivateSync = new Dictionary<Interfaces.IPlayer, ParameterMapping>();
        private static readonly Dictionary<Interfaces.IPlayer, ParameterUpdatedMapping> PrivateSyncUpdate = new Dictionary<Interfaces.IPlayer, ParameterUpdatedMapping>();

        static CharacterSyncHandler()
        {
            var timer = new Timer();
            timer.Elapsed += (source, e) => Sync();
            timer.Interval = SettingsManager.ServerSettings.DataSync.Interval;
            timer.Enabled = true;
        }

        static void Sync()
        {
            var players = Alt.GetAllPlayers().Cast<Interfaces.IPlayer>();
            if (!players.Any())
                return;

            // Logger.Debug($"Syncing Private/Public Character Data for { players.Count } Players.");
            //_Sync(players, PublicSync, PublicSyncUpdate, CharacterSyncType.Public);
           // _Sync(players, PrivateSync, PrivateSyncUpdate, CharacterSyncType.Private);
        }

        public static void SendInitialSync(Interfaces.IPlayer player)
        {
            /*if (PrivateSync.ContainsKey(player))
                player.Emit("PlayerPrivateSync", JsonConvert.SerializeObject(PrivateSync[player], Formatting.None, DtoSerializationBinder.SerializerSettings));

            var summary = PublicSync.ToDictionary(x => (ushort)x.Key.Value, x => x.Value);
            if (summary.Count == 0) return;
            player.Emit("PlayerPublicSync",
                JsonConvert.SerializeObject(summary, Formatting.None, DtoSerializationBinder.SerializerSettings));*/
        }

        private static void _Sync(IReadOnlyCollection<Interfaces.IPlayer> players,
            IReadOnlyDictionary<Interfaces.IPlayer, ParameterMapping> data,
            IReadOnlyDictionary<Interfaces.IPlayer, ParameterUpdatedMapping> updatedFields, CharacterSyncType type)
        {
            var summary = new Dictionary<ushort, ParameterMapping>();
            foreach (var player in players)
            {
                if (player == null || !data.ContainsKey(player) || !updatedFields.ContainsKey(player))
                    continue;

                var syncDict = updatedFields[player].ToDictionary(x => x.Key, x => data[player][x.Key]);
                updatedFields[player].Clear();

                if (syncDict.Count == 0) continue;
                if (type == CharacterSyncType.Private)
                {
                    //var json = JsonConvert.SerializeObject(syncDict, Formatting.None,
                   //     DtoSerializationBinder.SerializerSettings);
                   // player.Emit("PlayerPrivateSync", json);
                    return;
                }

                summary[(ushort)player.Id] = syncDict;
            }

            if (summary.Count == 0) return;
            foreach (var player in players)
            {
               // var json = JsonConvert.SerializeObject(summary, Formatting.None,
                  //  DtoSerializationBinder.SerializerSettings);
               // player.Emit("PlayerPublicSync", json);
            }
        }

        public static void SetPublicSync<T>(Interfaces.IPlayer client, string key, T value, bool forceSync = false)
        {
            PublicSync.TryGetValue(client, out var dict);
            PublicSyncUpdate.TryGetValue(client, out var dictUpdate);
            if (dict == null)
            {
                dict = new Dictionary<string, object>();
                PublicSync.Add(client, dict);
            }

            if (dictUpdate == null)
            {
                dictUpdate = new Dictionary<string, bool>();
                PublicSyncUpdate.Add(client, dictUpdate);
            }

            dict[key] = value;
            dictUpdate[key] = true;

            if (!forceSync) return;
            Sync();
        }

        public static T GetPublicSync<T>(Interfaces.IPlayer client, string key)
        {
            return PublicSync.ContainsKey(client) ? (T)PublicSync[client][key] : default(T);
        }

        public static bool TryGetPublicSync<T>(Interfaces.IPlayer client, string key, out T value)
        {
            if (PublicSync.TryGetValue(client, out var dict) && dict.TryGetValue(key, out var result) && result is T outValue)
            {
                value = outValue;
                return true;
            }
            value = default(T);
            return false;
        }

        public static void ClearPublicSync(Interfaces.IPlayer client)
        {
            if (PublicSyncUpdate.ContainsKey(client))
                PublicSyncUpdate[client].Clear();
            if (PublicSync.ContainsKey(client))
                PublicSync[client].Clear();
        }

        public static void SetPrivateSync<T>(Interfaces.IPlayer client, string key, T value, bool forceSync = false)
        {
            PrivateSync.TryGetValue(client, out var dict);
            PrivateSyncUpdate.TryGetValue(client, out var dictUpdate);
            if (dict == null)
            {
                dict = new Dictionary<string, object>();
                PrivateSync.Add(client, dict);
            }

            if (dictUpdate == null)
            {
                dictUpdate = new Dictionary<string, bool>();
                PrivateSyncUpdate.Add(client, dictUpdate);
            }

            dict[key] = value;
            dictUpdate[key] = true;

            if (!forceSync) return;
            Sync();
        }

        public static T GetPrivateSync<T>(Interfaces.IPlayer client, string key)
        {
            return PrivateSync.ContainsKey(client) ? (T)PrivateSync[client][key] : default(T);
        }

        public static bool TryGetPrivateSync<T>(Interfaces.IPlayer client, string key, out T value)
        {
            if (PrivateSync.TryGetValue(client, out var dict) && dict.TryGetValue(key, out var result) && result is T outValue)
            {
                value = outValue;
                return true;
            }
            value = default(T);
            return false;
        }

        public static void ClearPrivateSync(Interfaces.IPlayer client)
        {
            if (PrivateSyncUpdate.ContainsKey(client))
                PrivateSyncUpdate[client].Clear();
            if (PrivateSync.ContainsKey(client))
                PrivateSync[client].Clear();
        }
    }
}
