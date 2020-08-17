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
          this.uiManager.navigate('/vehicleui', true)
          native.playSoundFrontend(-1, 'CONTINUE', 'HUD_FRONTEND_DEFAULT_SOUNDSET', true)

          if (player.vehicle) {
            this.uiManager.emit('VehicleUI:ChangeUI', 'inVehicle')
          } else {
            this.uiManager.emit('VehicleUI:ChangeUI', 'outVehicle')
          }
        }
      }
    })

    alt.on('keyup', (key) => {
      if (key === 0x58) {
        var vehicle = native.getClosestVehicle(player.pos.x, player.pos.y, player.pos.z, 5, 0, 70) || player.vehicle.scriptID

        if (vehicle) {
          alt.toggleGameControls(true)
          this.uiManager.reset()
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
            case 'Auf-/Zuschließen':
              alt.emitServer('Vehicle:ToggleLock', (player.vehicle ? entity.getByScriptID(player.vehicle.scriptID) : entity.getByScriptID(vehicle)))
              break;
          }
        }
      }
    })

    this.uiManager.on('VehicleUI:UpdateData', (_option) => {
      option = _option
    })

  }
}
