import alt from "alt-client";
import natives from "natives";

alt.onServer("Animation:Start",(anim, dict, flag) => {
  if(!natives.hasAnimDictLoaded(dict)) natives.requestAnimDict(dict)
  const interval = alt.setInterval(() => {
    if(natives.hasAnimDictLoaded(dict)) {
      natives.taskPlayAnim(alt.Player.local.scriptID, dict, anim, 1, -1, -1, flag, 0, false, false, false)
      alt.clearInterval(interval);
    }
  }, 10);
})

alt.onServer("Animation:Clear", () => {
  natives.clearPedTasks(alt.Player.local.scriptID);
  if(!alt.Player.local.vehicle) {
    natives.clearPedSecondaryTask(alt.Player.local.scriptID)
  }
})

alt.onServer("Animation:ForceClear", () => {
  natives.clearPedTasksImmediately(alt.Player.local.scriptID);
})
