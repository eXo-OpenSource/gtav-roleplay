import React, { Component, createRef, useRef } from "react"
import InputNumber from "../../components/input-number"

import { faceNames } from "./utility/faces"

export default class Charcreation extends Component {

  constructor(props) {
    super(props)

    this.state = {
      page: 1
    }

    this.fatherFaceRef = createRef()

    this.next = this.next.bind(this)
    this.back = this.back.bind(this)
    this.changeMale = this.changeMale.bind(this)
    this.changeFemale = this.changeFemale.bind(this)
  }

  next(e) {
    this.setState({page: e.target.getAttribute("setpage")})
    this.forceUpdate()
  }

  back(e) {
    this.setState({page: e.target.getAttribute("setpage")})
    this.forceUpdate()
  }

  changeMale(e) {
    this.setState({gender: 1})
    e.target.className = "btn btn-primary w-2/4 h-10 m-2 font-semibold text-shadow-md"
    document.getElementById("female").className = "btn btn-primary bg-blue-700 w-2/4 h-10 m-2 font-semibold text-shadow-md"
    document.getElementById("gender").innerText = "Männlich"
  }

  changeFemale(e) {
    this.setState({gender: 0})
    e.target.className = "btn btn-primary w-2/4 h-10 m-2 font-semibold text-shadow-md"
    document.getElementById("male").className = "btn btn-primary bg-blue-700 w-2/4 h-10 m-2 font-semibold text-shadow-md"
    document.getElementById("gender").innerText = "Weiblich"
  }


  render() {
    if (this.state.page == 1) {
      return (
        <div>
          <div className="container ml-auto mr-4 mt-12 bg-black opacity-75 rounded-md shadow-xl inner-box-shadow" style={{height: "900px", width: "450px"}}>
            {/* Back, Next */}
            <div className="flex mx-auto bg-gray-900 h-16 rounded-t-md inner-box-shadow box-border border border-black">
              <button className="btn btn-secondary w-2/4 h-12 m-2 font-semibold text-shadow-md" setpage="1" onClick={this.back}>Zurück</button>
              <button className="btn btn-primary w-2/4 h-12 m-2 font-semibold text-shadow-md" setpage="2" onClick={this.next}>Weiter</button>
            </div>
            {/* Geschlecht */}
            <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
              <div className="flex">
                <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Geschlecht</p>
                <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md" id="gender">Männlich</p>
              </div>
              <div className="flex">
                <button className="btn btn-primary w-2/4 h-10 m-2 font-semibold text-shadow-md" id="male" onClick={this.changeMale}>Männlich</button>
                <button className="btn btn-primary w-2/4 h-10 m-2 font-semibold text-shadow-md" id="female" onClick={this.changeFemale}>Weiblich</button>
              </div>
            </div>
            {/* Gesicht vom Vater */}
            <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
              <div className="flex">
                <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Gesicht vom Vater</p>
                <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md" id="gender">{this.fatherFaceRef.current} | {faceNames.length - 1}</p>
              </div>
              <div className="m-4">
                <InputNumber ref={this.fatherFaceRef} id="fatherFace" type={1} values={faceNames} current={0} min={0} max={faceNames.length - 1}></InputNumber>
              </div>
            </div>
            {/* Haut vom Vater */}
            <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
              <div className="flex">
                <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Gesicht vom Vater</p>
                <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md" id="gender">{faceNames.length - 1}</p>
              </div>
              <div className="m-4">
                <InputNumber id="fatherSkin" type={1} values={faceNames} current={0} min={0} max={faceNames.length - 1}></InputNumber>
              </div>
            </div>
            {/* Gesicht der Mutter */}
            <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
              <div className="flex">
                <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Gesicht der Mutter</p>
                <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md" id="gender">{faceNames.length - 1}</p>
              </div>
              <div className="m-4">
                <InputNumber id="motherFace" type={1} values={faceNames} current={0} min={0} max={faceNames.length - 1}></InputNumber>
              </div>
            </div>
            {/* Haut der Mutter */}
            <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
              <div className="flex">
                <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Haut der Mutter</p>
                <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md" id="gender">{faceNames.length - 1}</p>
              </div>
              <div className="m-4">
                <InputNumber id="motherSkin" type={1} values={faceNames} current={0} min={0} max={faceNames.length - 1}></InputNumber>
              </div>
            </div>
          </div>
        </div>
      )
    } else if (this.state.page == 2) {
      return (
        <div>
          <div className="container ml-auto mr-4 mt-12 opacity-75 rounded-md shadow-xl inner-box-shadow text-shadow-md" style={{height: "900px", width: "450px"}}>
            {/* Back, Next */}
            <div className="flex mx-auto bg-gray-900 h-16 rounded-t-md inner-box-shadow box-border">
              <button className="btn btn-secondary w-2/4 h-12 m-2 text-shadow-md" setpage="1" onClick={this.back}>Zurück</button>
              <button className="btn btn-primary w-2/4 h-12 m-2 text-shadow-md" setpage="3" onClick={this.next}>Weiter</button>
            </div>
          </div>
        </div>
      )
    }
  }
}
