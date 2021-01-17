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
      Chat.opened = false;

      alt.toggleGameControls(true);
  }

  static openChat() {
    if (!Chat.loaded)
        return;

    if (!Chat.opened && alt.gameControlsEnabled()) {
      Chat.opened = true;
      UiManager.emit('Chat:Open', true);
      alt.toggleGameControls(false);
    }
  }

  static loadChat() {
    for(const msg of Chat.buffer) {
      Chat.addMessage(msg.name, msg.text);
    }
    Chat.loaded = true;
  }

  static closeChat() {
    if (!Chat.loaded)
      return;

    if(Chat.opened) {
      Chat.opened = false;
      UiManager.emit('Chat:Open', false);
      alt.toggleGameControls(true);
    }
  }

  static hide() {
    Chat.hidden = !Chat.hidden
    UiManager.emit('Chat:Visible', Chat.hidden)
  }

  static pushMessage(name, text: string) {
    if (!Chat.loaded) {
      Chat.buffer.push({ name, text });
    } else {
      Chat.addMessage(name, text);
    }
  }

  static pushLine(text: string) {
    Chat.pushMessage(null, text);
  }

  static isHidden() {
    return Chat.hidden;
  }

  static isOpen() {
    return Chat.opened;
  }

  private static addMessage(name, text) {
    if(name) {
      UiManager.emit("addMessage", 'chat', name, text)
    } else {
      UiManager.emit("addMessage", 'chat', null, text)
    }
  }

}

alt.onServer('Chat:Message', Chat.pushMessage);
alt.onServer('Chat:Hide', Chat.hide);

export default Chat;
