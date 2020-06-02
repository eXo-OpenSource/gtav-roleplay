import * as alt from "alt";
import { Singleton } from "./utils/Singleton";
import { RegisterLogin } from './ui/RegisterLogin';
import { HUD } from  './ui/HUD';
import { UiManager } from './ui/UiManager';
// import { log } from "util";
import {Vehicle} from "./systems/Vehicle";
import {Notification} from "./systems/Notification";
import Interaction from "./systems/Interaction";

@Singleton
export class Core {
    private vehicle = new Vehicle();
    private notification = new Notification();
    private uiManager = new UiManager();
    private interaction = new Interaction(this.uiManager);

    constructor() {

        alt.log('Loaded: client.mjs');

        alt.on('consoleCommand', () => {
            alt.log('consoleCommand');
            alt.emitServer("ClientConnectionComplete", "Test")
        })

    }
}

