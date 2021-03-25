import * as alt from "alt-client"
import UiManager from "../ui/UiManager"

export class Drivingschool {
  private static visible = false;

  static openUI() {
    if (!Drivingschool.visible) {
      alt.toggleGameControls(false)
      UiManager.navigate("/drivingschool", true)
      Drivingschool.visible = true
    }
  }

  static resetUI() {
    alt.toggleGameControls(true)
    UiManager.reset()
    Drivingschool.visible = false
  }

  static closeUI(score) {
    if (Drivingschool.visible) {
        Drivingschool.resetUI()
        alt.emitServer("Drivingschool:OnExamFinished", score)
    }
  }
}

alt.onServer("Drivingschool:OpenUi", Drivingschool.openUI)
UiManager.on("Drivingschool:CloseUi", (score) => Drivingschool.closeUI(score))
