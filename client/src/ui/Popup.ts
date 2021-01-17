import * as alt from 'alt-client'
import * as native from 'natives'
import UiManager from "./UiManager";

export class Popup {
  private static popup;

  static clickPopup(item) {
    if(item == "Schließen") {
      Popup.close()
    } else {
      const pItem = Popup.popup.items.find(value => value.name === item);
      const args: Array<object> = pItem.callbackArgs;
      alt.emitServer(pItem.callback, ...args)
    }
  }

  static showPopup(popup) {
    Popup.popup = popup;
    popup.active = true;
    popup.items.push({id: 1, name: "Schließen", color: "red"})
    UiManager.emit("Popup:Data", popup);
  }

  static close() {
    Popup.popup = null;
    UiManager.emit("Popup:Close")
  }
}

UiManager.on("Popup:Click", Popup.clickPopup)
alt.onServer("Popup:Show", Popup.showPopup)
alt.onServer("Popup:Close", Popup.close)

export default Popup;
