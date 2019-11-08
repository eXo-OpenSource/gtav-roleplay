import * as alt from 'alt';
import { RegisterLogin } from './RegisterLogin';

alt.log('Loaded: client.mjs');

alt.on('consoleCommand', () => {
    alt.log('consoleCommand');
    alt.emitServer("ClientConnectionComplete", "Test")
})

alt.onServer("showLogin", () => {
    RegisterLogin.show();
});