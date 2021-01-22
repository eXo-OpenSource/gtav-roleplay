import alt, {Entity, Player} from 'alt-client';
import * as native from 'natives';

let markers: Marker[] = []

export class Marker {
  public type;
  public pos;
  public color;
  public scale;
  public visible;
  public bobUpAndDown;

  constructor(type, pos, scale, color) {
    this.type = type;
    this.pos = pos;
    this.scale = scale;
    this.color = color;
  }

  public delete(): boolean {
    try {
      markers[markers.indexOf(this)] = null;
      return true;
    } catch {
      return false;
    }
  }

  public static createMarker(type, pos, scale, color, visible = true, bobUpAndDown = false): Marker {
    let marker = new Marker(type, pos, scale, color);
    marker.visible = visible;
    marker.bobUpAndDown = bobUpAndDown;
    markers.push(marker);
    return marker;
  }
}

alt.everyTick(() => {
  markers.forEach((marker) => {
    if(marker?.visible) {
      native.drawMarker(marker.type, marker.pos.x, marker.pos.y, marker.pos.z, 0, 0, 0, 0, 0, 0,
        marker.scale, marker.scale, marker.scale, marker.color.r, marker.color.b, marker.color.g, marker.color.a,
        marker.bobUpAndDown, true, 2, false, null, null, false)
      native.drawRect(0, 0, 0, 0, 0, 0, 0, 0, false)
    }
  })
})

export default Marker;
