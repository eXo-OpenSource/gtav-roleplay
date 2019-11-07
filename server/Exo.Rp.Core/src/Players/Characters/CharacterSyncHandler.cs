using System.Collections.Generic;
using System.Linq;
using System.Timers;
using AltV.Net;
using server.Util.Log;
using server.Util.Settings;

//using shared.Serialization;

namespace server.Players.Characters
{
    using ParameterMapping = Dictionary<string, object>;
    using ParameterUpdatedMapping = Dictionary<string, bool>;

    public class CharacterSyncHandler
    {
        private static readonly Logger<CharacterSyncHandler> Logger = new Logger<CharacterSyncHandler>();

        private static readonly Dictionary<IPlayer, ParameterMapping> PublicSync = new Dictionary<IPlayer, ParameterMapping>();
        private static readonly Dictionary<IPlayer, ParameterUpdatedMapping> PublicSyncUpdate = new Dictionary<IPlayer, ParameterUpdatedMapping>();
        private static readonly Dictionary<IPlayer, ParameterMapping> PrivateSync = new Dictionary<IPlayer, ParameterMapping>();
        private static readonly Dictionary<IPlayer, ParameterUpdatedMapping> PrivateSyncUpdate = new Dictionary<IPlayer, ParameterUpdatedMapping>();

        static CharacterSyncHandler()
        {
            var timer = new Timer();
            timer.Elapsed += (source, e) => Sync();
            timer.Interval = SettingsManager.ServerSettings.DataSync.Interval;
            timer.Enabled = true;
        }

        private static void Sync()
        {
            var players = Alt.GetAllPlayers().Cast<IPlayer>().ToList();
            if (!players.Any())
                return;

            Logger.Debug($"Syncing Private/Public Character Data for { players.Count } Players.");
            _Sync(players, PublicSync, PublicSyncUpdate, CharacterSyncType.Public);
            _Sync(players, PrivateSync, PrivateSyncUpdate, CharacterSyncType.Private);
        }

        public static void SendInitialSync(IPlayer player)
        {
            if (PrivateSync.ContainsKey(player))
                player.Emit("PlayerPrivateSync", PrivateSync[player]);

            var summary = PublicSync.ToDictionary(x => x.Key, x => x.Value);
            if (summary.Count() == 0) return;
            player.Emit("PlayerPublicSync", summary);
        }

        private static void _Sync(IReadOnlyList<IPlayer> players,
            IReadOnlyDictionary<IPlayer, ParameterMapping> data,
            IReadOnlyDictionary<IPlayer, ParameterUpdatedMapping> updatedFields, CharacterSyncType type)
        {
            var summary = new Dictionary<IPlayer, ParameterMapping>();
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
                   player.Emit("PlayerPrivateSync", syncDict);
                    return;
                }

                summary[player] = syncDict;
            }

            if (summary.Count == 0) return;
            foreach (var player in players)
            {
                // var json = JsonConvert.SerializeObject(summary, Formatting.None,
                //  DtoSerializationBinder.SerializerSettings);
                // player.Emit("PlayerPublicSync", json);
                player.Emit("PlayerPublicSync", summary);
            }
        }

        public static void SetPublicSync<T>(IPlayer client, string key, T value, bool forceSync = false)
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

        public static T GetPublicSync<T>(IPlayer client, string key)
            where T : class
        {
            return PublicSync.ContainsKey(client) ? PublicSync[client][key] as T : default;
        }

        public static bool TryGetPublicSync<T>(IPlayer client, string key, out T value)
            where T : class
        {
            if (PublicSync.TryGetValue(client, out var dict) && dict.TryGetValue(key, out var result) && result is T outValue)
            {
                value = outValue;
                return true;
            }
            value = default(T);
            return false;
        }

        public static void ClearPublicSync(IPlayer client)
        {
            if (PublicSyncUpdate.ContainsKey(client))
                PublicSyncUpdate[client].Clear();
            if (PublicSync.ContainsKey(client))
                PublicSync[client].Clear();
        }

        public static void SetPrivateSync<T>(IPlayer client, string key, T value, bool forceSync = false)
            where T : class
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

        public static T GetPrivateSync<T>(IPlayer client, string key)
            where T : class
        {
            return PrivateSync.ContainsKey(client) ? (T)PrivateSync[client][key] : default(T);
        }

        public static bool TryGetPrivateSync<T>(IPlayer client, string key, out T value)
            where T : class
        {
            if (PrivateSync.TryGetValue(client, out var dict) && dict.TryGetValue(key, out var result) && result is T outValue)
            {
                value = outValue;
                return true;
            }
            value = default(T);
            return false;
        }

        public static void ClearPrivateSync(IPlayer client)
        {
            if (PrivateSyncUpdate.ContainsKey(client))
                PrivateSyncUpdate[client].Clear();
            if (PrivateSync.ContainsKey(client))
                PrivateSync[client].Clear();
        }
    }
}
