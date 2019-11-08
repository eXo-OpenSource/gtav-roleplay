import * as alt from 'alt';
import { Singleton } from "./utils/Singleton";
import { RegisterLogin } from './RegisterLogin';

@Singleton
export class Core {
    constructor() {
        alt.log('Loaded: client.mjs');

        alt.on('consoleCommand', () => {
            alt.log('consoleCommand');
            alt.emitServer("ClientConnectionComplete", "Test")
        })

        alt.onServer("showLogin", () => {
            RegisterLogin.show();
        });
    }
}