import alt, {BaseObject, Entity, Player, PointBlip, Vector3, WorldObject} from 'alt-client';
import * as native from 'natives';
import {StreamedEntity} from "../systems/Streamer";

let globalBlips = []

export class Blip extends alt.PointBlip {
  constructor(entity: StreamedEntity) {
    super(entity.position.x, entity.position.y, entity.position.z);
    this.sprite = entity.data.sprite;
    this.name = entity.data.name;
  }
}

// Testing, remove later
alt.onServer("gotoWayPoint", () => {
  const waypoint = native.getFirstBlipInfoId(8)
  if (!waypoint) return
  let xyz = native.getBlipInfoIdCoord(waypoint)
  alt.emitServer("Dev:GotoWaypoint", xyz.x, xyz.y, xyz.z)
})

alt.onServer("globalBlips:init", (blips) => {
  //alt.log(JSON.stringify(blips))
  blips.forEach(b => {
    //console.log(b.name)
    let blip = new PointBlip(b.x, b.y, b.z)
    blip.sprite = +b.sprite;
    blip.color = +b.color;
    blip.scale = +b.scale;
    blip.name = b.name;
    blip.shortRange = false;
    globalBlips.push(blip)
  })
})

export default Blip;
