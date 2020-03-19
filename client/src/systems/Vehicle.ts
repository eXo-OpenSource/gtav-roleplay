import alt, {Entity, Player} from 'alt';
import * as native from 'natives';
import {distance} from "../utils/Vector";

alt.log('Loaded: client->utility->vehicle.mjs');

export class Vehicle {

}

alt.onServer("Vehicle:SetIntoVehicle", (veh, seat) => {
    alt.setTimeout(() => native.setPedIntoVehicle(Player.local.scriptID, alt.Vehicle.getByID(veh).scriptID, seat),
        300);
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
        let vehicle = native.getClosestVehicle(player.pos.x, player.pos.y, player.pos.z, 5.0, 0, 70);

        if(!vehicle) return;

        if(!native.areAnyVehicleSeatsFree(vehicle)) return;

        let nearestSeatDistance = Number.MAX_VALUE;
        let nearestSeat = 0;
        for (let i=0; i<= seatBones.length; i++) {
            if(!native.isVehicleSeatFree(vehicle, i, false)) continue;

            const boneIndex = native.getEntityBoneIndexByName(vehicle, seatBones[i]);
            if(boneIndex == -1) continue;

            const dist = distance(native.getWorldPositionOfEntityBone(vehicle, boneIndex), player.pos);
            if(dist > nearestSeatDistance) continue;
            nearestSeatDistance = dist;
            nearestSeat = i;
        }
        /*if(nearestSeat > 3) {
            native.taskWarpPedIntoVehicle(player.scriptID, vehicle, nearestSeat);
        } else {*/
            native.taskEnterVehicle(player.scriptID, vehicle, 5000, nearestSeat, 2, 1, 0);
        //}
    } else if(key == 0x58 && alt.gameControlsEnabled()) { //X
    	alt.emitServer("Vehicle:ToggleEngine")
	} else if(key == 76 && alt.gameControlsEnabled()) { //L
		alt.emitServer("Vehicle:ToggleLight")
	}
});

alt.on("streamSyncedMetaChange", (entity: Entity, key: string, value: any) => {
	if (entity.type == 1) {
		if(key == "vehicle.Light") {
			native.setVehicleLights(entity.scriptID, (value as boolean) ? 2 : 1);
		}
	}
})

//disable seat shuffling and engine key turning
alt.everyTick(() => {
   if(alt.Player.local.vehicle == null) return;
   native.setPedConfigFlag(alt.Player.local.scriptID, 184, true);
   native.setPedConfigFlag(alt.Player.local.scriptID, 429, true);
});
