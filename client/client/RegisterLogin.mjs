import * as alt from 'alt';
import { View } from '/client/utils/View.mjs';
import * as chat from '/client/chat/Chat.mjs';

const url = 'http://resource/cef/login/index.html';
var webview = undefined;

export class RegisterLogin {
    
    static show() {
        
        if (!webview) {
            webview = new View();
        }
    
        // Setup Webview
        chat.pushLine(url);

        webview.open(url, true);
        webview.on('login', RegisterLogin.login);
        
        alt.onServer("registerLogin:Error", (error) => {
            webview.emit("setError", error)
        });
        
        alt.onServer("registerLogin:Success", () => {
            webview.close();
        });
    }
    
    static login(username, password) {
        chat.pushLine('hello ' + username + ' PW: ' + password);
        chat.pushMessage('tacoGuy', 'hello');

        alt.emitServer('registerLogin:Login', username, password);
    }
}