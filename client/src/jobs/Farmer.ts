import * as alt from "alt-client"
import * as native from "natives"
import { UiManager } from "../ui/UiManager"
import { Singleton } from "../utils/Singleton"
import { Cursor } from "../utils/Cursor"

@Singleton
export class Farmer {
  private uiManager: UiManager
  private open = false

  public constructor(uiManager) {
    this.uiManager = uiManager;

    this.uiManager.on("Farmer:SelectJob", this.jobSelected.bind(this))

    alt.onServer("Farmer:OpenGUI", () => {
      if (this.open) return
      this.uiManager.navigate("/farmer", true)
      alt.toggleGameControls(false)
      this.open = true
    })
  }

  jobSelected(jobId) {
    alt.emitServer("JobFarmer:Start", jobId)
    this.closeGUI()
  }

  closeGUI() {
    this.uiManager.reset()
    Cursor.show(false)
    alt.toggleGameControls(true)
    this.open = false
  }
}
