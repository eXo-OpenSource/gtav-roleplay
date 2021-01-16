import * as alt from 'alt-client'
import * as native from 'natives'
import { View } from '../utils/View'
import { Singleton } from '../utils/Singleton'
import { UiManager } from "./UiManager";

const url = 'http://resource/cef/index.html#/hud'

alt.onServer("HUD:Hide", (isHidden) => {
  UiManager.emit("HUD:SetData", "hidden", isHidden)
})

alt.onServer("HUD:ShowRadar", (show) => {
  alt.nextTick(() => {
    native.displayRadar(show)
    native.drawRect(0, 0, 0, 0, 0, 0, 0, 0, false)
  })
})

alt.everyTick(() => {
  native.displayAmmoThisFrame(false) // hides amount of ammo
  native.hideHudComponentThisFrame(20) // hides weapon stats ui

  if (native.isPedArmed(alt.Player.local.scriptID, 7)) {
    let selectedWeapon = native.getSelectedPedWeapon(alt.Player.local.scriptID)
    let ammoInWeapon = native.getAmmoInPedWeapon(alt.Player.local.scriptID, selectedWeapon)
    let [_, ammoInClip] = native.getAmmoInClip(alt.Player.local.scriptID, selectedWeapon, ammoInWeapon)

    UiManager.emit("HUD:SetData", "amount", ammoInClip + " / " + (ammoInWeapon - ammoInClip))
  } else {
    UiManager.emit("HUD:SetData", "amount", "$ " + HUD.money)
  }

  UiManager.emit("HUD:SetData", "kevlar", native.getPedArmour(alt.Player.local.scriptID))
  UiManager.emit("HUD:SetData", "health", native.getEntityHealth(alt.Player.local.scriptID)/2)
  UiManager.emit("HUD:SetData", "hunger", "75") // need hunger-system
})

alt.setInterval(() => {
  let date = new Date()
  // Fir
  /* date.setHours(native.getClockHours())
  date.setMinutes(native.getClockMinutes())
  date.setSeconds(native.getClockSeconds())
  date.setFullYear(native.getClockYear(), native.getClockMonth())
  date.setDate(native.getClockDayOfMonth()) */

  let zone = native.getLabelText(native.getNameOfZone(alt.Player.local.pos.x, alt.Player.local.pos.y, alt.Player.local.pos.z))

  UiManager.emit("HUD:SetData", "location", zone)
  UiManager.emit("HUD:SetData", "date", `${date.getDate()}.${date.getMonth() + 1}.${date.getFullYear()}`)
  UiManager.emit("HUD:SetData", "time", Date().slice(16, 21))
}, 300);

@Singleton
export class HUD {

    static money = 0;
    

    static updateMoney(money) {
      this.money = money;
    }

    public constructor() {
    let money = 0

    }
}

alt.onServer("HUD:UpdateMoney", HUD.updateMoney)
