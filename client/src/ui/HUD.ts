import * as alt from 'alt'
import * as native from 'natives'
import { View } from '../utils/View'
import { Singleton } from '../utils/Singleton'
import {UiManager} from "./UiManager";

const url = 'http://resource/cef/index.html#/hud'

@Singleton
export class HUD {
	private uiManager: UiManager;

    public constructor(uiManager) {
		this.uiManager = uiManager;
        alt.setInterval(() => {
            let date = new Date()
            const dateOptions = { year: 'numeric', month: 'long', day: 'numeric' }
            let zone = native.getLabelText(native.getNameOfZone(alt.Player.local.pos.x, alt.Player.local.pos.y, alt.Player.local.pos.z))

            this.uiManager.emit("HUD:SetData", "location", zone)
            this.uiManager.emit("HUD:SetData", "date", date.toLocaleDateString("de-DE", dateOptions))
            this.uiManager.emit("HUD:SetData", "time", date.toLocaleTimeString("de-DE"))
        }, 1000)

        alt.onServer("HUD:Hide", (isHidden) => {
            this.uiManager.emit("HUD:SetData", "hidden", isHidden)
        })
    }


}
