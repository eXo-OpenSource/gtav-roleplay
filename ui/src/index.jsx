import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { HashRouter, Route, Switch, Link } from "react-router-dom";
import Chat from "./hud/chat";
import loadable from "@loadable/component";

import './root.css';

const LoadableLoginComponent = loadable(() => import("./forms/login"));
const LoadableCharacterCreatorComponent = loadable(() => import("./forms/character-creator"));

class App extends Component {
    render() {
        return (
			<div>
				<HashRouter>
					<Switch>
						<Route exact path="/login" component={LoadableLoginComponent} />
						<Route exact path="/charactercreator" component={LoadableCharacterCreatorComponent} />
					</Switch>

					<Link to="/login">Login</Link><br/>
					<Link to="/charactercreator">Character creator</Link>
				</HashRouter>
				<Chat/>
			</div>
		)
    }
}

ReactDOM.render(<App />, document.getElementById('root'));
