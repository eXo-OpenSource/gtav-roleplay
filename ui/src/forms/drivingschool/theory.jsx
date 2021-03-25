import React, { Component } from "react"
import BrowserMockup from "../../components/browser-mockup"
import { Quiz, quizScore } from "../../components/quiz"
import { drivingSchoolQuestions } from './questions'


class DrivingSchoolTheory extends Component {
  constructor(props) {
    super(props)
    this.startTest = this.startTest.bind(this)
    this.finished = this.finished.bind(this)

    this.state = {
      quizStarted: false
    }
  }

  startTest() {
    this.setState({ quizStarted: true })
  }

  finished() {
    alt.emit("Drivingschool:CloseUi", quizScore); 
    this.setState({ quizStarted: false })
    quizScore = 0;
  }

  renderIntroduction() {
    return (
      <>
      <BrowserMockup url={"https://www.drivingschool.san-andreas.com/online-exam"} site={
        <>
          <div className="container m-auto w-1/2 h-16 pt-5 mt-32 text-center align-middle text-gray-700 font-bold text-lg bg-gray-400 shadow-md rounded-t">
            Fahrschulprüfung
          </div>
          <div className="m-auto w-1/2 text-center bg-gray-300" style={{height: "400px"}}>
            <h1 className="pt-3 font-bold text-gray-700">Willkommen im Online Fahrschultest!</h1>
            <p className="p-5 text-sm">
              Um als Bürger die Möglichkeit zu haben, ein Auto zu fahren, müssen Sie ihren theoretischen Teil der Führerscheinprüfung bei der Polizei absolvieren.<br></br>
              Lassen Sie sich Zeit und denken Sie nach bevor Sie antworten.
            </p>
            <p className="p-5 text-sm italic">
              Die Kosten für die Theorieprüfung belaufen sich auf $500.<br></br>
              Sie haben bestanden, wenn 80% ihrer Angaben richtig sind.
            </p>
            <p className="p-5 text-sm">
              Sie erwarten insgesamt 30 Fragen
            </p>
            <button className="btn btn-primary mt-5" onClick={this.startTest}>Onlinetest starten</button>
          </div>
        </>
      }/>
      </>
    )
  }

  renderQuiz() {
    return (
      <BrowserMockup url={"https://www.drivingschool.san-andreas.com/online-exam"} site={
        <>
          <div className="mt-32 container w-1/2 m-auto">
            <Quiz questions={drivingSchoolQuestions} closeFunction={this.finished}/>
          </div>
        </>
      }/>
    )
  }
 
  render() {
    let body
    if (this.state.quizStarted) {
      body = this.renderQuiz()
    } else {
      body = this.renderIntroduction()
    }
    return (<>{body}</>)
  }
}

export default DrivingSchoolTheory;
 