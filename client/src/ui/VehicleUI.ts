import * as alt from 'alt'
import * as native from 'natives'
import { UiManager } from './UiManager'
import { Vector3 } from 'natives'
import { Singleton } from '../utils/Singleton'

const url = 'http://resource/cef/index.html#/vehicleui'

export class VehicleUI {
    private uiManager: UiManager

    public constructor(uiManager) {
        this.uiManager = uiManager
        const player = alt.Player.local
        let option = ''

        alt.on('keydown', (key) => {
            if (key === 0x58 && alt.gameControlsEnabled()) {
                var vehicle = native.getClosestVehicle(player.pos.x, player.pos.y, player.pos.z, 5, 0, 70)
                if (!vehicle) return
                alt.toggleGameControls(false)
                this.uiManager.navigate('/vehicleui', true)    
                native.playSoundFrontend(-1, 'CONTINUE', 'HUD_FRONTEND_DEFAULT_SOUNDSET', true)

                if (player.vehicle) {
                    this.uiManager.emit('VehicleUI:ChangeUI', 'inVehicle')
                } else {
                    this.uiManager.emit('VehicleUI:ChangeUI', 'outVehicle')
                }
            }
        })

        alt.on('keyup', (key) => {
            if (key === 0x58) {
                var veh = native.getClosestVehicle(alt.Player.local.pos.x, alt.Player.local.pos.y, alt.Player.local.pos.z, 5, 0, 70)
                if (!veh) return
                alt.toggleGameControls(true)
                this.uiManager.reset()

                if (option == 'Fahrzeuginfo') {
                    alt.emitServer('Vehicle:GetInfo')
                } else if (option == 'Licht an/aus') {
                    alt.emitServer('Vehicle:ToggleLight')
                } else if (option == 'Motor an/aus') {
                    alt.emitServer('Vehicle:ToggleEngine')
                } else if (option == 'Auf-/Zuschließen') {
                    alt.emitServer('Vehicle:ToggleLock', alt.Entity.getByScriptID(veh))
                }

                native.playSoundFrontend(-1, 'EXIT', 'HUD_FRONTEND_DEFAULT_SOUNDSET', true)
            }
        })
        
        this.uiManager.on('VehicleUI:UpdateData', (_option) => {
            option = _option
        })

    }
}