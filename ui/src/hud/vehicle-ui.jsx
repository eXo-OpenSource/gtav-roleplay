import React, { Component } from "react"

class VehicleUI extends Component {
    constructor(props) {
        super(props)

        this.state = {
            currentSelection: "Schließen"
        }

        this.hoverImage = this.hoverImage.bind(this)
        this.unhoverImage = this.unhoverImage.bind(this)
    }

    updateChanges() {
        if ('alt' in window) {
            alt.emit("VehicleUI:UpdateData", this.state.currentSelection)
        }  
    }

    hoverImage(e) {
        let type = e.target.getAttribute('type')

        if (type === 'vehInfo') {
            this.setState({ currentSelection: 'Fahrzeuginfo' })
        } else if (type === 'light') {
            this.setState({ currentSelection: 'Licht an/aus' })
        } else if (type === 'lock') {
            this.setState({ currentSelection: 'Aufschließen' })
        } else if (type === 'unlock') {
            this.setState({ currentSelection: 'Abschließen' })
        } else if (type === 'engine') {
            this.setState({ currentSelection: 'Motor starten' })
        }

        this.updateChanges()
    }
    
    unhoverImage(e) {
        this.setState({ currentSelection: 'Schließen' })
        this.updateChanges()
    }

    render() {
        return (
            <div>
                <div className="flex mt-32">
                    <img type="vehInfo" className="mt-16 ml-auto mr-32" src="https://exocentral.de/vehicle/VehInfo.png" onMouseOver={this.hoverImage} onMouseLeave={this.unhoverImage}></img>
                    <img type="light" className="mt-16 mr-auto" src="https://exocentral.de/vehicle/Light.png" onMouseOver={this.hoverImage} onMouseLeave={this.unhoverImage}></img>
                </div>
                <p type="vehInfo" className="text-center text-bold text-3xl mx-auto">{this.state.currentSelection}</p>
                <div className="flex">
                    <img type="lock" className="ml-auto mr-64" src="https://exocentral.de/vehicle/Locked.png" onMouseOver={this.hoverImage} onMouseLeave={this.unhoverImage}></img>
                    <img type="unlock" className="mr-auto" src="https://exocentral.de/vehicle/Unlocked.png" onMouseOver={this.hoverImage} onMouseLeave={this.unhoverImage}></img>
                </div>
                <img type="engine" className="mx-auto" src="https://exocentral.de/vehicle/Engine.png" onMouseOver={this.hoverImage} onMouseLeave={this.unhoverImage}></img>
            </div>
        )
    }
}

export default VehicleUI