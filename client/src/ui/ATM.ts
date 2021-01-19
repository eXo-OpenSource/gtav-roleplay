import * as alt from "alt-client"
import * as native from "natives"
import UiManager from "../ui/UiManager"
import { Singleton } from "../utils/Singleton"
import { Cursor } from "../utils/Cursor"

@Singleton
export class ATM {
  private static open = false


  static showATM() {
    UiManager.navigate("/atm", true)
      alt.toggleGameControls(false)
      ATM.open = true
  }

  static updateData(_bankmoney, _money, _normalizedName) {
    UiManager.emit("ATM:SetData", "bankmoney", _bankmoney)
    UiManager.emit("ATM:SetData", "money", _money)
    UiManager.emit("ATM:SetData", "name", _normalizedName)
    alt.emitServer("BankAccount:RefreshData")
  }

  static cashIn(_amount) {
    alt.emitServer("BankAccount:CashIn", Number(_amount))
    alt.emitServer("BankAccount:RefreshData")
  }

  static cashOut(_amount) {
    alt.emitServer("BankAccount:CashOut", Number(_amount))
    alt.emitServer("BankAccount:RefreshData")
  }

  static logOut() {
    UiManager.reset()
    Cursor.show(false)
    alt.toggleGameControls(true)
    alt.setTimeout(() => ATM.open = false, 1000)
  }
}

UiManager.on("ATM:Logout", ATM.logOut)
UiManager.on("ATM:CashIn", ATM.cashIn)
UiManager.on("ATM:CashOut", ATM.cashOut)

alt.onServer("ATM:Show", ATM.showATM)

alt.emitServer("BankAccount:RefreshData")

alt.onServer("BankAccount:UpdateData", ATM.updateData)
