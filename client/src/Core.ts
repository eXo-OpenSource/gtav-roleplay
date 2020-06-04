import * as alt from "alt";
import { Singleton } from "./utils/Singleton";
import { UiManager } from './ui/UiManager';
// import { log } from "util";
import {Vehicle} from "./systems/Vehicle";
import {Notification} from "./systems/Notification";
import Interaction from "./systems/Interaction";
import Streamer from "./systems/Streamer";
import WasteCollector from "./jobs/WasteCollector";

@Singleton
export class Core {
    private vehicle = new Vehicle();
    private notification = new Notification();
    private uiManager = new UiManager();
    private interaction = new Interaction(this.uiManager);
    private streamer = new Streamer();
    private waste = new WasteCollector();

    constructor() {

        alt.log('Loaded: client.mjs');

        alt.on('consoleCommand', () => {
            alt.log('consoleCommand');
            alt.emitServer("ClientConnectionComplete", "Test")
        })

    }
}

