using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using server.Util.Log;

namespace server.Util
{
    public class WoltlabApi
    {
        private static readonly Logger<WoltlabApi> Logger = new Logger<WoltlabApi>();

        public enum LoginStatus
        {
            Success = 0,
            InvalidCredentials = 1,
            NoBetaAccess = 2
        }

        private const string Url = "https://exo-roleplay.de/forum/?user-api";
        private const bool PrintToConsole = true;
        private const bool OnlyBeta = true;
        private const int BetaGroupId = 7;

        private static readonly HttpClient HttpClient = new HttpClient();

        public static async Task<LoginStatus> Login(string username, string password)
        {
            var values = new Dictionary<string, string>
            {
                {"method", "login"},
                {"secret", "jWTf6KBkVcJI$m0kP2tR_M"},
                {"username", username},
                {"password", password}
            };

            var content = new FormUrlEncodedContent(values);
            var response = await HttpClient.PostAsync(Url, content);

            var responseString = await response.Content.ReadAsStringAsync();
            if (PrintToConsole)
            {
                Logger.Debug("Response from WBB:");
                Logger.Debug(responseString);
            }

            var result = JsonConvert.DeserializeObject<WbbLoginResponse>(responseString);
            if (result.status != null && result.status == 200)
                return OnlyBeta == false || CheckGroup(result.data.groups, BetaGroupId)
                    ? LoginStatus.Success
                    : LoginStatus.NoBetaAccess;


            return LoginStatus.InvalidCredentials;
        }

        public static async Task<LoginStatus> HashLogin(string username, string hash)
        {
            var values = new Dictionary<string, string>
            {
                {"method", "loginWithHash"},
                {"secret", "jWTf6KBkVcJI$m0kP2tR_M"},
                {"username", username},
                {"hash", hash}
            };

            var content = new FormUrlEncodedContent(values);
            var response = await HttpClient.PostAsync(Url, content);

            var responseString = await response.Content.ReadAsStringAsync();
            if (PrintToConsole)
            {
                Logger.Debug("Response from WBB:");
                Logger.Debug(responseString);
            }

            var result = JsonConvert.DeserializeObject<WbbLoginResponse>(responseString);
            if (result.status != null && result.status == 200)
                return OnlyBeta == false || CheckGroup(result.data.groups, BetaGroupId)
                    ? LoginStatus.Success
                    : LoginStatus.NoBetaAccess;


            return LoginStatus.InvalidCredentials;
        }

        public static async Task<UserData> GetUserData(string username)
        {
            var values = new Dictionary<string, string>
            {
                {"method", "getPasswordHash"},
                {"secret", "jWTf6KBkVcJI$m0kP2tR_M"},
                {"username", username}
            };

            var content = new FormUrlEncodedContent(values);
            var response = await HttpClient.PostAsync(Url, content);

            var responseString = await response.Content.ReadAsStringAsync();
            if (PrintToConsole)
            {
                Logger.Debug("Response from WBB:");
                Logger.Debug(responseString);
            }

            var result = JsonConvert.DeserializeObject<WbbLoginResponse>(responseString);
            if (result.status != null && result.status == 200) return result.data;

            return null;
        }

        public static bool CheckGroup(List<Group> groups, int groupId)
        {
            foreach (var group in groups)
                if (group.groupID == groupId)
                    return true;

            return false;
        }


        public class Group
        {
            public int groupID { get; set; }
            public string groupName { get; set; }
            public int groupType { get; set; }
        }

        public class UserData
        {
            public int userID { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public List<Group> groups { get; set; }
            public string hash { get; set; }
        }

        public class WbbLoginResponse
        {
            public int? status { get; set; }
            public int? code { get; set; }
            public UserData data { get; set; }
        }
    }
}