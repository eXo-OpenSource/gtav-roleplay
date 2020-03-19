import React, { Component } from "react";
import Gauge from "../components/gauge";

class Speedometer extends Component {

	constructor(props) {
		super(props);
		this.state = {
			rpm: 7000
		}
	}

	componentDidMount() {
		if ("alt" in window) {
			alt.on("setRPM", this.setState({rpm: this}))
		}
	}

	render() {
		return (
			<div className="absolute bottom-0 right-0 w-64">
				<Gauge value={this.rpm}>
					<div className="w-full absolute text-center" style={{top: "130px"}}>0 rpm</div>
				</Gauge>
			</div>
		);
	}

}

export default Speedometer;
