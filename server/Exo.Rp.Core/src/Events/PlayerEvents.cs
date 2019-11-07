using System;
using AltV.Net;
using models.Enums;
using server.Database;
using server.Players;
using server.Players.Accounts;
using server.Util;
using server.Util.Log;
using IPlayer = server.Players.IPlayer;

namespace server.Events
{
    class PlayerEvents : IScript
    {
        private static readonly Logger<PlayerEvents> Logger = new Logger<PlayerEvents>();

        [Event("registerLogin:Login")]
        public void OnPlayerLogin(IPlayer player, string username, string password)
        {
            //Logger.Info(player.Name + " has requested Login");
            Console.Write(player.Name + " has requested Login");

            try
            {
                    var result = WoltlabApi.Login(username, password).Result;
                    switch (result)
                    {
                        case WoltlabApi.LoginStatus.Success:
                            {
                                Console.WriteLine("Login erfolgreich!");
                                player.Emit("registerLogin:Success");
                                
                                // Todo: Remove later
                                if (player.GetAccount() == default)
                                {
                                    Logger.Debug($"Creating new account for {player.Name}");
                                    var userdata = WoltlabApi.GetUserData(username).Result;
                                    AccountStatic.CreateAccount(player, username, userdata.email,
                                        AdminLevel.Entwickler);
                                }

                                Core.GetService<PlayerManager>().DoLogin(player);

                            break;
                        }
                        case WoltlabApi.LoginStatus.NoBetaAccess:
                            {
                                player.Emit("registerLogin:Error", "Du hast keinen Beta Zugang!");
                                break;
                            }
                        case WoltlabApi.LoginStatus.InvalidCredentials:
                            {
                                player.Emit("registerLogin:Error", "Falsches Passwort eingegeben!");
                                break;
                            }
                        default:
                            {
                                player.Emit("registerLogin:Error", "Unbekannter Fehler!");
                                break;
                            }
                    }
            }
            catch (Exception e)
            {
                player.Emit("registerLogin:Error", "Unbekannter Fehler!");
            }
        }

        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public void PlayerConnect(IPlayer player, string reason)
        {
            Alt.Log($"{player.Name} connected.");
            player.Emit("showLogin");
        }
    }
}
