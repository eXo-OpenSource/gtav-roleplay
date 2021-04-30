import React, {Component} from 'react'
import {options} from "./license-options"

class Licenses extends Component {
    constructor(props) {
        super(props)
        this.state = {
            selected: 0
        }

        this.handleChange = this.handleChange.bind(this)
        this.close = this.close.bind(this)
        this.buy = this.buy.bind(this)
    }

    handleChange(e) {
        console.log("license selected")
        this.setState({ selected: e.target.value })
        console.log(this.state.selected)
    }

    buy() {
        alt.emit("Cityhall:BuyLicense", this.state.selected)
    }

    close() {
        alt.emit("Cityhall:CloseLicenses")
    }

    render() {
        return(
          <>
            <div className="container mx-auto max-w-xl" style={{ margin: "0 auto", marginTop: "12%" }}>
              <div className="card-header">Bürgerliche Lizenzen</div>
              <div className="card-body">
                <div className="ml-0 flex">
                    <img className="shadow-md" src="https://i.gyazo.com/367a6d13f53b97025c1634583fa4599a.png"></img>
                    <p className="ml-4 p-4 bg-white shadow-md bg-shadow italic text-gray-700">Mein Name ist Jacob Taylor. Ich bin zuständig für die Vergabe der bürgerlichen Lizenzen. <br></br>Wie kann ich dir helfen?</p>
                </div>
                <div className="bg-gray-100 mt-4 h-64 flex">
                    <select onChange={this.handleChange} className="w-1/2 h-full bg-gray-200 px-3 py-2 outline-none text-gray-700" multiple>
                        {options.map((option) => (
                            <option class="py-1 hover:bg-gray-300" value={option.id}>{option.label}</option>
                        ))}
                    </select>
                    <div className="w-full pl-4 pt-2 text-gray-700">
                        {options[this.state.selected].value}
                        <br></br><b>Preis: ${options[this.state.selected].price}</b>
                    </div>
                </div>
              </div>
              <div className="card-footer flex mx-auto">
              <button className="btn btn-success w-full" onClick={this.buy}>Lizenz erwerben</button>
              <button className="btn btn-danger ml-6" onClick={this.close}>Schließen</button>
              </div>
            </div>
          </>
        )
    }
} 

export default Licenses;