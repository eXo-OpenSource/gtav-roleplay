import * as alt from 'alt-client'
import * as native from 'natives'
import { UiManager } from './UiManager'

export class VehicleUI {
  private uiManager: UiManager

  public constructor(uiManager) {
    this.uiManager = uiManager
    const player = alt.Player.local
    const entity = alt.Entity
    let option = ''

    alt.on('keydown', (key) => {
      if (key === 0x58 && alt.gameControlsEnabled()) {
        var vehicle = native.getClosestVehicle(player.pos.x, player.pos.y, player.pos.z, 5, 0, 70) || player.vehicle.scriptID

        if (vehicle) {
          alt.toggleGameControls(false)
          native.playSoundFrontend(-1, 'CONTINUE', 'HUD_FRONTEND_DEFAULT_SOUNDSET', true)

          if (player.vehicle) {
            this.uiManager.emit('InteractionMenu:UpdateItems', [
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
            this.uiManager.emit('InteractionMenu:UpdateItems', [
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
          this.uiManager.emit("InteractionMenu:ToggleShow", true)
        }
      }
    })

    alt.on('keyup', (key) => {
      if (key === 0x58) {
        var vehicle = native.getClosestVehicle(player.pos.x, player.pos.y, player.pos.z, 5, 0, 70) || player.vehicle.scriptID

        if (vehicle) {
          alt.toggleGameControls(true)
          this.uiManager.emit("InteractionMenu:ToggleShow", false)
          native.playSoundFrontend(-1, 'EXIT', 'HUD_FRONTEND_DEFAULT_SOUNDSET', true)

          switch(option) {
            case 'Fahrzeuginfo':
              alt.emitServer('Vehicle:GetInfo', (player.vehicle ? entity.getByScriptID(player.vehicle.scriptID) : entity.getByScriptID(vehicle)))
              break;
            case 'Licht an/aus':
              alt.emitServer('Vehicle:ToggleLight')
              break;
            case 'Motor an/aus':
              alt.emitServer('Vehicle:ToggleEngine')
              break;
            case 'Motorhaube auf/zu':
              if (native.getVehicleDoorAngleRatio(vehicle, 4) >= 0.1) {
                alt.emitServer('Vehicle:ToggleDoor', entity.getByScriptID(vehicle), 4, false)
              } else {
                alt.emitServer('Vehicle:ToggleDoor', entity.getByScriptID(vehicle), 4, true)
              }
              break;
            case 'Kofferraum auf/zu':
              if (native.getVehicleDoorAngleRatio(vehicle, 5) >= 0.1) {
                alt.emitServer('Vehicle:ToggleDoor', entity.getByScriptID(vehicle), 5, false)
              } else {
                alt.emitServer('Vehicle:ToggleDoor', entity.getByScriptID(vehicle), 5, true)
              }
              break;
            case 'Auf-/ Zuschließen':
              alt.emitServer('Vehicle:ToggleLock', (player.vehicle ? entity.getByScriptID(player.vehicle.scriptID) : entity.getByScriptID(vehicle)))
              break;
          }
        }
      }
    })

    this.uiManager.on('InteractionMenu:CurrentSelection', (_option) => {
      option = _option
    })

  }
}
