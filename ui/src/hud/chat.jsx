import React, { Component } from "react";

class Chat extends Component {

    constructor(props) {
        super(props);
        this.state = {
            messages: [
                { type: "chat", player: "gatno", msg: "Hallo wie geht es dir?" },
                { type: "chat", player: "deejaybro", msg: "eXo-RP ist so kuhl!" },
                { type: "chat", player: "gatno", msg: "Ja das stimmt!" }
            ],
            currentMessage: "",
            chatEnabled: false,
            chatVisible: false,
            chatBoxVisible: false
        };
    }

    componentDidMount() {
        if ("alt" in window) {
            alt.on("addMessage", this.addMessage.bind(this));
            alt.on("showChat", this.showChat.bind(this));
            alt.on("hideChat", this.hideChat.bind(this));
            alt.on("toggleChat", this.toggleChat.bind(this));
        }

        document.addEventListener("keyup", this.onKeyDown.bind(this), false);
    }

    onKeyDown(event) {
        switch (event.keyCode) {
            case 84:// Key: F
                this.setState({ chatBoxVisible: true });
                this.chatInput.focus();
                break;
            case 13: // Key: Enter
                this.sendMessage();
                break;
            case 27: // Key: ESC
                if (!this.state.chatBoxVisible) return;
                this.setState({ chatBoxVisible: false });
                break;
        }
    }

    sendMessage() {
        if (this.state.currentMessage.length <= 0) return;

        console.log("Chat:Message " + this.state.currentMessage)
        if ("alt" in window) {
            alt.emit('Chat:Message', this.state.currentMessage);
        }

        this.setState({ chatBoxVisible: false, currentMessage: "" });
    }

    addMessage(type, player, msg) {
        const messages = this.state.messages;
        messages.push({
            type: type,
            player: player,
            msg: msg
        });
        this.setState({ messages: messages});
    }

    onChatInputChange(event) {
        if (this.state.chatBoxVisible) {
            this.setState({ currentMessage: event.target.value });
        }
    }

    renderMessages() {
        return this.state.messages.map((value, key) => {
            return <div key={key} className="text-white" style={{ textShadow: "0px 0px 4px black"}}>
                <strong>{value.player}:</strong> {value.msg}
                   </div>
        });
    }

    render() {
        return <div className="w-1/4 container rounded px-6 py-6 ">
                   {this.renderMessages()}
                   {this.state.chatBoxVisible ? 
                <input
                    ref={(input) => { this.chatInput = input; }} 
                    className="border rounded w-full appearance-none py-2"
                    value={this.state.currentMessage}
                    onChange={this.onChatInputChange.bind(this)}
                    type="text" />
            :null}
               </div>;
    };
}

export default Chat;
