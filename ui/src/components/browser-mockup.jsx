import React, { Component } from "react"

export default class BrowserMockup extends Component {
    constructor(props) {
        super(props)

        const { url, site } = this.props
        this.url = url
        this.site = site
    }

    render() {
        return (
          <div>
            <div className="h-screen m-0" style={{ backgroundImage: "url('https://static.exo.cool/exov-static/images/backgrounds/computerbackground.jpg')", backgroundSize: 'cover', backgroundRepeat: 'no-repeat'}}></div>
            <div className="max-w-6xl mx-auto" style={{marginTop: "-50%"}}>
              <div className="w-full h-12 rounded-t-lg border-2 border-gray-300 bg-gray-300 flex justify-start items-center space-x-2 px-4">
                <span className="relative w-3 h-3 rounded-full bg-red-400"></span>
                <span className="relative w-3 h-3 rounded-full bg-yellow-400"></span>
                <span className="relative w-3 h-3 rounded-full bg-green-400 pr-1/3"></span>
                <input className="rounded-full text-gray-700 bg-gray-200 p-1 w-full px-5" type="text" readOnly value={this.url} />
              </div>
              <div className="relative border-t-0 w-full">
                <div className="absolute w-full inset-0 bg-gray-200 opacity-60" style={{ height: "750px" }}>{this.site}</div>
              </div>
            </div>
          </div>
        )
    }
}
