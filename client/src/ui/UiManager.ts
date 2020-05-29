import * as alt from 'alt';
import { FaceFeaturesUi } from './FaceFeaturesUi';
import { RegisterLogin } from './RegisterLogin';
import {View} from "../utils/View";
import Chat from "../chat/Chat";
import {Cursor} from "../utils/Cursor";
import {HUD} from "./HUD";
import Speedo from "./Speedo";

const url = 'http://resource/cef/index.html#';

export class UiManager {
	private mainView: View;
	private chat: Chat;
	private hud: HUD;
	private speedo: Speedo;

	constructor() {
		this.mainView = new View()
		this.mainView.open(url, false, true);

		this.chat = new Chat(this);
		this.hud = new HUD(this);
		this.speedo = new Speedo(this);
		this.loadEvents()
	}

    loadEvents() {
        alt.log('Loaded: UI Manager Events');


        alt.onServer('Ui:ShowFaceFeatures', () => new FaceFeaturesUi());

        alt.onServer('Ui:ShowRegisterLogin', () => new RegisterLogin(this));

        this.mainView.on("ready", () => alt.emitServer("ready"))
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

	navigate(subUrl, cursor) {
		this.mainView.emit("locationChange", subUrl);
		if(cursor) {
			Cursor.show(true);
		}
	}

}
