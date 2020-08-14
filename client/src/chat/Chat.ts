import * as alt from 'alt';
import * as natives from 'natives';
import {UiManager} from "../ui/UiManager";

export class Chat {
	private uiManager: UiManager;
	private loaded = false;
	private opened = false;
	private hidden = false;
	private readonly buffer;

	constructor(uiManager) {
		this.uiManager = uiManager;
		this.buffer = [];

		this.uiManager.on("Chat:Loaded", () => {
			for(const msg of this.buffer) {
				this.addMessage(msg.name, msg.text);
			}
			this.loaded = true;
		})

		this.uiManager.on("Chat:Message", (text) => {
			alt.emitServer("Chat:Message", text)
			this.opened = false;

			alt.toggleGameControls(true);
		})

		alt.onServer('Chat:Message', this.pushMessage.bind(this));
		alt.onServer('Chat:Hide', this.hide.bind(this));

		alt.on('keyup', (key) => {
			if (!this.loaded)
				return;

			if (!this.opened && key === 0x54 && alt.gameControlsEnabled()) { // T
				this.opened = true;
				this.uiManager.emit('Chat:Open', true);
				alt.toggleGameControls(false);
			}
			else if (!this.opened && key === 0xBF && alt.gameControlsEnabled()) { // /
				this.opened = true;
				this.uiManager.emit('Chat:Open', true);
				alt.toggleGameControls(false);
			}
			else if (this.opened && key == 0x1B) { // Esc
				this.opened = false;
				this.uiManager.emit('Chat:Open', false);
				alt.toggleGameControls(true);
			}

			if (key == 0x76) {
				this.hidden = !this.hidden;
				// natives.displayHud(!this.hidden);
				// natives.displayRadar(!this.hidden);
				this.uiManager.emit('Chat:Visible', this.hidden);
			}
		});
	}

	hide(state) {
		this.hidden = !state
		this.uiManager.emit('Chat:Visible', !state)
	}

	pushMessage(name, text) {
		if (!this.loaded) {
			this.buffer.push({ name, text });
		} else {
			this.addMessage(name, text);
		}
	}

	pushLine(text) {
		this.pushMessage(null, text);
	}

	isHidden() {
		return this.hidden;
	}

	isOpen() {
		return this.opened;
	}

	private addMessage(name, text) {
		if(name) {
			this.uiManager.emit("addMessage", 'chat', name, text)
		} else {
			this.uiManager.emit("addMessage", 'chat', null, text)
		}
	}

}

export default Chat;
