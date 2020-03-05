import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { HashRouter, Route } from "react-router-dom";
import LoginForm from "./forms/login";
import Chat from "./hud/chat";

import './root.css';


class App extends Component {
    render() {
        return <div>
            <Chat></Chat>

            <HashRouter>
                <Route path="/login" component={LoginForm} />
            </HashRouter>

       </div> 
    }
}

ReactDOM.render(<App />, document.getElementById('root'));
