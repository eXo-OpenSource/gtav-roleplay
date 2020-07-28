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
                if (player.vehicle) {
                    alt.toggleGameControls(false)
                    this.uiManager.navigate('/vehicleui', true) 
                }
            }
        })

        alt.on('keyup', (key) => {
            if (key === 0x58) {
                if (option == 'Fahrzeuginfo') {
                    alt.emitServer('Vehicle:GetInfo')
                } else if (option == 'Licht an/aus') {
                    alt.emitServer('Vehicle:ToggleLight')
                } else if (option == 'Motor an/aus') {
                    alt.emitServer('Vehicle:ToggleEngine')
                }
                alt.toggleGameControls(true)
                this.uiManager.reset()    
                
            }
        })
        
        this.uiManager.on('VehicleUI:UpdateData', (_option) => {
            option = _option
        })

    }
}