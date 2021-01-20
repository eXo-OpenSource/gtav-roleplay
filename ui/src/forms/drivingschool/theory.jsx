import React, { Component } from "react"
import BrowserMockup from "../../components/browser-mockup"
import { Quiz } from "../../components/quiz"
import { drivingSchoolQuestions } from './questions'

class DrivingSchoolTheory extends Component {
  constructor(props) {
    super(props)

    this.state = { step: 1 }

    this.startTest = this.startTest.bind(this)
  }

  startTest() {
    this.setState({ step: 2 })
    this.forceUpdate()
    console.log(this.state.step)
  }

  handleExit() {
    console.log("halolo")
  }
 
  render() {
    return (
      <div>
        <BrowserMockup url={"https://www.drivingschool.san-andreas.com/online-exam.php"} site={
          <div>
            <div class="mt-32 container w-1/2 m-auto">
              <Quiz questions={drivingSchoolQuestions}/>
            </div>
          </div>
        }/>
      </div>
    )
  }
}

export default DrivingSchoolTheory;
 