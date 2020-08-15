import * as alt from 'alt-client'
import * as native from 'natives'
import {UiManager} from "./UiManager";

export class Popup {
	private uiManager: UiManager
	private popup;

	constructor(uiManager) {
		this.uiManager = uiManager;

		this.uiManager.on("Popup:Click", (item) => {
			if(item == "Schließen") {
				this.close()
			} else {
				const pItem = this.popup.items.find(value => value.name === item);
				const args: Array<object> = pItem.callbackArgs;
				alt.emitServer(pItem.callback, ...args)
			}
		})

		alt.onServer("Popup:Show", (popup) => {
			this.popup = popup;
			popup.active = true;
			popup.items.push({id: 1, name: "Schließen", color: "red"})
			this.uiManager.emit("Popup:Data", popup);
		})

		alt.onServer("Popup:Close", () => {
			this.close()
		})
	}

	close() {
		this.popup = null;
		this.uiManager.emit("Popup:Close")
	}
}

export default Popup;
