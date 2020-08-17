import React, { Component } from "react";
import Gauge from "../components/gauge";

class Speedometer extends Component {

  constructor(props) {
    super(props);
    this.state = {
      rpm: 7000,
      speed: 116,
      gear: 1,
      indicatorLeft: 0,
      indicatorRight: 0,
      seatbelt: 0,
      lights: 0,
      fuel: 0,
      active: false
    }
  }

  componentDidMount() {
    if ("alt" in window) {
      if ("alt" in window) {
        alt.on("Speedo:SetData", this.setData.bind(this))
      }
    } else {
      this.setState({
        active: false
      })

      setInterval(() => {
        this.setState({
          rpm: Math.round(Math.random()* 9000)
        })
      }, 2000)
    }
  }

  setData(key, value) {
    this.setState({ [key]: value })
    this.forceUpdate()
  }

  gaugeColor() {
    if (this.state.rpm < 7000) {
      return "#5ee432"; // green
    } else if (this.state.rpm < 8000) {
      return "#fffa50"; // yellow
    } else if (this.state.rpm < 9000) {
      return "#ef4655"; // orange
    } else {
      return "#ef4655"; // red
    }

  }

  render() {
    return !this.state.active ? null :
      <div className="absolute bottom-0 right-0 w-64 select-none">
        <Gauge value={this.state.rpm} color={() => this.gaugeColor()}>
          {/*<div className="w-full text-gray-100 absolute text-center" style={{top: "130px"}}>{this.state.rpm} rpm</div>*/}
          <div className="absolute w-full text-gray-100 text-center text-lg" style={{top: "90px"}}>&nbsp;KM/H</div>
          <div className="absolute w-full text-gray-100 text-center text-5xl font-bold" style={{top: "35px"}}>{this.state.speed}</div>
          <div className="absolute text-center font-bold bg-yellow-400 px-2 py-1 shadow-md" style={{left: "175px", top: "90px"}}>{this.state.gear}</div>
          <img className="absolute w-6 h-6" src={"https://static.exo.cool/exov-static/images/vehicles/speedo/seatbelt_" + this.state.seatbelt + ".png"} data-arg="seatbelt" style={{left: "61px", top: "130px"}}></img>
          <img className="absolute w-6 h-6" src={"https://static.exo.cool/exov-static/images/vehicles/speedo/carlights_" + this.state.lights + ".png"} data-arg="lights" style={{left: "91px", top: "130px"}}></img>
          <img className="absolute w-6 h-6" src={"https://static.exo.cool/exov-static/images/vehicles/speedo/left_arrow_" + this.state.indicatorLeft + ".png"} data-arg="indicatorLeft" style={{left: "121px", top: "130px"}}></img>
          <img className="absolute w-6 h-6" src={"https://static.exo.cool/exov-static/images/vehicles/speedo/right_arrow_" + this.state.indicatorRight + ".png"} data-arg="indicatorRight" style={{left: "148px", top: "130px"}}></img>
          <img className="absolute w-6 h-6" src={"https://static.exo.cool/exov-static/images/vehicles/speedo/fuel_" + this.state.fuel + ".png"} data-arg="fuel" style={{left: "172px", top: "130px"}}></img>
          <div className="absolute bg-gray-700 w-32 bg-grey-light mt-6 shadow" style={{left: "65px", top: "140px", outlineStyle: "solid", outlineWidth: "1px"}}>
            <div className="bg-yellow-500 text-xs leading-none py-1 h-3 text-center text-gray-100" style={{width: "50px"}}></div>
          </div>
        </Gauge>
      </div>;
  }

}

export default Speedometer;
