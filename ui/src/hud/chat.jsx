import React, { Component } from "react";
import SimpleBar from 'simplebar-react';
import 'simplebar/dist/simplebar.min.css';

class Chat extends Component {

  constructor(props) {
    super(props);
    this.state = {
      messages: [
        { type: "chat", player: "gatno", msg: "Hallo wie geht es dir?" },
        { type: "chat", player: "deejaybro", msg: "eXo-RP ist so kuhl!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
        { type: "chat", player: "gatno", msg: "Ja das stimmt!" },
      ],
      currentMessage: "",
      chatEnabled: true,
      chatVisible: true,
      chatBoxVisible: false
    };
  }

  componentDidMount() {
    if ("alt" in window) {
      alt.on("addMessage", this.addMessage.bind(this));
      alt.on("Chat:Visible", this.setVisible.bind(this));
      alt.on("Chat:Open", this.openChatBox.bind(this))
      alt.on("setEnabled", this.setEnabled.bind(this));
      alt.on("clear", this.clear.bind(this));
      alt.emit("Chat:Loaded");
    }

    document.addEventListener("keyup", this.onKeyDown.bind(this), false);
    this.scrollToBottom("auto");
    setInterval(() => {
      this.scrollToBottom();
    }, 5000)
  }

  setVisible(state) {
    this.setState({
      chatVisible: state
    });
    if (state == false) {
      this.setState({
        chatBoxVisible: false,
        currentMessage: ""
      });
    }
  }

  setEnabled(state) {
    this.setState({
      chatEnabled: state,
    });
    if (state == false) {
      this.setState({
        chatBoxVisible: false,
        currentMessage: ""
      });
    }
  }

  clear() {
    this.setState({
      messages: [],
    });
  }

  scrollToBottom(behavior = "smooth") {
    this.msgEnd.scrollIntoView({ behavior: behavior });
  }

  openChatBox(toggle) {
    this.setState({ chatBoxVisible: toggle });
    if(toggle) {
      this.chatInput.focus();
    }
  }

  onKeyDown(event) {
    if (!this.state.chatEnabled) return;

    switch (event.keyCode) {
      case 84:// Key: F
        if(!("alt" in window)) {
          this.setState({chatBoxVisible: true});
          this.chatInput.focus();
        }
        break;
      case 13: // Key: Enter
        this.sendMessage();
        break;
      case 27: // Key: ESC
        if(!("alt" in window)) {
          if (!this.state.chatBoxVisible) return;
          this.setState({chatBoxVisible: false, currentMessage: ""});
        }
        break;
    }
  }

  sendMessage() {
    if (this.state.currentMessage.length <= 0) {
      this.setState({ chatBoxVisible: false });
      return;
    }

    console.log("Chat:Message " + this.state.currentMessage);
    if ("alt" in window) {
      alt.emit('Chat:Message', this.state.currentMessage);
    } else {
      this.addMessage('chat', 'Test', this.state.currentMessage);
    }

    this.setState({ chatBoxVisible: false, currentMessage: "" });
    this.scrollToBottom();
  }

  addMessage(type, player, msg) {
    const messages = this.state.messages;
    messages.push({
      type: type,
      player: player,
      msg: msg
    });
    this.setState({ messages: messages});
    this.scrollToBottom();
  }

  onChatInputChange(event) {
    if (this.state.chatBoxVisible) {
      this.setState({ currentMessage: event.target.value });
    }
  }

  renderMessages() {
    return this.state.messages.map((value, key) => {
      return (
        <div key={key} className="text-white mx-2" style={{ textShadow: "0px 0px 2px black" }}>
          {value.player ? <strong>{value.player}:</strong>: null} {value.msg}
        </div>
      );
    });
  }

  render() {
    return this.state.chatVisible ?
      (
        <div className="w-1/4 container rounded px-3 py-3 absolute top-0">
          <SimpleBar style={{ maxHeight: 94 }}>
            {this.renderMessages()}
            <div ref={(msgEnd) => { this.msgEnd = msgEnd; }} />
          </SimpleBar>
          {this.state.chatBoxVisible
            ? <input
              ref={(input) => { this.chatInput = input; }}
              className="border rounded w-full appearance-none py-1 px-2 opacity-75 bg-gray-400
              outline-none"
              value={this.state.currentMessage}
              onChange={this.onChatInputChange.bind(this)}
              type="text"
            />
            : null}
        </div>
      ) : null;
  }
}

export default Chat;
