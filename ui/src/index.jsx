import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { HashRouter, Route, Switch, Link } from "react-router-dom";
import LoginForm from "./forms/login";
import Chat from "./hud/chat";
import CharacterCreatorForm from "./forms/character-creator";

import './root.css';

class App extends Component {
    render() {
        return (
			<div>
				<HashRouter>
					<Switch>
						<Route exact path="/login" component={LoginForm} />
						<Route exact path="/charactercreator" component={CharacterCreatorForm} />
					</Switch>
				</HashRouter>
				<Chat/>
			</div>
		)
    }
}

ReactDOM.render(<App />, document.getElementById('root'));
