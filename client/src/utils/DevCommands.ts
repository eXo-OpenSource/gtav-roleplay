import * as alt from "alt-client";
import * as native from "natives";

var positions = new Array();
var collected = 0
var player = alt.Player.local

alt.on("keyup", (key) => {
  if (key == 0x73) {
    positions[collected++] = "new Position(" + player.pos.x + "f, " + player.pos.y + "f, " + player.pos.z + "f)"
  }
})

alt.on("consoleCommand", (cmd, ...args) => {
  if (cmd == "positions") {
    positions.forEach(element => {
      alt.log(element);
    });
  }
})
