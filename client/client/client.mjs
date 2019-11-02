/* eslint-disable no-unused-vars */
import * as alt from 'alt';
import * as native from 'natives';
import { RegisterLogin } from '/client/RegisterLogin.mjs'
import * as chat from '/client/chat/Chat.mjs';

alt.log('Loaded: client.mjs');

alt.on('consoleCommand', () => {
    alt.log('consoleCommand');
    alt.emitServer("ClientConnectionComplete", "Test")
})

alt.onServer("showLogin", () => {
    RegisterLogin.show();
});



//RegisterLogin.show();
