import * as alt from "alt-client"
import * as native from "natives"
import { UiManager } from "../ui/UiManager"
import { Singleton } from "../utils/Singleton"
import { Cursor } from "../utils/Cursor"

@Singleton
export class WoodCutter {
  private uiManager: UiManager
  private open = false

  public constructor(uiManager) {
    this.uiManager = uiManager;

    this.uiManager.on("WoodCutter:CloseGUI", this.closeGUI.bind(this))

    alt.onServer("WoodCutter:OpenGUI", () => {
      if (this.open) return
      this.uiManager.navigate("/woodcutter", true)
      alt.toggleGameControls(false)
      this.open = true
    })
  }

  closeGUI() {
    this.uiManager.reset()
    Cursor.show(false)
    alt.toggleGameControls(true)
    this.open = false
  }
}
