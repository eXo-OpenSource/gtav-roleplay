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
 
  render() {
    return (
      <div>
        <BrowserMockup url={"https://www.drivingschool.san-andreas.com/online-exam.php"} site={
          <div>
            {this.state.step == 1 ? (
              <div>
                <div class="container m-auto w-1/2 h-16 pt-5 mt-32 text-center align-middle text-gray-700 font-bold text-lg bg-gray-400 shadow-md rounded-t">
                  Fahrschulprüfung {this.state.step}
                </div>
                <div class="m-auto w-1/2 text-center bg-gray-300" style={{height: "400px"}}>
                  <h1 class="pt-3 font-bold text-gray-700">Willkommen im Online Fahrschultest!</h1>
                  <p class="p-5 text-sm">
                    Um als Bürger die Möglichkeit zu haben, ein Auto zu fahren, müssen Sie ihren theoretischen Teil der Führerscheinprüfung bei der Polizei absolvieren.<br></br>
                    Lassen Sie sich Zeit und denken Sie nach bevor Sie antworten.
                  </p>
                  <p class="p-5 text-sm italic">
                    Die Kosten für die Theorieprüfung belaufen sich auf $500.<br></br>
                    Sie haben bestanden, wenn Sie unter 5 Fehlerpunkte haben.
                  </p>
                  <p class="p-5 text-sm">
                    Sie erwarten insgesamt 30 Fragen
                  </p>
                  <button class="btn btn-primary mt-5" onClick={this.startTest}>Onlinetest starten</button>
                  <button class="btn btn-secondary mt-5">Abbrechen</button>
                </div>
              </div>
            ) : (
              <div>
                <div class="mt-32 container w-1/2 m-auto">
                  <Quiz questions={drivingSchoolQuestions}/>
                </div>
              </div>
            )}
          </div>
        }/>
      </div>
    )
  }
}

export default DrivingSchoolTheory;
 