import alt, {Entity, Player, Vector3, Vehicle, WorldObject} from 'alt';
import * as native from 'natives';
import Marker from "../systems/Marker";
import {distance} from "../utils/Vector";

export class WasteCollector {

	private collector: Entity;
	private marker: Marker = Marker.createMarker(22, new Vector3(50, 0, 0), 2, {r: 255, g: 255, b: 0, a: 125});
	private hit = false;

	constructor() {

		alt.onServer("JobTrash:SetVehicle", (veh, max) => {
			const interval = alt.setInterval(() => {
				const vehicle = alt.Vehicle.getByID(veh);
				if (vehicle) {
					this.collector = vehicle;
					alt.clearInterval(interval)
				}
			}, 10);
		});

		alt.onServer("Job:StopJob", (jobid) => {
			this.marker.visible = false;
		})

		alt.everyTick(() => {
			if(!this.collector || !this.collector.valid) return;
			if(native.getEntitySpeed(this.collector.scriptID)*3.6 > 5) {
				this.marker.visible = false;
				return;
			}

			const markerPos = native.getOffsetFromEntityInWorldCoords(this.collector.scriptID, 0, -5, 0);

			if(distance(markerPos, this.marker.pos) > 1) {
				this.marker.pos = markerPos;
				this.marker.visible = true;
			}

			if(distance(markerPos, alt.Player.local.pos) < 1.5 && !this.hit) {
				alt.emitServer("JobTrash:onVehicleMarkerHit", this.collector)
				this.hit = true;
			} else {
				if(this.hit) this.hit = false;
			}
		})
	}
}

export default WasteCollector;
