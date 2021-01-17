import React, { Component } from "react"
import BrowserMockup from "../../components/browser-mockup"

class DrivingSchoolTheory extends Component {
  constructor(props) {
    super(props)
  }

  render() {
    return (
      <div>
        <BrowserMockup url={"Hallo"} site={
          <p>Test :)</p>
        }/>
      </div>
    )
  }
}

export default DrivingSchoolTheory;
 