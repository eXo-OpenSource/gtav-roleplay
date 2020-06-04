using System;
using System.Linq;
using AltV.Net;
using AltV.Net.Data;
using models.Enums;
using Sentry;
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

        [ClientEventAttribute("RegisterLogin:Login")]
        public void Login(IPlayer player, string username, string password)
        {
            try
            {
                    var result = WoltlabApi.Login(username, password).Result;
                    Console.WriteLine(result.ToString());
                    switch (result)
                    {
                        case WoltlabApi.LoginStatus.Success:
                            {
                                Console.WriteLine("Login erfolgreich!");
                                player.Emit("registerLogin:Success");
                                player.Emit("HUD:Hide", false);
								player.Emit("HUD:UpdateMoney", "penis");

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
                /* e.WithScopeOrThrow(s =>
                {
                    s.User = player.SentryContext;
                    var correlationId = SentrySdk.CaptureException(e);

                    Console.WriteLine(e.Message);
                    player.Emit("registerLogin:Error", "Unbekannter Fehler! Correlation Id: {0}", correlationId);
                }); */
            }
        }

        //Needed because SPA initlialization
        [ClientEventAttribute("ready")]
        public void PlayerReady(IPlayer player)
        {
	        player.Emit("Ui:ShowRegisterLogin");
        }

        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public void PlayerConnect(IPlayer player, string reason)
        {
            Alt.Log($"{player.Name} connected.");
            player.Position = new Position(0,0,75);
        }

        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public void PlayerDisconnect(IPlayer player, string reason)
        {
            Alt.Log($"{player.Name} disconnected.");
            Core.GetService<PlayerManager>().OnDisconnect(player);
        }
    }
}
