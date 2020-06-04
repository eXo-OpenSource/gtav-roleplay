import alt, {Entity, Player} from 'alt';
import * as native from 'natives';

let markers: Marker[] = []

export class Marker {

	public type;
	public pos;
	public color;
	public scale;
	public visible;

	constructor(type, pos, scale, color) {
		this.type = type;
		this.pos = pos;
		this.scale = scale;
		this.color = color;
	}

	public static createMarker(type, pos, scale, color, visible = true): Marker {
		let marker = new Marker(type, pos, scale, color);
		marker.visible = visible;
		markers.push(marker);
		return marker;
	}
}

alt.everyTick(() => {
	markers.forEach((marker) => {
		if(marker.visible) {
			native.drawMarker(marker.type, marker.pos.x, marker.pos.y, marker.pos.z, 0, 0, 0, 0, 0, 0,
				marker.scale, marker.scale, marker.scale,marker.color.r, marker.color.b, marker.color.g, marker.color.a,
				true, true, 2, false, null, null, false)
		}
	})
})

export default Marker;
