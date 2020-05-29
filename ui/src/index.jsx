import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import {HashRouter as Router, Route, Switch, Link, withRouter} from "react-router-dom";
import Chat from "./hud/chat";
import HUD from "./hud/hud";
import CharacterCreatorForm from "./forms/character-creator";
import loadable, { Options as LoadableOptions } from "@loadable/component";

import './root.css';
import Speedometer from "./hud/speedometer";

const loadableOptions = { };
const LoadableLoginComponent = loadable(() => import("./forms/login"), loadableOptions);
const LoadableCharacterCreatorComponent = loadable(() => import("./forms/character-creator"), loadableOptions);

class App extends Component {
	constructor(props) {
		super(props)
		this.routerRef = React.createRef()
	}

    render() {
        return (
			<div>
				<Router ref={this.routerRef}>
					<Switch>
						<Route path="/login" component={LoadableLoginComponent} />
						<Route path="/charactercreator" component={LoadableCharacterCreatorComponent} />
					</Switch>
				</Router>
				<Chat/>
				<HUD/>
				<Speedometer/>
			</div>
		)
    }

    componentDidMount() {
		if ("alt" in window) {
			alt.on("locationChange", this.changeLocation.bind(this));
			alt.emit("ready");
		}
	}

	changeLocation(url) {
    	console.log("debug")
		this.routerRef.current.history.push(url)
	}
}

ReactDOM.render(<App />, document.getElementById('root'));
