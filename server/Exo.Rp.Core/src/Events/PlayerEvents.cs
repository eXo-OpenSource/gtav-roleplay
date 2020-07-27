using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using AltV.Net;
using AltV.Net.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using models.Enums;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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

        [ClientEvent("RegisterLogin:Login")]
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
        [ClientEvent("ready")]
        public void PlayerReady(IPlayer player)
        {
	        player.Emit("Ui:ShowRegisterLogin");
        }

		[ClientEvent("FaceFeatures:ApplyData")]
		public void ApplyFaceFeatures(IPlayer player, string _data)
		{
			var data = JsonConvert.DeserializeObject<List<object>>(_data);
			var ff = player.GetCharacter().FaceFeatures;

			player.GetCharacter().FirstName = data[0].ToString();
			player.GetCharacter().LastName = data[1].ToString();

			// Different variant used instead of simple casting because otherwise the server would crash
			ff.Gender = int.Parse(data[2].ToString());
			ff.ShapeFirst = int.Parse(data[3].ToString());
			ff.ShapeSecond = int.Parse(data[4].ToString());
			ff.ShapeThird = 0;
			ff.SkinFirst = int.Parse(data[3].ToString());
			ff.SkinSecond = int.Parse(data[4].ToString());
			ff.SkinThird = 0;
			ff.ShapeMix = Convert.ToDouble(float.Parse(data[5].ToString(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture));
			ff.SkinMix = Convert.ToDouble(float.Parse(data[6].ToString(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture));
			ff.Freckles = int.Parse(data[7].ToString());
			ff.EyeColor = int.Parse(data[8].ToString());
			ff.Hair = int.Parse(data[9].ToString());
			ff.HairColor = int.Parse(data[10].ToString());
			ff.HairColorHighlight = int.Parse(data[11].ToString());
			ff.Eyebrows = int.Parse(data[12].ToString());
			ff.EyebrowsColor1 = int.Parse(data[13].ToString());
			ff.Ageing = int.Parse(data[14].ToString());
			ff.FacialHair = int.Parse(data[15].ToString());
			ff.FacialHairColor1 = int.Parse(data[16].ToString());

			player.Emit("HUD:Hide", false);
			Alt.Log($"{player.GetCharacter().FirstName} {player.GetCharacter().LastName} ist erschienen!");
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
