import alt, {Entity, Player, Vector3, Vehicle, WorldObject} from 'alt'
import * as native from 'natives'
import Marker from "../systems/Marker"
import {distance} from "../utils/Vector"

export default class LawnMower {
    private player = alt.Player.local
    private marker: Marker
    private hit = false

    constructor() {
        alt.onServer("JobLawn:SetWaypoint", (x, y, z) => {
            this.marker = Marker.createMarker(1, new Vector3(x, y, z), 1, {r: 25, g: 175, b: 0, a: 225})
            native.setNewWaypoint(x, y)
            alt.log("[LawnMower]: Waypoint set")
        })
        
        alt.onServer("JobLawn:DelWaypoint", () => {
            this.marker.visible = false
            alt.log("[LawnMower]: Waypoint deleted")
        })

        alt.everyTick(() => {
            alt.log(this.hit)
            if (!this.player || !this.player.valid) return
            if (distance(this.marker.pos, this.player.pos) < 10 && !this.hit) {
                alt.emitServer("JobLawn:OnMarkerHit")
                this.hit = true
            } else {
                if (this.hit) this.hit = false
            }
        })
    }
}