import * as alt from "alt-client"
import UiManager from "../ui/UiManager"

export class Drivingschool {
  private static visible = false;

  static openUI(license) {
    if (!Drivingschool.visible) {
      alt.setTimeout(() => UiManager.emit("DrivingschoolUi:StartTest", license), 500)
      alt.setTimeout(() => {
        alt.toggleGameControls(false)
        UiManager.navigate("/drivingschool", true)
        Drivingschool.visible = true
      }, 250)
    }
  }

  static resetUI() {
    alt.toggleGameControls(true)
    UiManager.reset()
    Drivingschool.visible = false
  }

  static closeUI(score, license) {
    if (Drivingschool.visible) {
        Drivingschool.resetUI()
        alt.emitServer("Drivingschool:OnExamFinished", score, license)
    }
  }
}

alt.onServer("Drivingschool:OpenUi", Drivingschool.openUI)
UiManager.on("Drivingschool:CloseUi", Drivingschool.closeUI)
