import * as alt from 'alt';
import { FaceFeaturesUi } from './FaceFeaturesUi';
import { RegisterLogin } from './RegisterLogin';
import { View } from "../utils/View";
import { Float } from "../utils/Float";
import Chat from "../chat/Chat";
import { Cursor } from "../utils/Cursor";
import { HUD } from "./HUD";
import Speedo from "./Speedo";
import Popup from "./Popup";

const url = 'http://resource/cef/index.html#';

export class UiManager {
	private mainView: View;
	private chat: Chat;
	private hud: HUD;
	private speedo: Speedo;
	private popup: Popup;

	constructor() {
		this.mainView = new View()
		this.mainView.open(url, false, true);

		this.chat = new Chat(this);
		this.hud = new HUD(this);
		this.speedo = new Speedo(this);
		this.popup = new Popup(this);
		this.loadEvents()
	}

    loadEvents() {
        alt.log('Loaded: UI Manager Events');


        alt.onServer('Ui:ShowFaceFeatures', () => new FaceFeaturesUi(this));

        alt.onServer('Ui:ShowRegisterLogin', () => new RegisterLogin(this));

        this.mainView.on("ready", () => alt.emitServer("ready"))

		alt.onServer("Toast:AddTimed", (name, text, time) => {
			this.insertTimedToast(name, text, time)
		})

		alt.onServer("Progress:Set", (val) => {
			this.emit("Progress:Set", val)
		})

		alt.onServer("Progress:Text", (text) => {
			this.emit("Progress:Text", text)
		})

		alt.onServer("Progress:Active", (toggle) => {
			this.emit("Progress:Active", toggle)
		})
    }

    reset() {
		this.navigate("/", false)
		Cursor.show(false)
	}

	emit(name, ...args) {
		this.mainView.emit(name, ...args);
	}

	on(name, func) {
		this.mainView.on(name, func);
	}

	writeChat(text) {
		this.chat.pushLine(text);
	}

	insertToast(id, title, text) {
		this.emit("Toast:Add", {
			id: id,
			title: title,
			text: text
		})
	}

	insertTimedToast(title, text, time: number) {
		const id = (Math.random()*1000)
		this.insertToast(id, title, text)

		alt.setTimeout(() => {
			this.removeToast(id)
		}, time)
	}

	removeToast(id) {
		this.emit("Toast:Remove", id)
	}

	navigate(subUrl, cursor) {
		this.mainView.emit("locationChange", subUrl);
		if(cursor) {
			Cursor.show(true);
		}
	}

}
