import * as alt from "alt";
import { Singleton } from "./utils/Singleton";
import { RegisterLogin } from './ui/RegisterLogin';
import { UiManager } from './ui/UiManager';
import { log } from "util";
import {Vehicle} from "./systems/Vehicle";

@Singleton
export class Core {
    //private registerLoging: RegisterLogin = new RegisterLogin();
    private vehicle = new Vehicle();

    constructor() {
        UiManager.loadEvents();
        
        
        alt.log('Loaded: client.mjs');

        alt.on('consoleCommand', () => {
            alt.log('consoleCommand');
            alt.emitServer("ClientConnectionComplete", "Test")
        })
        

    }
}