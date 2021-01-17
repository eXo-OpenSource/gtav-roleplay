import * as alt from 'alt-client'
import * as native from 'natives'
import {Singleton} from "../utils/Singleton";
import UiManager from "./UiManager";
import { isNumber, isBoolean } from 'util';

@Singleton
export class Speedo {
  private static active = false;

  static updateSpeedo() {
      if(alt.Player.local.vehicle == null) {
        if(Speedo.active) {
          UiManager.emit("Speedo:SetData", "active", false);
          Speedo.active = false;
        }
      } else {
        if(!Speedo.active) {
          UiManager.emit("Speedo:SetData", "active", true);
          Speedo.active = true;
        }
        const veh = alt.Player.local.vehicle;

        UiManager.emit("Speedo:SetData", "rpm", Math.round(veh.rpm*8000));
        UiManager.emit("Speedo:SetData", "speed", Math.round(native.getEntitySpeed(veh.scriptID)*3.65));
        UiManager.emit("Speedo:SetData", "gear", veh.gear);
      }
  }
}

alt.everyTick(Speedo.updateSpeedo)

alt.on("Speedo:EmitData", (key, value) => {
  UiManager.emit("Speedo:SetData", key, value)
});

export default Speedo;
