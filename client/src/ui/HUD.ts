import * as alt from 'alt'
import * as native from 'natives'
import { View } from '../utils/View'
import { Singleton } from '../utils/Singleton'

const url = 'http://resource/cef/index.html#/hud'

@Singleton
export class HUD {
    private webview: View

    public constructor() {
        if (!this.webview) {
            this.webview = new View()
        }
        this.webview.open(url, true)
        
        alt.setInterval(() => {
            let date = new Date()
            const dateOptions = { year: 'numeric', month: 'long', day: 'numeric' }
            let zone = native.getLabelText(native.getNameOfZone(alt.Player.local.pos.x, alt.Player.local.pos.y, alt.Player.local.pos.z))

            this.webview.emit("HUD:SetData", "location", zone)
            this.webview.emit("HUD:SetData", "date", date.toLocaleDateString("de-DE", dateOptions))
            this.webview.emit("HUD:SetData", "time", date.toLocaleTimeString("de-DE"))
        }, 1000)

        alt.onServer("HUD:Hide", (isHidden) => {
            this.webview.emit("HUD:SetData", "hidden", isHidden)
        })
    }

    
}