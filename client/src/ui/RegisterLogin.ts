import * as alt from 'alt';
import {UiManager} from "./UiManager";

const url = 'http://resource/cef/index.html#/login';


export class RegisterLogin {
    private uiManager: UiManager;

    public constructor(uiManager) {

    	this.uiManager = uiManager;

        // Setup Webview
        //this.uiManager.writeChat(url);

		alt.toggleGameControls(false)
        this.uiManager.navigate("/login", true)
        this.uiManager.on('login', (username: string, password: string) => {
            alt.emitServer('RegisterLogin:Login', username, password);
        });

        alt.onServer("registerLogin:Error", (error) => {
            this.uiManager.emit("setError", error)
        });

        alt.onServer("registerLogin:Success", () => {
            this.uiManager.reset()
			alt.toggleGameControls(true)
        });
    }
}
