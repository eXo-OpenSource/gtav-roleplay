import alt, {BaseObject, Entity, Player, PointBlip, Vector3, WorldObject} from 'alt-client';
import * as native from 'natives';
import {StreamedEntity} from "./Streamer";

export class Blip {
	public static createFromEntity(entity: StreamedEntity): PointBlip {
		const blip = new PointBlip(entity.position.x, entity.position.y, entity.position.z);
		blip.sprite = entity.data.sprite;
		blip.name = entity.data.name;

		return blip;
	}
}

// Testing, remove later
alt.onServer("gotoWayPoint", () => {
	const waypoint = native.getFirstBlipInfoId(8)
	if (!waypoint) return
	let xyz = native.getBlipInfoIdCoord(waypoint)
	alt.emitServer("Dev:GotoWaypoint", xyz.x, xyz.y, xyz.z)
})

export default Blip;
