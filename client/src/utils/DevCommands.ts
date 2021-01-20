import * as alt from "alt-client";
import * as native from "natives";

var positions = [];
var rotations = [];
var collected = 0;
var player = alt.Player.local;

alt.on("keyup", (key) => {
  switch (key) {
    case 0x73:
      positions.push("new Position(" + player.pos.x + "f, " + player.pos.y + "f, " + player.pos.z + "f)\n");
      rotations.push("new Rotation(" + player.rot.x + "f, " + player.rot.y + "f, " + player.rot.z + "f)\n");
      alt.emitServer("Chat:Message", "[ID: " + (positions.length - 1) + "] Position hinzugefügt");
      break;
    case 0x72:
      if (positions.length < 1) break;
      alt.emitServer("Chat:Message", "[ID: " + (positions.length - 1) + "] Position gelöscht");
      positions.pop();
      rotations.pop();
      break
  }
})

alt.on("consoleCommand", (cmd, ...args) => {
  switch (cmd) {
    case "positions":
      var resString = "Keine Positionen gespeichert";
      if (args.length > 0 && args[0] == "true") {
        resString = "";
        for (var i = 0; i < positions.length; i++) {
          resString += "{" + positions[i].replace("\n", ",") + " " + rotations[i].replace("\n", "") + "},\n";
        }
      } else {
        resString = positions.join("");
      }
      alt.log(resString);
      break;
    case "resetpos":
      positions = [];
      rotations = [];
      alt.emitServer("Chat:Message", "Alle Positionen gelöscht!");
      break;
    default:
      alt.emitServer("Chat:Message", "Command " + cmd + " not found!");
      break;
  }
})
