import * as alt from "alt-client"
import * as native from "natives"
import { UiManager } from "../ui/UiManager"

export class CarRent {
  private uiManager: UiManager
  private player = alt.Player.local
  private visible;

  public constructor(uiManager) {
    this.uiManager = uiManager
    this.visible = false

    alt.onServer("CarRent:OpenUI", () => {
      if (!this.visible) {
        alt.toggleGameControls(false)
        this.uiManager.navigate("/carrent", true)
        this.visible = true
      }
    })

    alt.onServer("CarRent:CloseUI", () => this.resetUI())

    this.uiManager.on("CarRent:Rent", (vehicle: string, price: number) => {
      alt.emitServer("CarRent:SpawnVehicle", vehicle, price)
    })

    alt.on("keyup", (key: number) => {
      if (key === 32 && this.visible)
        this.resetUI()
    })
  }

  resetUI() {
    alt.toggleGameControls(true)
    this.uiManager.reset()
    this.visible = false
  }
}
