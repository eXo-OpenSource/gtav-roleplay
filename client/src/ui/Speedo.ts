import * as alt from 'alt'
import * as native from 'natives'
import {Singleton} from "../utils/Singleton";
import {UiManager} from "./UiManager";

@Singleton
export class Speedo {
	private uiManager: UiManager;
	private active = false;

	public constructor(uiManager) {
		this.uiManager = uiManager;

		alt.everyTick(() => {
			if(alt.Player.local.vehicle == null) {
				if(this.active) {
					this.uiManager.emit("Speedo:SetData", "active", false);
					this.active = false;
				}
			} else {
				if(!this.active) {
					this.uiManager.emit("Speedo:SetData", "active", true);
					this.active = true;
				}
				const veh = alt.Player.local.vehicle;

				this.uiManager.emit("Speedo:SetData", "rpm", Math.round(veh.rpm*8000));
				this.uiManager.emit("Speedo:SetData", "speed", Math.round(native.getEntitySpeed(veh.scriptID)*3.6));
				this.uiManager.emit("Speedo:SetData", "gear", veh.gear);
			}
		})
	}
}

export default Speedo;