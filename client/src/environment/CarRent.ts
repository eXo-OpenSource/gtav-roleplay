import * as alt from "alt-client"
import * as native from "natives"
import UiManager from "../ui/UiManager"

export class CarRent {
  private static player = alt.Player.local
  private static visible = false;

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
}

alt.onServer("CarRent:CloseUI", CarRent.resetUI)
alt.onServer("CarRent:OpenUI", CarRent.openUI)
UiManager.on("CarRent:Rent", (vehicle: string, price: number) => {
  alt.emitServer("CarRent:SpawnVehicle", vehicle, price)
})
