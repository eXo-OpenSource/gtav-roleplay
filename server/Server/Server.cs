using System;
using AltV.Net;

namespace eXoTest1
{
    internal class eXoTest1 : Resource
    {

        public eXoTest1()
        {
        }
        public override void OnStart()
        {
            Console.WriteLine("Started");
            var test = new Test2();
        }

        public override void OnStop()
        {
            Console.WriteLine("Stopped");
        }

        

    }
}