import * as alt from 'alt-client';
import * as natives from 'natives';
import UiManager from "./UiManager";

export class Chat {
  private static loaded = false;
  private static opened = false;
  private static hidden = false;
  private static readonly buffer = [];

  static handleChatMessage(text: string) {
    alt.emitServer("Chat:Message", text)
      this.opened = false;

      alt.toggleGameControls(true);
  }

  static openChat() {
    if (!this.loaded)
        return;

    if(!this.opened && alt.gameControlsEnabled()) {
      this.opened = true;
      UiManager.emit('Chat:Open', true);
      alt.toggleGameControls(false);
    }
  }

  static loadChat() {
    for(const msg of this.buffer) {
      this.addMessage(msg.name, msg.text);
    }
    this.loaded = true;
  }

  static closeChat() {
    if (!this.loaded)
      return;

    if(this.opened) {
      this.opened = false;
      UiManager.emit('Chat:Open', false);
      alt.toggleGameControls(true);
    }
  }

  static hide() {
    this.hidden = !this.hidden
    UiManager.emit('Chat:Visible', this.hidden)
  }

  static pushMessage(name, text: string) {
    if (!this.loaded) {
      this.buffer.push({ name, text });
    } else {
      this.addMessage(name, text);
    }
  }

  static pushLine(text: string) {
    this.pushMessage(null, text);
  }

  static isHidden() {
    return this.hidden;
  }

  static isOpen() {
    return this.opened;
  }

  private static addMessage(name, text) {
    if(name) {
      UiManager.emit("addMessage", 'chat', name, text)
    } else {
      UiManager.emit("addMessage", 'chat', null, text)
    }
  }

}

UiManager.on("Chat:Message", Chat.handleChatMessage);
UiManager.on("Chat:Loaded", Chat.loadChat)

alt.onServer('Chat:Message', Chat.pushMessage);
alt.onServer('Chat:Hide', Chat.hide);

export default Chat;
