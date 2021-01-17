import * as alt from "alt-client"
import * as native from "natives"
import UiManager from "../ui/UiManager"
import { Singleton } from "../utils/Singleton"
import { Cursor } from "../utils/Cursor"

@Singleton
export class Farmer {
  private static open = false

  static openGUI() {
    if (this.open) return
      UiManager.navigate("/farmer", true)
      alt.toggleGameControls(false)
      this.open = true
  }

  static jobSelected(jobId) {
    alt.emitServer("JobFarmer:Start", jobId)
    this.closeGUI()
  }

  static closeGUI() {
    UiManager.reset()
    Cursor.show(false)
    alt.toggleGameControls(true)
    this.open = false
  }
}

UiManager.on("Farmer:SelectJob", Farmer.jobSelected)
alt.onServer("Farmer:OpenGUI", Farmer.openGUI)
