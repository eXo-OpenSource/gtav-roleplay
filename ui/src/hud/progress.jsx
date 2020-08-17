import React, { Component } from "react";
import Progressbar from "../components/progressbar";


class Progress extends Component {

  constructor(props) {
    super(props);
    this.state = {
      active: false,
      progress: 0.7,
      text: ""
    }
  }
  componentDidMount() {
    if("alt" in window) {
      alt.on("Progress:Set", (val) => {
        this.setState({
          progress: val
        })
      })
      alt.on("Progress:Active", (toggle) => {
        this.setState({
          active: toggle
        })
      })
      alt.on("Progress:Text", (text) => {
        this.setState({
          text: text
        })
      })
    }
  }

  render() {
    return !this.state.active ? null :
      <div className="absolute mt-3 max-w-md bg-gray-800 opacity-75 rounded-md px-2 py-2" style={{marginLeft: "40%"}}>
        <Progressbar value={this.state.progress}/>
        <div className="text-center font-medium text-white">{this.state.text}</div>
      </div>;
  }
}

export  default Progress;
