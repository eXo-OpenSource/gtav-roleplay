import * as alt from 'alt-client'
import * as native from 'natives'
import UiManager from './UiManager'

export class VehicleUI {
  static player = alt.Player.local;
  static option = '';
  static entity = alt.Entity;

  static activateInteractionMenu() {
    if(alt.gameControlsEnabled()) {
      var vehicle = native.getClosestVehicle(VehicleUI.player.pos.x, VehicleUI.player.pos.y, VehicleUI.player.pos.z, 5, 0, 70) || VehicleUI.player.vehicle.scriptID

        if (vehicle) {
          alt.toggleGameControls(false)
          native.playSoundFrontend(-1, 'CONTINUE', 'HUD_FRONTEND_DEFAULT_SOUNDSET', true)

          if (VehicleUI.player.vehicle) {
            UiManager.emit('InteractionMenu:UpdateItems', [
              {
                title: "Fahrzeuginfo",
                img: "info"
              },
              {
                title: "Licht an/aus",
                img: "light"
              },
              {
                title: "Auf-/ Zuschließen",
                img: "key"
              },
              {
                title: "Schließen",
                img: "close"
              },
              {
                title: "Motor an/aus",
                img: "engine"
              }
            ])
          } else {
            UiManager.emit('InteractionMenu:UpdateItems', [
              {
                title: "Fahrzeuginfo",
                img: "info"
              },
              {
                title: "Auf-/ Zuschließen",
                img: "key"
              },
              {
                title: "Schließen",
                img: "close"
              },
              {
                title: "Motorhaube auf/zu",
                img: "engineHood"
              },
              {
                title: "Kofferraum auf/zu",
                img: "trunk"
              }
            ])
          }
          UiManager.emit("InteractionMenu:ToggleShow", true)
        }
    }
  }

  static deactivateInteractionMenu() {
    var vehicle = native.getClosestVehicle(VehicleUI.player.pos.x, VehicleUI.player.pos.y, VehicleUI.player.pos.z, 5, 0, 70) || VehicleUI.player.vehicle.scriptID

        if (vehicle) {
          alt.toggleGameControls(true)
          UiManager.emit("InteractionMenu:ToggleShow", false)
          native.playSoundFrontend(-1, 'EXIT', 'HUD_FRONTEND_DEFAULT_SOUNDSET', true)

          switch(VehicleUI.option) {
            case 'Fahrzeuginfo':
              alt.emitServer('Vehicle:GetInfo', (VehicleUI.player.vehicle ? VehicleUI.entity.getByScriptID(VehicleUI.player.vehicle.scriptID) : VehicleUI.entity.getByScriptID(vehicle)))
              break;
            case 'Licht an/aus':
              alt.emitServer('Vehicle:ToggleLight')
              break;
            case 'Motor an/aus':
              alt.emitServer('Vehicle:ToggleEngine')
              break;
            case 'Motorhaube auf/zu':
              if (native.getVehicleDoorAngleRatio(vehicle, 4) >= 0.1) {
                alt.emitServer('Vehicle:ToggleDoor', VehicleUI.entity.getByScriptID(vehicle), 4, false)
              } else {
                alt.emitServer('Vehicle:ToggleDoor', VehicleUI.entity.getByScriptID(vehicle), 4, true)
              }
              break;
            case 'Kofferraum auf/zu':
              if (native.getVehicleDoorAngleRatio(vehicle, 5) >= 0.1) {
                alt.emitServer('Vehicle:ToggleDoor', VehicleUI.entity.getByScriptID(vehicle), 5, false)
              } else {
                alt.emitServer('Vehicle:ToggleDoor', VehicleUI.entity.getByScriptID(vehicle), 5, true)
              }
              break;
            case 'Auf-/ Zuschließen':
              alt.emitServer('Vehicle:ToggleLock', (VehicleUI.player.vehicle ? VehicleUI.entity.getByScriptID(VehicleUI.player.vehicle.scriptID) : VehicleUI.entity.getByScriptID(vehicle)))
              break;
          }
        }
  }

  static setOption(_option) {
    VehicleUI.option = _option;
  }
}

UiManager.on('InteractionMenu:CurrentSelection', VehicleUI.setOption)
