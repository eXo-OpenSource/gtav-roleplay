import alt, {Entity, Player, Vector3, Vehicle, WorldObject} from 'alt-client'
import * as native from 'natives'
import Marker from "../systems/Marker"
import {distance} from "../utils/Vector"

export default class LawnMower {
  private player = alt.Player.local
  private marker: Marker
  private hit = false
  private mower = null
  private sprint = true

  constructor() {
    this.setWayPoint = this.setWayPoint.bind(this)
    this.deleteWayPoint = this.deleteWayPoint.bind(this)

    alt.onServer("JobLawn:StartJob", () => {
      alt.onServer("JobLawn:SetWaypoint", this.setWayPoint)
      alt.onServer("JobLawn:DelWaypoint", this.deleteWayPoint)

      this.sprint = false
      alt.everyTick(this.enableSprint.bind(this))
      alt.setInterval(this.onMarkerHit.bind(this), 300)
    })

    alt.onServer("JobLawn:StopJob", () => {
      this.sprint = true

      alt.offServer("JobLawn:SetWaypoint", this.setWayPoint)
      alt.offServer("JobLawn:DelWaypoint", this.deleteWayPoint)

      alt.clearEveryTick(this.enableSprint.bind(this))
      alt.clearInterval(this.onMarkerHit.bind(this))
    })

    alt.on("syncedMetaChange", (player: Player, key: string, value: any) => {
      if (key == "lawnJob.syncMower") {
        if (value == "attach") {
          native.requestModel(447976993)
          alt.setTimeout(() => {
            this.mower = native.createObject(447976993, player.pos.x, player.pos.y, player.pos.z, true, true, false)
            native.attachEntityToEntity(this.mower, player.scriptID, native.getPedBoneIndex(player.scriptID, alt.hash("0x0")), 0, 1, -1, 0, 0, 180, false, false, true, true, 2, true)
          }, 300)
        } else {
          native.detachEntity(this.mower, false, false)
          native.deleteObject(this.mower)
        }
      }
    })

    // Output position for adding later more markers
    alt.on("consoleCommand", (cmd, ...arg) => {
      if (cmd == "pos") {
        alt.log("new Position(" + this.player.pos.x + "f, " + this.player.pos.y + "f, " + this.player.pos.z + "f),")
      }
    })
  }

  enableSprint() {
    if (this.sprint) {
      native.enableControlAction(0, 21, true)
      native.enableControlAction(0, 22, true)
      native.enableControlAction(0, 23, true)
      native.enableControlAction(0, 24, true)
    } else {
      native.disableControlAction(0, 21, true)
      native.disableControlAction(0, 22, true)
      native.disableControlAction(0, 23, true)
      native.disableControlAction(0, 24, true)
    }
  }

  setWayPoint(x: number, y: number, z: number) {
    this.marker = Marker.createMarker(30, new Vector3(x, y, z), 1, {r: 25, g: 175, b: 0, a: 225})
    native.setNewWaypoint(x, y)
  }

  deleteWayPoint() {
    this.marker.visible = false
  }

  onMarkerHit() {
    if (!this.player || !this.player.valid || !this.marker) return

    if (distance(this.marker.pos, this.player.pos) < 1 && !this.hit)
      alt.emitServer("JobLawn:OnMarkerHit")
    else
      if (this.hit) this.hit = false
  }
}
