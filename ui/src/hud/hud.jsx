import React, { Component } from "react"
import ProgressCircle from "../components/progress-circle"

class HUD extends Component {

	constructor(props) {
	super(props)
		this.state = {
			amount: "0", // money or ammo
			location: "",
			time: "",
			date: "",
			hidden: false,
			kevlarProgress: 25,
			healthProgress: 50,
			hungerProgress: 75
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
				<div className="select-none">
					<div className="absolute top-0 right-0 mx-3 my-3"><img src="https://img.exocentral.de/HUD.png"></img></div>
					<div className="absolute right-0 top-0 mx-24 my-4 w-64 text-left text-white text-sm italic">{this.state.location}</div>
					<div className="absolute right-0 top-0 mx-12 my-4 text-right text-white text-sm font-bold">{this.state.time}</div>
					<div className="absolute right-0 top-0 mx-48 my-12 w-24 text-left text-white italic font-bold">{this.state.amount}</div>
					<div className="absolute right-0 top-0 mx-12 my-12 w-32 text-center text-white text-sm italic">{this.state.date}</div>
					<div className="absolute right-0 top-0 mx-8 my-12 w-10 h-10 mt-20 bg-gray-900 rounded-full border-blue-500 opacity-75 shadow-xl hover:border-3 hover:border-gray-200" style={{borderWidth: "3px"}}>
                        <img className="mx-auto w-6 h-6 mt-1 shadow-img" src="https://exocentral.de/hud/hunger.png" style={{marginLeft: "0.3rem"}}></img>
                    </div>
					<div className="absolute right-0 top-0 mx-20 my-12 w-10 h-10 mt-20 bg-gray-900 rounded-full border-blue-500 opacity-75 shadow-xl hover:border-3 hover:border-gray-200" style={{borderWidth: "3px"}}>
                        <img className="mx-auto w-6 h-6 mt-1 shadow-img" src="https://exocentral.de/hud/health.png" style={{marginLeft: "0.3rem"}}></img>
                    </div>
					<div className="absolute right-0 top-0 mx-32 my-12 w-10 h-10 mt-20 bg-gray-900 rounded-full border-blue-500 opacity-75 shadow-xl hover:border-3 hover:border-gray-200" style={{borderWidth: "3px"}}>
                        <img className="mx-auto w-6 h-6 mt-1 shadow-img" src="https://exocentral.de/hud/kevlar.png" style={{marginLeft: "0.3rem"}}></img>
                    </div>
					<div className="absolute right-0 top-0 mx-48 my-12 w-10 h-10 mt-20 bg-gray-900 rounded-full border-blue-500 opacity-75 shadow-xl hover:border-3 hover:border-gray-200" style={{borderRadius: "50%", borderWidth: "3px"}}>
                        <img className="mx-auto w-6 h-6 mt-1 shadow-img" src="https://exocentral.de/hud/microphone.png" style={{marginLeft: "0.3rem"}}></img>
                    </div>
					<ProgressCircle radius={60} stroke={4} progress={this.state.progress}/>
				</div>
			)
		} else {
			return null
		}
	}
}

export default HUD
