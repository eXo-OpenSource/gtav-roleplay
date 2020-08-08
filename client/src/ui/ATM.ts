import * as alt from "alt"
import * as native from "natives"
import {UiManager} from "../ui/UiManager"

export class ATM {
    private uiManager: UiManager

    public constructor(uiManager) {
        this.uiManager = uiManager;
        this.uiManager.navigate("/atm", true)

        this.uiManager.on("ATM:Logout", this.logOut.bind(this))
        this.uiManager.on("ATM:CashIn", this.cashIn.bind(this))
        this.uiManager.on("ATM:CashOut", this.cashOut.bind(this))
    }

    cashIn() {
        alt.log("CashIn")
    }

    cashOut() {
        alt.log("CashOut")
    }

    logOut() {
        alt.log("LogOut")
        this.uiManager.reset()
    }
}