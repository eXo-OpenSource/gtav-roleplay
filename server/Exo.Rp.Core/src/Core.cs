using System;
using AltV.Net;
using server.AutoMapper;
using server.Database;
using server.Players;

namespace server
{
    public class Core : Resource
    {
        private static readonly Util.Log.Logger<Core> Logger = new Util.Log.Logger<Core>();

        private DatabaseCore _databaseCore;

        public override void OnStart()
        {
            /*
            foreach (var player in Alt.GetAllPlayers())
            {
                player.SendError("Server is booting....");
                player.Kick("Server is booting... Please try again later.");
            }
            */
            
            // Initialize database
            DatabaseCore.OnDatabaseInitialized += OnReady;
            _databaseCore = new DatabaseCore();
            _databaseCore.OnResourceStartHandler();

            // Prepare other stuff required before loading components
            AutoMapperConfiguration.Initialize();
        }
        
        private static void OnReady()
        {

        }
        
        public override void OnStop()
        {
            
        }
    }
}