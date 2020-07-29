import React, { Component } from "react"

class VehicleUI extends Component {
    constructor(props) {
        super(props)

        this.state = {
            ui: '',
            currentSelection: 'Schließen'
        }

        this.hover = this.hover.bind(this)
        this.updateUI = this.updateUI.bind(this)
    }

    componentDidMount() {
        if ('alt' in window) {
            alt.on('VehicleUI:ChangeUI', this.updateUI)
        }
    }

    updateUI(type) {
        this.setState({ ui: type })
        this.forceUpdate()
    }

    hover(e) {
        let type = e.target.getAttribute('type')

        if (type == 'vehInfo') {
            this.setState({ currentSelection: 'Fahrzeuginfo' })
        } else if (type == 'light') {
            this.setState({ currentSelection: 'Licht an/aus' })
        } else if (type == 'lock') {
            this.setState({ currentSelection: 'Auf-/Zuschließen' })
        } else if (type == 'close') {
            this.setState({ currentSelection: 'Schließen' })
        } else if (type == 'engine') {
            this.setState({ currentSelection: 'Motor an/aus' })
        } else if (type == 'engineHood') {
            this.setState({ currentSelection: 'Motorhaube auf/zu' })
        } else if (type == 'trunk') {
            this.setState({ currentSelection: 'Kofferraum auf/zu' })
        }
        
        this.forceUpdate()

        if ('alt' in window) {
            alt.emit('VehicleUI:UpdateData', this.state.currentSelection)
        }
    }

    render() {
        if (this.state.ui == 'inVehicle') {
            return (
                <div className="select-none">
                    <div className="flex mt-48">
                        <div type="vehInfo" className="group mt-32 ml-auto mr-32 w-32 h-32 bg-blue-500 rounded-full border-4 border-blue-500 opacity-75 shadow-xl hover:border-4 hover:border-gray-200" onMouseOver={this.hover}>
                            <img className="w-24 h-24 mx-auto mt-3" src="https://exocentral.de/vehicle/info.png"></img>
                        </div>
                        <div type="light" className="mt-32 mr-auto w-32 h-32 bg-blue-500 rounded-full border-4 border-blue-500 opacity-75 shadow-xl hover:border-4 hover:border-gray-200" onMouseOver={this.hover}>
                            <img className="w-24 h-24 ml-3 mt-2" src="https://exocentral.de/vehicle/light.png"></img>
                        </div>
                    </div>
                    <p className="relative mt-6 text-center text-bold text-gray-200 text-3xl mx-auto">{this.state.currentSelection}</p>
                    <div className="flex">
                        <div type="engine" className="ml-auto mr-64 w-32 h-32 bg-blue-500 rounded-full border-4 border-blue-500 opacity-75 shadow-xl hover:border-4 hover:border-gray-200" onMouseOver={this.hover}>
                            <img className="w-20 h-20 mx-auto mt-5" src="https://exocentral.de/vehicle/engine.png"></img>
                        </div>
                        <div type="close" className="mr-auto w-32 h-32 bg-blue-500 rounded-full border-4 border-blue-500 opacity-75 shadow-xl hover:border-4 hover:border-gray-200" onMouseOver={this.hover}>
                            <img className="w-24 h-24 mx-auto mt-2" src="https://exocentral.de/vehicle/close.png"></img>
                        </div>
                    </div>
                </div>
            )
        } else if (this.state.ui == 'outVehicle') {
            return (
                <div className="select-none">
                    <div className="flex mt-48">
                        <div type="vehInfo" className="group mt-32 ml-auto mr-32 w-32 h-32 bg-blue-500 rounded-full border-4 border-blue-500 opacity-75 shadow-xl hover:border-4 hover:border-gray-200" onMouseOver={this.hover}>
                            <img className="w-24 h-24 mx-auto mt-3" src="https://exocentral.de/vehicle/info.png"></img>
                        </div>
                        <div type="trunk" className="mt-32 mr-auto w-32 h-32 bg-blue-500 rounded-full border-4 border-blue-500 opacity-75 shadow-xl hover:border-4 hover:border-gray-200" onMouseOver={this.hover}>
                            <img className="w-24 h-24 ml-3 mt-3" src="https://exocentral.de/vehicle/trunk.png"></img>
                        </div>
                    </div>
                    <p className="relative mt-6 text-center text-bold text-gray-200 text-3xl mx-auto">{this.state.currentSelection}</p>
                    <div className="flex">
                        <div type="lock" className="ml-auto mr-64 w-32 h-32 bg-blue-500 rounded-full border-4 border-blue-500 opacity-75 shadow-xl hover:border-4 hover:border-gray-200" onMouseOver={this.hover}>
                            <img className="w-20 h-20 mx-auto mt-5" src="https://exocentral.de/vehicle/key.png"></img>
                        </div>
                        <div type="engineHood" className="mr-auto w-32 h-32 bg-blue-500 rounded-full border-4 border-blue-500 opacity-75 shadow-xl hover:border-4 hover:border-gray-200" onMouseOver={this.hover}>
                            <img className="w-24 h-24 mx-auto mt-2" src="https://exocentral.de/vehicle/engineHood.png"></img>
                        </div>
                    </div>
                    <div type="close" className="mx-auto w-32 h-32 bg-blue-500 rounded-full border-4 border-blue-500 opacity-75 shadow-xl hover:border-4 hover:border-gray-200" onMouseOver={this.hover}>
                        <img className="mx-auto w-24 h-24 mt-3" src="https://exocentral.de/vehicle/close.png"></img>
                    </div>
                </div>
            )
        } else { return null }
    }
}

export default VehicleUI