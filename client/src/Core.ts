import * as alt from "alt";
import { Singleton } from "./utils/Singleton";
import { UiManager } from './ui/UiManager';
// import { log } from "util";
import {Vehicle} from "./systems/Vehicle";
import {Notification} from "./systems/Notification";
import Interaction from "./systems/Interaction";
import Streamer from "./systems/Streamer";
import WasteCollector from "./jobs/WasteCollector";
import IPLManager from "./world/IPLManager";
import DoorManager from "./world/Doormanager";

@Singleton
export class Core {
    private vehicle = new Vehicle();
    private notification = new Notification();
    private uiManager = new UiManager();
    private interaction = new Interaction(this.uiManager);
    private streamer = new Streamer();
	private waste = new WasteCollector();
	private iplManager = new IPLManager()
	private doorManager = new DoorManager();

    constructor() {

        alt.log('Loaded: client.mjs');

        alt.on('consoleCommand', () => {
            alt.log('consoleCommand');
            alt.emitServer("ClientConnectionComplete", "Test")
        })

    }
}

