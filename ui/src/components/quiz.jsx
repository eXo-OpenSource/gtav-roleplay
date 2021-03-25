import React, {Component} from "react"

export var quizScore = 0;

class QuizQuestion extends Component {
    constructor(props) {
        super(props)

        this.handleChange = this.handleChange.bind(this)
    }

    handleChange(e) {
        e.preventDefault()
        this.props.onChoiceChange(e.target.value)
    }

    render() {
        return (
            <div>
                <div class="container w-full h-full rounded-t-lg px-4" style={{backgroundColor: "#31b6ef"}}>
                    <p class="text-center align-middle p-3 text-gray-200">{this.props.question.text}</p>
                </div>
                <ul className="list-inside" style={{backgroundColor: "#2b2b2b"}}>
                    { this.props.question.choices.map(choice => {
                        return (
                            <div class="flex items-center mr-4">
                                <li className="p-2 mt-2 container w-full h-12 text-gray-200 italic" key={choice.id}>                      
                                    <input class="pl-4 pr-4 ml-4 mr-6 align-middle form-radio h-5 w-5 text-gray-600" type="radio" onChange={this.handleChange} checked={false} name={this.props.question.id} value={choice.id}/>{choice.text}
                                </li>
                            </div>
                        ) 
                    })}
                </ul> 
            </div>
        )
    }
}

class QuizScore extends Component {
    render() {
        return (
            <div class="container w-full h-12 pt-3 rounded-b-lg px-4 text-gray-200" style={{backgroundColor: "#3b3b3b"}}>
                <p class="float-left pl-4">Frage {this.props.cur} von {this.props.total}</p>
                <p class="float-right pr-4">Punktzahl: {this.props.score}</p>
            </div>
        )
    }
}

class QuizResult extends Component {
    render() {
        const _quizScore = this.props.score/this.props.total*100
        quizScore = _quizScore;
        return (
            <div>
                <div class="container w-full h-full rounded-t-lg px-4" style={{backgroundColor: "#31b6ef"}}>
                    <p class="text-center align-middle p-3 text-gray-200">Quiz</p>
                </div>
                <div class="text-gray-200 w-full h-full p-4 text-center" style={{backgroundColor: "#2b2b2b"}}>
                    <p class="">Du hast {this.props.score} von {this.props.total} Fragen richtig.</p>
                    <p>{_quizScore}% - {_quizScore > 80 ? "Du hast bestanden!" : "Du bist durchgefallen."}</p>
                    <button class="btn btn-primary mt-8" onClick={this.props.closeFunction}>Quiz schließen</button>
                </div>
                <div class="container w-full h-12 pt-3 rounded-b-lg px-4 text-gray-200" style={{backgroundColor: "#3b3b3b"}}>
                    <p class="float-left pl-4">Fragen: {this.props.total}</p>
                    <p class="float-right pr-4">Punktzahl: {this.props.score}</p>
                </div>
            </div>
        )
    }
}

export class Quiz extends Component {
    constructor(props) {
        super(props)
        
        this.state = {
            score: 0,
            cur: 1,
            closeFunction: null
        }

        this.handleChange = this.handleChange.bind(this)
    }

    handleChange(choice) {
        this.setState((previous, props) => ({
            cur: previous.cur + 1,
            score: choice == props.questions[previous.cur - 1].correct ? previous.score + 1 : previous.score
        }))
        quizPoints = this.state.score
    }

    render() {
        return (
            <div>
                {this.state.cur <= this.props.questions.length &&
                    <QuizQuestion question={this.props.questions[this.state.cur - 1]} onChoiceChange={this.handleChange}></QuizQuestion> 
                }
                {this.state.cur > this.props.questions.length &&
                    <QuizResult total={this.props.questions.length} score={this.state.score} closeFunction={this.props.closeFunction}></QuizResult> 
                }
                {this.state.cur <= this.props.questions.length &&
                    <QuizScore total={this.props.questions.length} cur={this.state.cur} score={this.state.score}></QuizScore> 
                }
            </div>
        )
    }
}