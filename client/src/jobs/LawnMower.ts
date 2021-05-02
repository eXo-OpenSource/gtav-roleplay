import alt, {Entity, Player, Vector3, Vehicle, WorldObject} from 'alt-client'
import * as native from 'natives'
import Marker from "../systems/Marker"
import {distance} from "../utils/Vector"

class LawnMower {
  private static player = alt.Player.local
  private static marker: Marker
  private static hit = false
  private static mower: number = null
  private static sprint = true

  static handleStartJob() {

    alt.onServer("JobLawn:SetWaypoint", LawnMower.setWayPoint)
    alt.onServer("JobLawn:DelWaypoint", LawnMower.deleteWayPoint)

    LawnMower.sprint = false
    alt.everyTick(LawnMower.enableSprint.bind(LawnMower))
    alt.setInterval(LawnMower.onMarkerHit.bind(LawnMower), 300)
  }

  static handleStopJob() {
    LawnMower.sprint = true

    alt.offServer("JobLawn:SetWaypoint", LawnMower.setWayPoint)
    alt.offServer("JobLawn:DelWaypoint", LawnMower.deleteWayPoint)

    alt.clearEveryTick(LawnMower.enableSprint.bind(LawnMower))
    alt.clearInterval(LawnMower.onMarkerHit.bind(LawnMower))
  }

  static handleSyncMetaChange(player: Player, key: string, value: any) {
    if (key == "lawnJob.syncMower") {
      if (value == "attach") {
        native.requestModel(447976993)
        alt.setTimeout(() => {
          LawnMower.mower = native.createObject(447976993, player.pos.x, player.pos.y, player.pos.z, true, true, false)
          native.attachEntityToEntity(LawnMower.mower, player.scriptID, native.getPedBoneIndex(player.scriptID, alt.hash("0x0")), 0, 1, -1, 0, 0, 180, false, false, true, true, 2, true)
        }, 300)
      } else {
        native.detachEntity(LawnMower.mower, false, false)
        native.deleteObject(LawnMower.mower)
      }
    }
  }

  static enableSprint() {
    if (LawnMower.sprint) {
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

  static setWayPoint(x: number, y: number, z: number) {
    LawnMower.marker = Marker.createMarker(30, new Vector3(x, y, z), 1, {r: 25, g: 175, b: 0, a: 225})
    native.setNewWaypoint(x, y)
  }

  static deleteWayPoint() {
    LawnMower.marker.visible = false
  }

  static onMarkerHit() {
    if (!LawnMower.player || !LawnMower.player.valid || !LawnMower.marker) return

    if (distance(LawnMower.marker.pos, LawnMower.player.pos) < 1 && !LawnMower.hit)
      alt.emitServer("JobLawn:OnMarkerHit")
    else if (LawnMower.hit) LawnMower.hit = false
  }
}

alt.onServer("JobLawn:StartJob", LawnMower.handleStartJob)
alt.onServer("JobLawn:StopJob", LawnMower.handleStopJob)
alt.on("syncedMetaChange", LawnMower.handleSyncMetaChange)
