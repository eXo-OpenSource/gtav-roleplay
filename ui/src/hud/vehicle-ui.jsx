import React, { Component } from "react"

class VehicleUI extends Component {
    constructor(props) {
        super(props)

        this.state = {
            currentSelection: 'Schließen'
        }

        this.hover = this.hover.bind(this)
    }

    hover(e) {
        let type = e.target.getAttribute('type')

        if (type == 'vehInfo') {
            this.forceUpdate(this.setState({ currentSelection: 'Fahrzeuginfo' }))
        } else if (type == 'light') {
            this.forceUpdate(this.setState({ currentSelection: 'Licht an/aus' }))
        } else if (type == 'lock') {
            this.forceUpdate(this.setState({ currentSelection: 'Auf-/Zuschließen' }))
        } else if (type == 'close') {
            this.forceUpdate(this.setState({ currentSelection: 'Schließen' }))
        } else if (type == 'engine') {
            this.forceUpdate(this.setState({ currentSelection: 'Motor an/aus' }))
        }

        if ('alt' in window) {
            alt.emit('VehicleUI:UpdateData', this.state.currentSelection)
        }
    }

    render() {
        return (
            <div class="select-none">
                <div class="flex mt-48">
                    <div type="vehInfo" class="group mt-32 ml-auto mr-32 w-32 h-32 bg-blue-500 rounded-full border-4 border-blue-500 opacity-75 shadow-xl hover:border-4 hover:border-gray-200" onMouseOver={this.hover}>
                        <img class="w-24 h-24 mx-auto mt-3" src="https://exocentral.de/vehicle/info.png"></img>
                    </div>
                    <div type="light" class="mt-32 mr-auto w-32 h-32 bg-blue-500 rounded-full border-4 border-blue-500 opacity-75 shadow-xl hover:border-4 hover:border-gray-200" onMouseOver={this.hover}>
                        <img class="w-24 h-24 ml-3 mt-2" src="https://exocentral.de/vehicle/light.png"></img>
                    </div>
                </div>
                <p type="vehInfo" class="relative mt-6 text-center text-bold text-gray-200 text-3xl mx-auto">{this.state.currentSelection}</p>
                <div class="flex">
                    <div type="lock" class="ml-auto mr-64 w-32 h-32 bg-blue-500 rounded-full border-4 border-blue-500 opacity-75 shadow-xl hover:border-4 hover:border-gray-200" onMouseOver={this.hover}>
                        <img class="w-20 h-20 mx-auto mt-5" src="https://exocentral.de/vehicle/key.png"></img>
                    </div>
                    <div type="engine" class="mr-auto w-32 h-32 bg-blue-500 rounded-full border-4 border-blue-500 opacity-75 shadow-xl hover:border-4 hover:border-gray-200" onMouseOver={this.hover}>
                        <img class="w-24 h-24 mx-auto mt-2" src="https://exocentral.de/vehicle/engine.png"></img>
                    </div>
                </div>
                <div type="close" class="mx-auto w-32 h-32 bg-blue-500 rounded-full border-4 border-blue-500 opacity-75 shadow-xl hover:border-4 hover:border-gray-200" onMouseOver={this.hover}>
                    <img class="mx-auto w-24 h-24 mt-3" src="https://exocentral.de/vehicle/close.png"></img>
                </div>
            </div>
        )
    }
}

export default VehicleUI