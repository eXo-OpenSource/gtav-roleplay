import * as alt from "alt-client"
import UiManager from "../ui/UiManager"

export class Cityhall {
  private static visible = false;

  static openUi(license) {
    if (!Cityhall.visible) {
        alt.toggleGameControls(false)
        UiManager.navigate("/cityhall-licenses", true)
        Cityhall.visible = true
    }
  }

  static resetUi() {
    alt.toggleGameControls(true)
    UiManager.reset()
    Cityhall.visible = false
  }

  static buyLicense(licenseId) {
    if (Cityhall.visible) {
        alt.emitServer("Cityhall:BuyLicense", licenseId)
    }
  }

  static closeUi() {
    if (Cityhall.visible) {
        Cityhall.resetUi()
    }
  }
}

alt.onServer("Cityhall:OpenUi", Cityhall.openUi)
UiManager.on("Cityhall:CloseUi", Cityhall.closeUi)
UiManager.on("Cityhall:BuyLicense", Cityhall.buyLicense)
