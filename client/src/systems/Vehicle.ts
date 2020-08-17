import alt, {Entity, Player} from 'alt-client';
import * as native from 'natives';
import {distance} from "../utils/Vector";
import Speedo from '../ui/Speedo';

alt.log('Loaded: client->utility->vehicle.mjs');

alt.onServer("Vehicle:SetIntoVehicle", (veh, seat) => {
  let cleared = false;
  const interval = alt.setInterval(() => {
    if (!veh) return;
    const vehicleScriptId = alt.Vehicle.getByID(veh).scriptID;
    if (vehicleScriptId) {
      native.setPedIntoVehicle(alt.Player.local.scriptID, vehicleScriptId, seat);
      alt.clearInterval(interval);
      cleared = true;
    }
  }, 10);
  alt.setTimeout(() => {
    if (!cleared) {
      alt.clearInterval(interval);
    }
  }, 5000);
});

const seatBones: string[] = [
  "seat_pside_f",
  "seat_dside_r",
  "seat_pside_r",
  "seat_dside_r1",
  "seat_pside_r1",
  "seat_dside_r2",
  "seat_pside_r2",
  "seat_dside_r3",
  "seat_pside_r3",
  "seat_dside_r4",
  "seat_pside_r4",
  "seat_dside_r5",
  "seat_pside_r5",
  "seat_dside_r6",
  "seat_pside_r6",
  "seat_dside_r7",
  "seat_pside_r7"
];

alt.on("keyup", (key) => {
  // G
  if(key == 0x47 && alt.gameControlsEnabled()) {
    const player = alt.Player.local;
    let vehicle = native.getClosestVehicle(player.pos.x, player.pos.y, player.pos.z, 7.5, 0, 70);

    if(!vehicle) return;

    if(!native.areAnyVehicleSeatsFree(vehicle)) return;

    let nearestSeatDistance = Number.MAX_VALUE;
    let nearestSeat = 0;
    let dif = 0;
    for (let i=0; i< seatBones.length; i++) {
      //index in array is not equal to boneIndex. On Trash, seat_dside_r1 is 2
      //if(!native.isVehicleSeatFree(vehicle, i, false)) continue;

      const boneIndex = native.getEntityBoneIndexByName(vehicle, seatBones[i]);
      alt.log(seatBones[i], boneIndex);
      if(boneIndex == -1) {
        dif += 1
        continue;
      }

      const dist = distance(native.getWorldPositionOfEntityBone(vehicle, boneIndex), player.pos);
      if(dist > nearestSeatDistance) continue;
      nearestSeatDistance = dist;
      nearestSeat = i-dif;
    }
    /*if(nearestSeat > 3) {
      native.taskWarpPedIntoVehicle(player.scriptID, vehicle, nearestSeat);
    } else {*/
      native.taskEnterVehicle(player.scriptID, vehicle, 5000, nearestSeat, 1, 1, 0);
    //}
  }
});

// Sync vehicle states
alt.on("streamSyncedMetaChange", (entity: Entity, key: string, value: any) => {
  if (entity.type == 1) {
    switch (key) {
      case "vehicle.Light":
        native.setVehicleLights(entity.scriptID, (value as boolean) ? 2 : 1)
        alt.emit("Speedo:EmitData", "lights", (value as boolean) ? 1 : 0)
        break;
      case "vehicle.Trunk":
        if (value == 1)
          native.setVehicleDoorOpen(entity.scriptID, 5, true, true)
        else
          native.setVehicleDoorShut(entity.scriptID, 5, true)
        break;
      case "vehicle.EngineHood":
        if (value == true)
          native.setVehicleDoorOpen(entity.scriptID, 4, true, true)
        else
          native.setVehicleDoorShut(entity.scriptID, 4, true)
        break;
    }
  }
})

alt.on("syncedMetaChange", (entity: Entity, key: string, value: any) => {
  if (entity.type == 1) {
    if (key == "vehicle.Engine") {
      alt.emit("Speedo:EmitData", "fuel", (value as boolean) ? 1 : 0)
    }
  }
})

//disable seat shuffling and engine key turning
alt.everyTick(() => {
  if(alt.Player.local.vehicle == null) return;
  native.setPedConfigFlag(alt.Player.local.scriptID, 184, true);
  native.setPedConfigFlag(alt.Player.local.scriptID, 429, true);
});
