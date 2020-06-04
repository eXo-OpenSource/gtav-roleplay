import alt, {BaseObject, Entity, Player, PointBlip, Vector3, WorldObject} from 'alt';
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


export default Blip;
