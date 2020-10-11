import React, {Component} from "react";
import SimpleBar from "simplebar-react";

class VehicleShop extends Component {

  constructor(props) {
    super(props);
    this.state = {
      items: [],
      card: []
    }
  }

  componentDidMount() {
    if("alt" in window) {

    } else {
      this.setState({
        items: [
          {},
          {},
          {},
          {},
          {},
          {},
          {},
          {},
        ]
      })
    }
  }

  renderItems() {
    return this.state.items.map((value, i) => {
      return <div key={i} className="p-2 bg-gray-600 rounded-lg my-2 hover:bg-gray-700">
        <img style={{"width": "164px", "height": "91px"}} src="https://static.exo.cool/exov-static/images/shops/vehicleshop/Osiris.jpg"/>
        <div className="text-center">Osiris</div>
      </div>
    })
  }

  render() {
    return <div className="flex mt-24">
      <div className="text-center mr-0 pl-0 mt-64 w-48 text-gray-200 bg-gray-700" style={{marginLeft: "20.5%"}}>
        <img className="mx-auto mt-6 rounded-full w-32 h-32 border-2 border-blue-500" src="https://exocentral.de/atm/mazebank.jpg"></img>
        <p className="mt-4 text-xl">{name}</p>
      </div>
      <div className="container mt-64 max-w-4xl text-gray-200 card flex max-h-1/2">
        <div style={{"width": "80%"}}>
          <SimpleBar style={{ maxHeight: "50vh" }}>
          <div className="card-body opacity-100 bg-gray-300 flex flex-wrap justify-around">
            {this.renderItems()}
          </div>
          </SimpleBar>
        </div>
        <div style={{"width": "30%"}} className="flex flex-col">
          <div className="card-body opacity-100 bg-gray-200 flex flex-col flex-grow">
            <p className="ml-1 font-bold text-gray-600 text-1xl text-center">Rechnung</p>
            <div className="border-2 border-gray-600 rounded-lg my-2 text-gray-600 px-2 flex-grow">
              <div className="flex justify-between">
                <span>Osiris</span>
                <span className="font-bold text-red-600">-13.000.000 $</span>
              </div>
              <div className="flex justify-between">
                <span>Rabatt</span>
                <span className="font-bold text-green-600">2.000.000 $</span>
              </div>
            </div>
            <button className="btn btn-primary mt-2 rounded-sm font-normal w-full" >Kaufen</button>
          </div>
        </div>
      </div>
    </div>
  }
}

export default VehicleShop
