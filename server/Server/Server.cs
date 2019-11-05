using System;
using AltV.Net;
using eXoTest1;
using server.AutoMapper;
using server.Players;
using server.Util.Log;

namespace server
{
    internal class Core : Resource
    {
        private static readonly Logger<Core> Logger = new Logger<Core>();
        private PlayerManager _playerManager;

        public override void OnStart()
        {
            Console.WriteLine("Started");

            AutoMapperConfiguration.Initialize();
            Database.DatabaseCore.OnDatabaseInitialized += Event_OnResourceStart;

            var test = new Test2();
        }

        public override void OnStop()
        {
            Console.WriteLine("Stopped");
        }

        public void Event_OnResourceStart()
        {
            _playerManager = new PlayerManager();

        }

    }
}