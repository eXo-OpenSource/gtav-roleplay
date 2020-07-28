import * as alt from 'alt'
import * as native from 'natives'
import { UiManager } from './UiManager'
import { Vector3 } from 'natives'

const url = 'http://resource/cef/index.html#/vehicleui'

export class VehicleUI {
    private uiManager: UiManager

    constructor(uiManager) {
        this.uiManager = uiManager

        alt.on('VehicleUI:UpdateData', this.handleOption.bind(this))

        alt.on('keydown', (key) => {
            if (key === 0x58) {
                this.uiManager.navigate('/vehicleui', true)
            }
        })

        alt.on('keyup', (key) => {
            if (key === 0x58) {
                this.uiManager.reset()
            }
        })
    }

    handleOption(option) {
        if (option == 'Fahrzeuginfo') {
            alt.log('huhuhu')
        }
    }
}