import alt, {Player} from 'alt';
import * as native from 'natives';

alt.log('Loaded: client->utility->vehicle.mjs');

export class Vehicle {
    
}

alt.onServer("Vehicle:SetIntoVehicle", (veh, seat) => {
    alt.setTimeout(() => native.setPedIntoVehicle(Player.local.scriptID, alt.Vehicle.getByID(veh).scriptID, seat),
        200);
});