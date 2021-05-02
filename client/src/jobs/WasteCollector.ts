import alt, {Entity, Player, Vector3, Vehicle, WorldObject} from 'alt-client';
import * as native from 'natives';
import Marker from "../systems/Marker";
import {distance} from "../utils/Vector";

class WasteCollector {

  private static collector: Entity;
  private static marker: Marker = Marker.createMarker(22, new Vector3(50, 0, 0), 2, {r: 255, g: 255, b: 0, a: 125});
  private static hit = false;

  static handleStopJob(jobid) {
    WasteCollector.marker.visible = false;
  }

  static handleEveryTick() {
    if (!WasteCollector.collector || !WasteCollector.collector.valid) return;
    if (native.getEntitySpeed(WasteCollector.collector.scriptID) * 3.6 > 5) {
      WasteCollector.marker.visible = false;
      return;
    }

    const markerPos = native.getOffsetFromEntityInWorldCoords(WasteCollector.collector.scriptID, 0, -5, 0);

    if (distance(markerPos, WasteCollector.marker.pos) > 1) {
      WasteCollector.marker.pos = markerPos;
      WasteCollector.marker.visible = true;
    }

    if (distance(markerPos, alt.Player.local.pos) < 1.5 && !WasteCollector.hit) {
      alt.emitServer("JobTrash:onVehicleMarkerHit", WasteCollector.collector)
      WasteCollector.hit = true;
    } else {
      if (WasteCollector.hit) WasteCollector.hit = false;
    }
  }

  static handleStartJob(veh, max) {
    const interval = alt.setInterval(() => {
      const vehicle = alt.Vehicle.getByID(veh);
      if (vehicle) {
        WasteCollector.collector = vehicle;
        alt.clearInterval(interval)
      }
    }, 10);
  }
}

alt.everyTick(WasteCollector.handleEveryTick)
alt.onServer("Job:StopJob", WasteCollector.handleStopJob)
alt.onServer("JobTrash:SetVehicle", WasteCollector.handleStartJob);

