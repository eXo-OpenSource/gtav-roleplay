import alt, {Entity, Player} from 'alt-client';
import * as native from 'natives';
import {distance} from "../utils/Vector";
import {UiManager} from "../ui/UiManager";

alt.log('Loaded: client->utility->vehicle.mjs');

export interface InteractionInstance {
	id: string
	title: string;
	text: string;
	callback: string;
	key: number;
}

export class Interaction {
	private uiManager: UiManager;
	private currInteraction: InteractionInstance[];

	constructor(uiManager) {
		this.uiManager = uiManager;
		this.currInteraction = [];

		alt.onServer("Interaction:Show", (id, title, text, callback) => {
			this.currInteraction = this.currInteraction.filter(value => value.id !== id);
			this.currInteraction.push({
				id: id,
				title: title,
				text: text,
				callback: callback,
				key: 69 //E
			});
			this.uiManager.insertToast(id, title, text)
		})

		alt.onServer("Interaction:Hide", (id) => {
			this.uiManager.removeToast(id)
			this.currInteraction = this.currInteraction.filter(value => value.id !== id);
		})

		alt.on("keyup", (key) => {
			if(!this.currInteraction) return;

			const interactionForKey = this.currInteraction.find(value => value.key == key);
			if(!interactionForKey) return;

			alt.emitServer(interactionForKey.callback);
		})
	}
}

export default Interaction;
