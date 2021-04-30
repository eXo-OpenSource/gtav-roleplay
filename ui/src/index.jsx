import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import {HashRouter as Router, Route, Switch, Link, withRouter} from "react-router-dom";
import Chat from "./hud/chat";
import HUD from "./hud/hud";
import loadable, { Options as LoadableOptions } from "@loadable/component";

import './root.css';
import Speedometer from "./hud/speedometer";
import Popup from "./hud/popup";
import Toast from "./hud/toast";
import Progress from "./hud/progress";
import Interaction from "./hud/interaction";
import Farmer from "./jobs/farmer";

const loadableOptions = { };
const LoadableLoginComponent = loadable(() => import("./forms/login"), loadableOptions);
const LoadableCharacterCreatorComponent = loadable(() => import("./forms/charcreator/charcreator"), loadableOptions);
const LoadableATMComponent = loadable(() => import("./forms/atm"), loadableOptions);
const LoadableCarRentComponent = loadable(() => import("./forms/car-rent"), loadableOptions);
const LoadableFarmerComponent = loadable(() => import("./jobs/farmer"), loadableOptions);
const LoadableWoodCutterComponent = loadable(() => import("./jobs/woodcutter"), loadableOptions);
const LoadableDrivingSchoolComponent = loadable(() => import("./forms/drivingschool/theory"), loadableOptions);
const LoadablePetrolStationComponent = loadable(() => import("./forms/petrolstation"), loadableOptions);
const LoadableLicensesComponent = loadable(() => import("./environment/cityhall/licenses"), loadableOptions);

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
            <Route path="/atm" component={LoadableATMComponent} />
            <Route path="/carrent" component={LoadableCarRentComponent} />
            <Route path="/farmer" component={LoadableFarmerComponent} />
            <Route path="/woodcutter" component={LoadableWoodCutterComponent} />
            <Route path="/drivingschool" component={LoadableDrivingSchoolComponent} />
            <Route path="/petrolstation" component={LoadablePetrolStationComponent} />
            <Route path="/cityhall-licenses" component={LoadableLicensesComponent} />
          </Switch>
        </Router>
        <Chat/>
        <Toast/>
        <Popup/>
        <HUD/>
        <Speedometer/>
        <Interaction/>
        <Progress/>
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
