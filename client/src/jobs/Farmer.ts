import * as alt from "alt-client"
import * as native from "natives"
import UiManager from "../ui/UiManager"
import { Singleton } from "../utils/Singleton"
import { Cursor } from "../utils/Cursor"

@Singleton
export class Farmer {
  private static open = false

  static openGUI() {
    if (Farmer.open) return
      UiManager.navigate("/farmer", true)
      alt.toggleGameControls(false)
      Farmer.open = true
  }

  static jobSelected(jobId) {
    alt.emitServer("JobFarmer:Start", jobId)
    Farmer.closeGUI()
  }

  static closeGUI() {
    UiManager.reset()
    Cursor.show(false)
    alt.toggleGameControls(true)
    Farmer.open = false
  }
}

UiManager.on("Farmer:SelectJob", Farmer.jobSelected)
alt.onServer("Farmer:OpenGUI", Farmer.openGUI)
