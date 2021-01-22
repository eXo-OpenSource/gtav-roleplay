import * as alt from "alt-client"
import * as native from "natives"
import UiManager from "../ui/UiManager"
import { Marker } from "../systems/Marker";
import { Vector3, Entity } from "alt-client";

export class CarRent {
  private static player = alt.Player.local
  private static visible = false;
  private static marker: Marker[] = [];
  private static rentMarker: Marker;
  private static currentGroup;

  static openUI() {
    if (!CarRent.visible) {
      alt.toggleGameControls(false)
      UiManager.navigate("/carrent", true)
      CarRent.visible = true
    }
  }

  static closeUI() {
    if(CarRent.visible) {
      CarRent.resetUI()
    }
  }

  static resetUI() {
    alt.toggleGameControls(true)
    UiManager.reset()
    CarRent.visible = false
  }

  static createMarker(x: number, y: number, z: number, group: number) {
    var marker = Marker.createMarker(36, new Vector3(x, y, z), 1, { r: 0, g: 255, b: 255, a: 200 });
    CarRent.marker[group.toString()] = marker;
    alt.log("Marker created [" + group + "] @" + x + ", " + y + ", " + z);
  }
  static createMarkerTest(x: number, y: number, z: number, group: number, id: number) {
    CarRent.marker.forEach((m) => {
      m.delete();
    });
    CarRent.marker = [];
    var marker = Marker.createMarker(id, new Vector3(x, y, z), 1, { r: 0, g: 255, b: 255, a: 200 });
    CarRent.marker[group.toString()] = marker;
    alt.log("Marker created [" + group + "] @" + x + ", " + y + ", " + z);
  }

  static createRentMarker(x: number, y: number, z: number) {
    var marker = Marker.createMarker(0, new Vector3(x, y, z + 2.5), 1, { r: 0, g: 255, b: 255, a: 200 }, true, true);
    CarRent.rentMarker = marker;
  }

  static deleteRentMarker() {
    if (CarRent.rentMarker) {
      CarRent.rentMarker.delete();
      CarRent.rentMarker = null;
    }
  }

  static updateGroup(group: string) {
    CarRent.currentGroup = group;
  }

  static syncVehicleRemove(entity: Entity, key: string, value: any) {
    if (key == "removeFromVehicle" && value == true) {
      native.taskLeaveVehicle(CarRent.player.scriptID, CarRent.player.vehicle.scriptID, 0);
    }
  }
}

alt.on("syncedMetaChange", CarRent.syncVehicleRemove);

alt.onServer("CarRent:CreateMarkerTest", CarRent.createMarkerTest);
alt.onServer("CarRent:CreateMarker", CarRent.createMarker);
alt.onServer("CarRent:UpdateGroup", CarRent.updateGroup);
alt.onServer("CarRent:CreateRentMarker", CarRent.createRentMarker);
alt.onServer("CarRent:DeleteRentMarker", CarRent.deleteRentMarker);

alt.onServer("CarRent:CloseUI", CarRent.resetUI)
alt.onServer("CarRent:OpenUI", CarRent.openUI)
UiManager.on("CarRent:Rent", (vehicle: string, price: number) => {
  alt.emitServer("CarRent:SpawnVehicle", vehicle, price)
})
