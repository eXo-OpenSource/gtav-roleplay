import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import {HashRouter as Router, Route, Switch, Link, withRouter} from "react-router-dom";
import Chat from "./hud/chat";
import HUD from "./hud/hud";
import VehUI from "./hud/vehicle-ui"
import CharacterCreatorForm from "./forms/character-creator";
import loadable, { Options as LoadableOptions } from "@loadable/component";

import './root.css';
import Speedometer from "./hud/speedometer";
import Popup from "./hud/popup";
import Toast from "./hud/toast";
import Progress from "./hud/progress";

const loadableOptions = { };
const LoadableLoginComponent = loadable(() => import("./forms/login"), loadableOptions);
const LoadableCharacterCreatorComponent = loadable(() => import("./forms/character-creator"), loadableOptions);
const LoadableVehicleUIComponent = loadable(() => import("./hud/vehicle-ui"), loadableOptions);

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
						<Route path="/vehicleui" component={LoadableVehicleUIComponent} />
					</Switch>
				</Router>
				<Chat/>
				<HUD/>
				<Popup/>
				<Speedometer/>
				<Progress />
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
		this.routerRef.current.history.push(url)
	}
}

ReactDOM.render(<App />, document.getElementById('root'));
