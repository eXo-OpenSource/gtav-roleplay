import * as alt from "alt-client"
import * as native from "natives"
import { UiManager } from "../ui/UiManager"

export class CarRent {
    private uiManager: UiManager
    private player = alt.Player.local

    public constructor(uiManager) {
        this.uiManager = uiManager
        this.uiManager.navigate("/carrent", true)
        
        this.uiManager.on("CarRent:Rent", (vehicle: string) => {
            alt.emitServer("CarRent:SpawnVehicle", vehicle)
            alt.toggleGameControls(true)
        })

        alt.on("keyup", (key: number) => {
            if (key === 32) {
                this.uiManager.reset()
                alt.toggleGameControls(false)
            }
        })
    }
}