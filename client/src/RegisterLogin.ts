import * as alt from 'alt';
import { View } from './utils/View'
import * as chat from './chat/Chat';

const url = 'http://resource/cef/login/index.html';

export class RegisterLogin {
    private webview: View;

    public show() {
        
        if (!this.webview) {
            this.webview = new View();
        }
    
        // Setup Webview
        chat.pushLine(url);

        this.webview.open(url, true);
        this.webview.on('login', (username: string, password: string) => {
            alt.emitServer('registerLogin:Login', username, password);
        });
        
        alt.onServer("registerLogin:Error", (error) => {
            this.webview.emit("setError", error)
        });
        
        alt.onServer("registerLogin:Success", () => {
            this.webview.close();
        });
    }
}
