import alt, {Entity, Player, Vector3, Vehicle, WorldObject} from 'alt'
import * as native from 'natives'
import Marker from "../systems/Marker"
import {distance} from "../utils/Vector"

export default class LawnMower {
    private player = alt.Player.local
    private marker: Marker
    private hit = false
    private mower = null

    constructor() {
        this.setWayPoint = this.setWayPoint.bind(this)
        this.deleteWayPoint = this.deleteWayPoint.bind(this)
        this.onMarkerHit = this.onMarkerHit.bind(this)

        alt.onServer("JobLawn:StartJob", () => {
            alt.onServer("JobLawn:SetWaypoint", this.setWayPoint)
            alt.onServer("JobLawn:DelWaypoint", this.deleteWayPoint)

            alt.setInterval(this.onMarkerHit, 300)
        })

        alt.onServer("JobLawn:StopJob", () => {
            alt.offServer("JobLawn:SetWaypoint", this.setWayPoint)
            alt.offServer("JobLawn:DelWaypoint", this.deleteWayPoint)

            alt.clearInterval(this.onMarkerHit.bind(this))
        })

        // Sync mower
        alt.on("syncedMetaChange", (player: Player, key: string, value: any) => {
            if (key == "lawnJob.syncMower") {
                native.requestModel(447976993)
                alt.setTimeout(() => {
                    this.mower = native.createObject(447976993, player.pos.x, player.pos.y, player.pos.z, true, true, false)
                    native.attachEntityToEntity(this.mower, player.scriptID, native.getPedBoneIndex(player.scriptID, alt.hash("0x0")), 0, 1, -1, 0, 0, 180, false, false, true, true, 2, true)
                }, 300)
            }
        })
    }

    setWayPoint(x, y, z) {
        this.marker = Marker.createMarker(1, new Vector3(x, y, z), 1, {r: 25, g: 175, b: 0, a: 225})
        native.setNewWaypoint(x, y)
    }

    deleteWayPoint() {
        this.marker.visible = false
    }

    onMarkerHit() {
        if (!this.player || !this.player.valid || !this.marker) return
        if (distance(this.marker.pos, this.player.pos) < 1 && !this.hit) {
            alt.emitServer("JobLawn:OnMarkerHit")
        } else {
            if (this.hit) this.hit = false
        }
    }
}