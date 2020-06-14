import { Singleton } from "../utils/Singleton";
import * as alt from 'alt'
import * as native from 'natives'

@Singleton
export class IPLManager {
	constructor() {
		this.loadEvents();
	}

	loadEvents() {
		alt.onServer("IPLManager:requestIPL", (ipls: string[]) => {
			ipls.forEach(ipl => {
				alt.requestIpl(ipl)
			})
		})

		alt.onServer("IPLManager:removeIPL", (ipls: string[]) => {
			ipls.forEach(ipl => {
				alt.removeIpl(ipl)
			})
		})
	}
}

export default IPLManager
