import * as alt from "alt";
import { Singleton } from "./utils/Singleton";
import { RegisterLogin } from './ui/RegisterLogin';
import { UiManager } from './ui/UiManager';
import { log } from "util";

@Singleton
export class Core {
    private registerLoging: RegisterLogin = new RegisterLogin();

    constructor() {
        UiManager.loadEvents();
        
        
        alt.log('Loaded: client.mjs');

        alt.on('consoleCommand', () => {
            alt.log('consoleCommand');
            alt.emitServer("ClientConnectionComplete", "Test")
        })
        

    }
}