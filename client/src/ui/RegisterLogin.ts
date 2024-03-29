import * as alt from 'alt-client';
import * as native from 'natives';
import { Vector3 } from "alt-client"
import UiManager from "./UiManager";
import { Camera } from "../utils/Camera"

const url = 'http://resource/cef/index.html#/login';

export class RegisterLogin {

  private static cameraPoint: Vector3 = new Vector3(
    -80,
    -825.03,
    328.67
);

  private static camera: Camera = new Camera(RegisterLogin.cameraPoint, 17);

  static openLogin() {
    RegisterLogin.camera.pointAtCoord(RegisterLogin.cameraPoint)

    alt.toggleGameControls(false)
    UiManager.navigate("/login", true)

    alt.onServer("registerLogin:Success", () => {
      UiManager.reset()
      RegisterLogin.camera.destroy()
      alt.toggleGameControls(true)
    });

    alt.nextTick(() => {
      native.displayRadar(false)
      native.drawRect(0, 0, 0, 0, 0, 0, 0, 0, false)
    })
  }
}

UiManager.on('login', (username: string, password: string) => {
  alt.emitServer('RegisterLogin:Login', username, password);
});

alt.onServer("registerLogin:Error", (error) => {
  UiManager.emit("setError", error)
});
alt.onServer('Ui:ShowRegisterLogin', RegisterLogin.openLogin);
