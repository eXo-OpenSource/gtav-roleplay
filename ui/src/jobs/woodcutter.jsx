import React, {Component} from "react"

class WoodCutter extends Component {
  constructor(props) {
    super(props)
    this.closeGUI = this.closeGUI.bind(this)
  }

  closeGUI() {
    if ("alt" in window) {
      alt.emit("WoodCutter:CloseGUI")
    }
  }

  render() {
    return (
      <div>
        <div className="container mx-auto w-1/3" style={{ margin: "0 auto", marginTop: "12%" }}>
          <div className="card-header">Paleto Forest</div>
          <div className="card-body">
            <img className="mx-auto" src="https://static.exo.cool/exov-static/images/jobs/woodcutter.png"></img>
            <p className="mt-4 text-white-alpha-80 text-center">Willkommen im Paleto Forest!</p>
              <div className="p-1">
                <div className="card-body bg-black text-white text-sm">Deine Arbeit als Holzfäller ist das alleinige Hacken von Holz an rohen Baumstämmen, welche Du dann in der Sägerei zu Holzbrettern verarbeiten kannst. Damit sie einen wirtschaftlichen Nutzen haben, kannst Du mit den Holzbrettern dann zur Verarbeitung Dich begeben, um Sie dann zu Holzplatten zu verkaufen. Ob Du die Holzbretter oder Holzplatten, welche in der Regel mehr Geld bringen, verkaufst, ist Dir überlassen.</div>
              </div>
          </div>
          <div className="card-footer flex mx-auto">
            <button className="btn btn-primary w-full" onClick={this.closeGUI}>Arbeit beginnen</button>
          </div>
        </div>
      </div>
    )
  }
}

export default WoodCutter;
