import * as alt from "alt-client"
import * as native from "natives"
import { UiManager } from "../ui/UiManager"

export class CarRent {
    private uiManager: UiManager
    private player = alt.Player.local
    private visible;

    public constructor(uiManager) {
        this.uiManager = uiManager
        this.visible = false;

        alt.onServer("CarRent:OpenUI", () => {
            alt.toggleGameControls(false)
            this.uiManager.navigate("/carrent", true)
            this.visible = true;
        })
        
        this.uiManager.on("CarRent:Rent", (vehicle: string) => {
            alt.emitServer("CarRent:SpawnVehicle", vehicle)
        })

        alt.on("keyup", (key: number) => {
            if (key === 32) {
                if (this.visible) {
                    this.uiManager.reset()
                    this.visible = false
                    alt.toggleGameControls(true)
                }
            }
        })
    }
}