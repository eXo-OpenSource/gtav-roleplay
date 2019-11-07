using server.Players.Characters;
using IPlayer = server.Players.IPlayer;

namespace server.Extensions
{
    internal static class CharacterSyncHandlerIPlayerExtension
    {
        public static void SendInitialSync(this IPlayer client)
        {
            CharacterSyncHandler.SendInitialSync(client);
        }

        public static void SetPublicSync<T>(this IPlayer client, string key, T value, bool forceSync = false)
        {
            CharacterSyncHandler.SetPublicSync(client, key, value, forceSync);
        }

        public static T GetPublicSync<T>(this IPlayer client, string key)
            where T : class
        {
            return CharacterSyncHandler.GetPublicSync<T>(client, key);
        }

        public static bool TryGetPublicSync<T>(this IPlayer client, string key, out T value)
            where T : class
        {
            var returnValue = CharacterSyncHandler.TryGetPublicSync<T>(client, key, out var result);
            value = result;
            return returnValue;
        }

        public static void ClearPublicSync(this IPlayer client)
        {
            CharacterSyncHandler.ClearPublicSync(client);
        }

        public static void SetPrivateSync<T>(this IPlayer client, string key, T value, bool forceSync = false)
            where T : class
        {
            CharacterSyncHandler.SetPrivateSync(client, key, value, forceSync);
        }

        public static T GetPrivateSync<T>(this IPlayer client, string key)
            where T : class
        {
            return CharacterSyncHandler.GetPrivateSync<T>(client, key);
        }

        public static bool TryGetPrivateSync<T>(this IPlayer client, string key, out T value)
            where T : class
        {
            var returnValue = CharacterSyncHandler.TryGetPrivateSync<T>(client, key, out var result);
            value = result;
            return returnValue;
        }

        public static void ClearPrivateSync(this IPlayer client)
        {
            CharacterSyncHandler.ClearPrivateSync(client);
        }
    }
}
