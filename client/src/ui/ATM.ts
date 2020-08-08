import * as alt from "alt"
import * as native from "natives"
import {UiManager} from "../ui/UiManager"
import { Singleton } from "../utils/Singleton";

@Singleton
export class ATM {
    private uiManager: UiManager

    public constructor(uiManager) {
        this.uiManager = uiManager;
        this.uiManager.navigate("/atm", true)
        alt.toggleGameControls(false)

        this.uiManager.on("ATM:Logout", this.logOut.bind(this))
        this.uiManager.on("ATM:CashIn", this.cashIn.bind(this))
        this.uiManager.on("ATM:CashOut", this.cashOut.bind(this))

        alt.emitServer("BankAccount:RefreshData")
        
        alt.onServer("BankAccount:UpdateData", this.updateData.bind(this))
    }

    updateData(_bankmoney, _money) {
        this.uiManager.emit("ATM:SetData", "bankmoney", _bankmoney)
        this.uiManager.emit("ATM:SetData", "money", _money)
        alt.emitServer("BankAccount:RefreshData")
    }

    cashIn(_amount) {
        alt.emitServer("BankAccount:CashIn", Number(_amount))
        alt.emitServer("BankAccount:RefreshData")
    }

    cashOut(_amount) {
        alt.emitServer("BankAccount:CashOut", Number(_amount))
        alt.emitServer("BankAccount:RefreshData")
    }

    logOut() {
        this.uiManager.reset()
        alt.toggleGameControls(true)
    }
}