using AltV.Net;
using AltV.Net.Elements.Entities;

namespace eXoTest1
{
    public class Test2 : IScript
    {
        public Test2()
        {
            Alt.Log("Test");
        }

        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public void PlayerConnect(IPlayer player, string reason)
        {
            Alt.Log($"{player.Name} connected.");
            player.Emit("showLogin");
        }

}
}
