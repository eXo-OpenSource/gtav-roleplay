import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { HashRouter as Router, Route, Switch, Link } from "react-router-dom";
import Chat from "./hud/chat";
import HUD from "./hud/hud";
import loadable, { Options as LoadableOptions } from "@loadable/component";

import './root.css';
import Speedometer from "./hud/speedometer";

const loadableOptions = { };
const LoadableLoginComponent = loadable(() => import("./forms/login"), loadableOptions);
const LoadableCharacterCreatorComponent = loadable(() => import("./forms/character-creator"), loadableOptions);

class App extends Component {
    render() {
        return (
			<div>
				<Router>
					<Switch>
						<Route path="/login" component={LoadableLoginComponent} />
						<Route path="/charactercreator" component={LoadableCharacterCreatorComponent} />
					</Switch>
				</Router>
				<Chat/>
				<HUD/>
				{/* <Speedometer/> */}
			</div>
		)
    }
}

ReactDOM.render(<App />, document.getElementById('root'));
