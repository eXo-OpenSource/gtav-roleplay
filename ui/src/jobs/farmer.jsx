import React, {Component} from "react"

export default class Charcreation extends Component {

  constructor(props) {
    super(props)

    this.beginJob = this.beginJob.bind(this)
  }

  beginJob(e) {
    if ("alt" in window) {
      alt.emit("Farmer:SelectJob", parseInt(e.target.id))
    }
  }

  render() {
    return (
      <div>
        <div className="container mx-auto w-1/3" style={{ margin: "0 auto", marginTop: "12%" }}>
          <div className="card-header">Grapeseed Farm</div>
          <div className="card-body">
            <img className="mx-auto" src="https://static.exo.cool/exov-static/images/jobs/farmer.png"></img>
            <p className="mt-4 text-white-alpha-80 text-center">Willkommen auf der Farm!<br></br>Wähle eine Arbeit, mit der Du beginnen möchtest, aus.<br></br></p>
            <div className="flex mt-2">
              <div className="w-1/2 p-1">
                <div className="py-1 text-md rounded-t-lg text-white text-center bg-blue-600 text-shadow-md">Traktorfahrer</div>
                <div className="card-body bg-black text-white text-sm">Bei der Arbeit als Traktorfahrer ist es Deine Aufgabe, das Weizen zu ernten.</div>
                <div className="bg-blue-600 text-white mt-1 p-1 text-center text-sm">Joblevel: 2</div>
              </div>
              <div className="w-1/2 p-1">
                <div className="py-1 text-md rounded-t-lg text-white text-center bg-blue-600 text-shadow-md">Erntehelfer</div>
                <div className="card-body bg-black text-white text-sm">Dieser Arbeitsauftrag umfasst das Pflücken der Äpfel an den Bäumen!</div>
                <div className="bg-blue-600 text-white mt-1 p-1 text-center text-sm">Joblevel: 1</div>
              </div>
            </div>
          </div>
          <div className="card-footer flex mx-auto">
            <button className="btn btn-primary w-1/2 mx-1" id="1" onClick={this.beginJob}>Arbeiten als Traktorfahrer</button>
            <button className="btn btn-primary w-1/2" id="0" onClick={this.beginJob}>Bei der Ernte helfen</button>
          </div>
        </div>
      </div>
    );
  }
}
