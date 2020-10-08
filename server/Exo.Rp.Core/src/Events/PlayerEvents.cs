using System;
using System.Collections.Generic;
using System.Globalization;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Enums;
using Exo.Rp.Core.Players;
using Exo.Rp.Core.Players.Accounts;
using Exo.Rp.Core.Util;
using Exo.Rp.Core.Util.Log;
using Exo.Rp.Models.Enums;
using Exo.Rp.Sdk;
using Exo.Rp.Sdk.Logger;
using Newtonsoft.Json;
using Sentry;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Events
{
    class PlayerEvents : IScript
    {
        private static readonly ILogger<PlayerEvents> Logger = new Logger<PlayerEvents>();

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
                                    //var userdata = WoltlabApi.GetUserData(username).Result;
                                    AccountStatic.CreateAccount(player, username, "null", AdminLevel.Entwickler);
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
                e.WithScopeOrThrow(s =>
                {
                    s.User = player.SentryContext;
                    var correlationId = SentrySdk.CaptureException(e);

                    Console.WriteLine(e.Message);
                    player.Emit("registerLogin:Error", "Unbekannter Fehler! Correlation Id: {0}", correlationId);
                });
            }
        }

        //Needed because SPA initlialization
        [ClientEvent("ready")]
        public void PlayerReady(IPlayer player)
        {
            player.Emit("Ui:ShowRegisterLogin");
        }

        [ClientEvent("Player:SetPosition")]
        public void SetPosition(IPlayer player, float x, float y, float z, int rotx = 0, int roty = 0, int rotz = 0)
        {
            player.Position = new Position(x, y, z);
            player.Rotation = new Rotation(x, y, z);
            player.SendSuccess("Willkommen in San Andreas!");
        }

        [ClientEvent("BankAccount:RefreshData")]
        public void RefreshData(IPlayer client)
        {
            client.Emit("BankAccount:UpdateData", client.GetCharacter().GetMoney(true),
                client.GetCharacter().GetMoney(false), client.GetCharacter().GetNormalizedName());
        }

        [ClientEvent("BankAccount:CashIn")]
        public void CashIn(IPlayer client, int amount)
        {
            if (client.GetCharacter().GetMoney() < amount)
            {
                client.SendError("Du hast nicht genug Geld bei dir.");
                return;
            }

            RefreshData(client);

            client.GetCharacter().GiveMoney(amount, "Maze Bank Einzahlung", true);
            client.GetCharacter().TakeMoney(amount, "Maze Bank Einzahlung", false);
            client.SendSuccess($"Du hast ${amount} eingezahlt!");
        }

        [ClientEvent("BankAccount:CashOut")]
        public void CashOut(IPlayer client, int amount)
        {
            if (client.GetCharacter().BankAccount.GetMoney() < amount)
            {
                client.SendError("Du verfügst nicht über genügend Geld auf der Bank.");
                return;
            }

            RefreshData(client);

            client.GetCharacter().GiveMoney(amount, "Maze Bank Auszahlung", false);
            client.GetCharacter().TakeMoney(amount, "Maze Bank Auszahlung", true);
            client.SendSuccess($"Du hast ${amount} ausgezahlt!");
        }

        [ClientEvent("Ui:Hide")]
        public void HideUi(IPlayer player, bool hide = true)
        {
            player.HideUi(hide);
        }

        [ClientEvent("FaceFeatures:ApplyData")]
        public void ApplyFaceFeatures(IPlayer client, string _data)
        {
            var data = JsonConvert.DeserializeObject<List<object>>(_data);
            var ff = client.GetCharacter().FaceFeatures;

            Alt.Log(data[39].ToString());
            client.Model = Alt.Hash(data[39].ToString());
            client.GetCharacter().FirstName = data[0].ToString();
            client.GetCharacter().LastName = data[1].ToString();
            client.GetCharacter().Gender = int.Parse(data[2].ToString()) == 0 ? Gender.Female : Gender.Male;
            client.GetCharacter().Skin = (PedModel)client.Model;

            // Different variant used instead of simple casting because otherwise the server would crash
            ff.Gender = int.Parse(data[2].ToString());
            ff.ShapeFirst = int.Parse(data[3].ToString());
            ff.ShapeSecond = int.Parse(data[4].ToString());
            ff.ShapeThird = 0;
            ff.SkinFirst = int.Parse(data[5].ToString());
            ff.SkinSecond = int.Parse(data[6].ToString());
            ff.SkinThird = 0;
            ff.ShapeMix = float.Parse(data[7].ToString());
            ff.SkinMix = float.Parse(data[8].ToString());
            //ff.Freckles = int.Parse(data[9].ToString());
            ff.EyeColor = int.Parse(data[10].ToString());
            ff.Hair = int.Parse(data[11].ToString());
            ff.HairColor = int.Parse(data[12].ToString());
            ff.HairColorHighlight = int.Parse(data[13].ToString());
            ff.Eyebrows = int.Parse(data[14].ToString());
            ff.EyebrowsColor1 = int.Parse(data[15].ToString());
            ff.Ageing = int.Parse(data[16].ToString());
            ff.FacialHair = int.Parse(data[17].ToString());
            ff.FacialHairColor1 = int.Parse(data[18].ToString());
            ff.NoseWidth = float.Parse(data[19].ToString());
            ff.NoseHeight = float.Parse(data[20].ToString());
            ff.NoseLength = float.Parse(data[21].ToString());
            ff.NoseBridge = float.Parse(data[22].ToString());
            ff.NoseTip = float.Parse(data[23].ToString());
            ff.NoseShift = float.Parse(data[24].ToString());
            ff.BrowHeight = float.Parse(data[25].ToString());
            ff.BrowWidth = float.Parse(data[26].ToString());
            ff.CheekboneHeight = float.Parse(data[27].ToString());
            ff.CheekboneWidth = float.Parse(data[28].ToString());
            ff.CheeksWidth = float.Parse(data[29].ToString());
            ff.EyesWidth = float.Parse(data[30].ToString());
            ff.LipsWidth = float.Parse(data[31].ToString());
            ff.JawWidth = float.Parse(data[32].ToString());
            ff.JawHeight = float.Parse(data[33].ToString());
            ff.ChinLength = float.Parse(data[34].ToString());
            ff.ChinPosition = float.Parse(data[35].ToString());
            ff.ChinWidth = float.Parse(data[36].ToString());
            ff.ChinShape = float.Parse(data[37].ToString());
            ff.NeckWidth = float.Parse(data[38].ToString());

            client.GetCharacter().SyncFaceFeatures();
            Alt.Log($"{client.GetCharacter().FirstName} {client.GetCharacter().LastName} ist erschienen!");
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