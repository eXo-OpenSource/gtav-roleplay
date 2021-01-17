import * as alt from "alt-client"
import * as native from "natives"
import UiManager from "../ui/UiManager"
import { Singleton } from "../utils/Singleton"
import { Cursor } from "../utils/Cursor"

@Singleton
export class WoodCutter {
  private static open = false

  static openGUI() {
    if (this.open) return
      UiManager.navigate("/woodcutter", true)
      alt.toggleGameControls(false)
      this.open = true
  }

  static closeGUI() {
    UiManager.reset()
    Cursor.show(false)
    alt.toggleGameControls(true)
    this.open = false
  }
}

UiManager.on("WoodCutter:CloseGUI", WoodCutter.closeGUI)
alt.onServer("WoodCutter:OpenGUI", WoodCutter.openGUI)
