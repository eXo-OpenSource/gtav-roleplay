import React, { Component } from "react";
import Gauge from "../components/gauge";

class Speedometer extends Component {

	constructor(props) {
		super(props);
		this.state = {
			rpm: 7000,
			speed: 116,
			gear: 1,
			active: false,
		}
	}

	componentDidMount() {
		if ("alt" in window) {
			if ("alt" in window) {
				alt.on("Speedo:SetData", this.setData.bind(this))
			}
		} else {
			this.setState({
				active: true
			})

			setInterval(() => {
				this.setState({
					rpm: Math.round(Math.random()* 9000)
				})
			}, 2000)
		}
	}

	setData(key, value) {
		this.setState({ [key]: value})
	}

	gaugeColor() {
		if(this.state.rpm < 7000) {
			return "#5ee432"; // green
		}else if(this.state.rpm < 8000) {
			return "#fffa50"; // yellow
		}else if(this.state.rpm < 9000) {
			return "#ef4655"; // orange
		}else {
			return "#ef4655"; // red
		}

	}

	render() {
		return !this.state.active ? null :
			<div className="absolute bottom-0 right-0 w-64">
				<Gauge value={this.state.rpm} color={() => this.gaugeColor()}>
					<div className="w-full absolute text-center" style={{top: "130px"}}>{this.state.rpm} rpm</div>
					<div className="absolute w-full text-center text-lg" style={{top: "95px"}}>KM/H</div>
					<div className="absolute w-full text-center text-3xl font-bold" style={{top: "40px"}}>&nbsp;{this.state.speed}</div>
					<div className="absolute text-center font-bold bg-yellow-400 px-2 py-7" style={{left: "175px", top: "80px"}}>{this.state.gear}</div>
				</Gauge>
			</div>;
	}

}

export default Speedometer;
