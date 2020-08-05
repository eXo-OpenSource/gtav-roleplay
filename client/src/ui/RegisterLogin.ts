import * as alt from 'alt';
import * as native from 'natives';
import { Vector3 } from "natives"
import { UiManager } from "./UiManager";
import { Camera } from "../utils/Camera"

const url = 'http://resource/cef/index.html#/login';


export class RegisterLogin {
    private uiManager: UiManager;
    private camera: Camera;

    private cameraPoint: Vector3 = {
        x: -80,
        y: -825.03,
        z: 328.67
    }

    public constructor(uiManager) {

        this.uiManager = uiManager;
        this.camera = new Camera(this.cameraPoint, 17)
        this.camera.pointAtCoord(this.cameraPoint)

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
            this.camera.destroy()
			alt.toggleGameControls(true)
        });

        alt.nextTick(() => {
            native.displayRadar(false)
        })
    }
}
