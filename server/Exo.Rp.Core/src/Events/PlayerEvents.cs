using System;
using AltV.Net;
using AltV.Net.Elements.Entities;
using server.Players;
using server.Util;
using IPlayer = server.Players.IPlayer;

namespace server.Events
{
    class PlayerEvents : IScript
    {
        [Event("registerLogin:Login")]
        public void OnIPlayerLogin(IPlayer player, string username, string password)
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
            catch
            {
                player.Emit("registerLogin:Error", "Unbekannter Fehler!");
            }
        }

    }
}
