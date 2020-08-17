import * as alt from 'alt-client'
import * as native from 'natives'
import { View } from '../utils/View'
import { Singleton } from '../utils/Singleton'
import { UiManager } from "./UiManager";

const url = 'http://resource/cef/index.html#/hud'

@Singleton
export class HUD {
  private uiManager: UiManager;

    public constructor(uiManager) {
    this.uiManager = uiManager;
    let money = 0

    alt.everyTick(() => {
      native.displayAmmoThisFrame(false) // hides amount of ammo
      native.hideHudComponentThisFrame(20) // hides weapon stats ui

      if (native.isPedArmed(alt.Player.local.scriptID, 7)) {
        let selectedWeapon = native.getSelectedPedWeapon(alt.Player.local.scriptID)
        let ammoInWeapon = native.getAmmoInPedWeapon(alt.Player.local.scriptID, selectedWeapon)
        let [_, ammoInClip] = native.getAmmoInClip(alt.Player.local.scriptID, selectedWeapon, ammoInWeapon)

        this.uiManager.emit("HUD:SetData", "amount", ammoInClip + " / " + (ammoInWeapon - ammoInClip))
      } else {
        this.uiManager.emit("HUD:SetData", "amount", "$ "+ money)
      }

      let date = new Date()
      date.setHours(native.getClockHours())
      date.setMinutes(native.getClockMinutes())
      date.setSeconds(native.getClockSeconds())
      date.setFullYear(native.getClockYear(), native.getClockMonth())
      date.setDate(native.getClockDayOfMonth())

      const dateOptions = { year: 'numeric', month: 'long', day: 'numeric' }
      let zone = native.getLabelText(native.getNameOfZone(alt.Player.local.pos.x, alt.Player.local.pos.y, alt.Player.local.pos.z))

      this.uiManager.emit("HUD:SetData", "location", zone)
      this.uiManager.emit("HUD:SetData", "date", date.toLocaleDateString("de-DE", dateOptions))
      this.uiManager.emit("HUD:SetData", "time", date.toLocaleTimeString("de-DE"))
      this.uiManager.emit("HUD:SetData", "kevlar", native.getPedArmour(alt.Player.local.scriptID))
      this.uiManager.emit("HUD:SetData", "health", native.getEntityHealth(alt.Player.local.scriptID)/2)
      this.uiManager.emit("HUD:SetData", "hunger", "75") // need hunger-system
    })

    alt.onServer("HUD:Hide", (isHidden) => {
  this.uiManager.emit("HUD:SetData", "hidden", isHidden)
    })

    alt.onServer("HUD:UpdateMoney", (amount) => {
      money = amount
    })

    alt.onServer("HUD:ShowRadar", (show) => {
      alt.nextTick(() => {
        native.displayRadar(show)
        native.drawRect(0, 0, 0, 0, 0, 0, 0, 0, false)
      })
    })
    }
}
