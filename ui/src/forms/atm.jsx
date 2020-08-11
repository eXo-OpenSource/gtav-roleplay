import React, {Component} from "react"

var page = "nav-home"
var name =  "-"
var money =  0
var bankmoney = 0
var cashInAmount = 0
var cashOutAmount = 0
var regexp = /^\d+$/

class ATM extends Component {

    componentDidMount() {
        if ("alt" in window) {
            alt.on("ATM:SetData", this.setData.bind(this))
        }
    }

    setData(key, value) {
        if (key == "money") {
            money = value
        } else if (key == "bankmoney") {
            bankmoney = value
        } else if (key == "name") {
            name = value
        }

        this.forceUpdate()
    }

    changePage(e) {
        page = e.target.getAttribute("data-arg")
        this.forceUpdate()
    }

    cashIn() {
        if ("alt" in window) {
            alt.emit("ATM:CashIn", cashInAmount)
        }
    }

    cashOut() {
        if ("alt" in window) {
            alt.emit("ATM:CashOut", cashOutAmount)
        }
    }

    logOut(e) {
        if ("alt" in window) {
            alt.emit("ATM:Logout")
        }
    }
    
    changeEditBox(e) {
        let _box = e.target.getAttribute("data-arg")
        if ((regexp.test(e.target.value) || e.target.value === '')) {
            if (_box == "cashin") {
                cashInAmount = e.target.value
            } else if (_box == "cashout") {
                cashOutAmount = e.target.value
            }
        }
        this.forceUpdate()
    }

    render() {
        if (page == "nav-home") {
            return (
                <div className="flex mt-24">
                    <div className="text-center mr-0 pl-0 mt-64 w-48 text-gray-200 bg-gray-700" style={{marginLeft: "32.5%"}}>
                        <img className="mx-auto mt-6 rounded-full w-32 h-32 border-2 border-blue-500" src="https://exocentral.de/atm/mazebank.jpg"></img>
                        <p className="mt-4 text-xl">{name}</p>
                        <button className="btn btn-primary mt-8 rounded-sm font-normal" onClick={this.logOut.bind(this)}>Ausloggen</button>
                    </div>
                    <div className="container mt-64 max-w-md text-gray-200">
                        <div className="card">
                            <ul className="flex border-b-2 border-gray-300 bg-white">
                            <li className="flex-1 mr-2">
                                <a className="text-center block border border-blue-500 py-2 px-4 bg-blue-500 hover:bg-blue-700 text-white" data-arg="nav-home" onClick={this.changePage.bind(this)}>Home</a>
                            </li>
                            <li className="flex-1 mr-2">
                                <a className="text-center block border border-white hover:border-gray-200 text-blue-500 hover:bg-gray-200 py-2 px-4" data-arg="nav-cashout" onClick={this.changePage.bind(this)}>Auszahlen</a>
                            </li>
                            <li className="flex-1 mr-2">
                                <a className="text-center block border border-white hover:border-gray-200 text-blue-500 hover:bg-gray-200 py-2 px-4" data-arg="nav-cashin" onClick={this.changePage.bind(this)}>Einzahlen</a>
                            </li>
                            <li className="text-center flex-1">
                                <a className="block py-2 px-4 text-gray-400 cursor-not-allowed" data-arg="transferlog">Kontoauszug</a>
                            </li>
                            </ul>
                            <div className="card-body opacity-100 max-h-screen bg-gray-200 h-64">
                                <p className="ml-1 text-gray-600 text-1xl">Willkommen zurück, {name}.</p>
                                <p className="ml-1 mt-2 text-gray-600 text-2xl">Aktueller Kontostand</p>
                                <hr></hr>
                                <p className="ml-1 mt-1 text-green-600 text-2xl">${bankmoney}</p>
                                <p className="ml-1 mt-4 text-gray-600 text-2xl">Bargeld</p>
                                <hr></hr>
                                <p className="ml-1 text-green-600 text-2xl">${money}</p>
                                <p className="ml-56 text-gray-600 text-sm">San Andreas Maze Bank</p>
                            </div>
                        </div>
                    </div>
                </div>
            )
        } else if (page == "nav-cashout") {
            return (
                <div className="flex mt-24">
                    <div className="text-center mr-0 pl-0 mt-64 w-48 text-gray-200 bg-gray-700" style={{marginLeft: "32.5%"}}>
                        <img className="mx-auto mt-6 rounded-full w-32 h-32 border-2 border-blue-500" src="https://exocentral.de/atm/mazebank.jpg"></img>
                        <p className="mt-4 text-xl">{name}</p>
                        <button className="btn btn-primary mt-8 rounded-sm font-normal" onClick={this.logOut.bind(this)}>Ausloggen</button>
                    </div>
                    <div className="container mt-64 max-w-md text-gray-200">
                        <div className="card">
                            <ul className="flex border-b-2 border-gray-300 bg-white">
                            <li className="flex-1 mr-2">
                                <a className="text-center block border border-white hover:border-gray-200 text-blue-500 hover:bg-gray-200 py-2 px-4" data-arg="nav-home" onClick={this.changePage.bind(this)}>Home</a>
                            </li>
                            <li className="flex-1 mr-2">
                                <a className="text-center block border border-blue-500 py-2 px-4 bg-blue-500 hover:bg-blue-700 text-white" data-arg="nav-cashout" onClick={this.changePage.bind(this)}>Auszahlen</a>
                            </li>
                            <li className="flex-1 mr-2">
                                <a className="text-center block border border-white hover:border-gray-200 text-blue-500 hover:bg-gray-200 py-2 px-4" data-arg="nav-cashin" onClick={this.changePage.bind(this)}>Einzahlen</a>
                            </li>
                            <li className="text-center flex-1">
                                <a className="block py-2 px-4 text-gray-400 cursor-not-allowed" data-arg="transferlog">Kontoauszug</a>
                            </li>
                            </ul>
                            <div className="card-body opacity-100 max-h-screen bg-gray-200 h-64">
                                <p className="ml-1 text-gray-600 text-1xl">Bitte geben sie einen Betrag zum Einzahlen an.</p>
                                <p className="ml-1 mt-2 text-gray-600 text-2xl">Auszahlungsbetrag</p>
                                <hr></hr>
                                <div className="flex">
                                    <input type="edit" placeholder="500" className="edit w-32 ml-1 h-8 mt-2" data-arg="cashout" onChange={this.changeEditBox.bind(this)} value={cashOutAmount}></input>
                                    <p className="ml-1 mt-2 text-gray-600 text-xl">$</p>
                                    <button className="btn btn-primary rounded-sm mt-2 ml-16 h-8 py-0 font-normal" onClick={this.cashOut.bind(this)}>Auszahlen</button>
                                </div>
                                <p className="ml-1 mt-4 text-gray-600 text-2xl">Kontostand</p>
                                <hr></hr>
                                <p className="ml-1 text-green-600 text-2xl">${bankmoney}</p>
                                <p className="ml-56 text-gray-600 text-sm">San Andreas Maze Bank</p>
                            </div>
                        </div>
                    </div>
                </div>
            )
        } else if (page == "nav-cashin") {
            return (
                <div className="flex mt-24">
                    <div className="text-center mr-0 pl-0 mt-64 w-48 text-gray-200 bg-gray-700" style={{marginLeft: "32.5%"}}>
                        <img className="mx-auto mt-6 rounded-full w-32 h-32 border-2 border-blue-500" src="https://exocentral.de/atm/mazebank.jpg"></img>
                        <p className="mt-4 text-xl">{name}</p>
                        <button className="btn btn-primary mt-8 rounded-sm font-normal" onClick={this.logOut.bind(this)}>Ausloggen</button>
                    </div>
                    <div className="container mt-64 max-w-md text-gray-200">
                        <div className="card">
                            <ul className="flex border-b-2 border-gray-300 bg-white">
                            <li className="flex-1 mr-2">
                                <a className="text-center block border border-white hover:border-gray-200 text-blue-500 hover:bg-gray-200 py-2 px-4" data-arg="nav-home" onClick={this.changePage.bind(this)}>Home</a>
                            </li>
                            <li className="flex-1 mr-2">
                                <a className="text-center block border border-white hover:border-gray-200 text-blue-500 hover:bg-gray-200 py-2 px-4" data-arg="nav-cashout" onClick={this.changePage.bind(this)}>Auszahlen</a>
                            </li>
                            <li className="flex-1 mr-2">
                                <a className="text-center block border border-white py-2 px-4 bg-blue-500 hover:bg-blue-700 text-white" data-arg="nav-cashin" onClick={this.changePage.bind(this)}>Einzahlen</a>
                            </li>
                            <li className="text-center flex-1">
                                <a className="block py-2 px-4 text-gray-400 cursor-not-allowed" data-arg="transferlog">Kontoauszug</a>
                            </li>
                            </ul>
                            <div className="card-body opacity-100 max-h-screen bg-gray-200 h-64">
                                <p className="ml-1 text-gray-600 text-1xl">Bitte geben sie einen Betrag zum Auszahlen an.</p>
                                <p className="ml-1 mt-2 text-gray-600 text-2xl">Einzahlungsbetrag</p>
                                <hr></hr>
                                <div className="flex">
                                    <input type="edit" placeholder="500" className="edit w-32 ml-1 h-8 mt-2" data-arg="cashin" onChange={this.changeEditBox.bind(this)} value={cashInAmount}></input>
                                    <p className="ml-1 mt-2 text-gray-600 text-xl">$</p>
                                    <button className="btn btn-primary rounded-sm mt-2 ml-16 h-8 py-0 font-normal" onClick={this.cashIn.bind(this)}>Einzahlen</button>
                                </div>
                                <p className="ml-1 mt-4 text-gray-600 text-2xl">Bargeld</p>
                                <hr></hr>
                                <p className="ml-1 text-green-600 text-2xl">${money}</p>
                                <p className="ml-56 text-gray-600 text-sm">San Andreas Maze Bank</p>
                            </div>
                        </div>
                    </div>
                </div>
            )    
        } else { return null }
    }
}

export default ATM