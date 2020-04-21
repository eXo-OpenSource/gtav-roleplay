import React, { Component } from "react"
import { Redirect } from "react-router"

class HUD extends Component {

	constructor(props) {
	super(props)
		this.state = {
			money: "$0",
			location: "",
			time: "",
			date: "",
			hidden: true,
		}
	}

	componentDidMount() {
		if ("alt" in window) {
			alt.on("HUD:SetData", this.setData.bind(this))
		}
	}

	setData(key, value) {
        this.setState({ [key]: value})
    }
	
	render() {		
		if (!this.state.hidden) {
			return (
				<div>
					<div className="absolute top-0 right-0 mx-3 my-3"><img src="https://img.exocentral.de/HUD.png"></img></div>
					<div className="absolute right-0 top-0 mx-24 my-4 w-64 text-left text-white text-sm italic">{this.state.location}</div>
					<div className="absolute right-0 top-0 mx-12 my-4 text-right text-white text-sm font-bold">{this.state.time}</div>
					<div className="absolute right-0 top-0 mx-48 my-12 w-24 text-left text-white italic font-bold">{this.state.money}</div>
					<div className="absolute right-0 top-0 mx-12 my-12 w-32 text-center text-white text-sm italic">{this.state.date}</div>
				</div>
			)
		} else {
			return null
		}
	}
}

export default HUD
