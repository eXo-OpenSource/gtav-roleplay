import React, { Component, createRef, useRef } from "react"
import InputNumber from "../../components/input-number"

import { faceNames } from "./utility/faces"
import { eyeColors } from "./utility/eyes"
import { maleHair, femaleHair, hairColor, facialHair, overlayColors, eyebrowNames} from "./utility/hairs"

export default class Charcreation extends Component {

  constructor(props) {
    super(props)

    this.state = {
      page: 1,
      hairs: maleHair,
      data: {
        sex: 1,
        faceFather: 33,
        faceMother: 45,
        skinFather: 45,
        skinMother: 45,
        faceMix: 50,
        skinMix: 50,
        hair: 11,
        hairColor1: 5,
        hairColor2: 2,
        hairOverlay: '',
        facialHair: 29,
        facialHairColor1: 1,
        facialHairOpacity: 1,
        eyebrows: 0,
        eyebrowsOpacity: 1,
        eyebrowsColor1: 0,
        eyes: 0,
        ageing: 0,
        noseWidth: 0.5,
        noseHeight: 0.5,
        noseBridge: 0.5,
        noseLength: 0.5,
        noseTip: 0.5,
        noseBridgeShift: 0.5,
        browHeight: 0.5,
        browWidth: 0.5,
        cheekboneHeight: 0.5,
        cheekboneWidth: 0.5,
        cheeksWidth: 0.5,
        lipWide: 0.5,
        eyeWide: 0.5,
        jawWidth: 0.5,
        jawHeight: 0.5,
        chinLength: 0.5,
        chinPosition: 0.5,
        chinShape: 0.5,
        chinWidth: 0.5,
        neckWidth: 0.5,
      },
      name: "Kenzo",
      surname: "Shayan"
    }

    this.fatherFaceRef = React.createRef()
    this.fatherSkinRef = React.createRef()
    this.motherFaceRef = React.createRef()
    this.motherSkinRef = React.createRef()
    this.eyecolorRef = React.createRef()
    this.eyebrowsRef = React.createRef()
    this.hairRef = React.createRef()
    this.hairColorRef = React.createRef()
    this.hairHighlightRef = React.createRef()
    this.facialHairRef = React.createRef()
    this.facialHairColorRef = React.createRef()

    this.next = this.next.bind(this)
    this.back = this.back.bind(this)
    this.changeMale = this.changeMale.bind(this)
    this.changeFemale = this.changeFemale.bind(this)
    this.nameChange = this.nameChange.bind(this)
    this.surnameChange = this.surnameChange.bind(this)
  }

  componentDidMount() {
    this.next()
    if ("alt" in window) {
      alt.on("FaceFeatures:StartScenario", this.startScenario.bind(this))
      alt.on("FaceFeatures:FadeOut", this.fadeOut.bind(this))
    }
  }

  calculateMix(value) {
    return Math.round((parseFloat(value) / 100) * 100) / 100
  }

  addEvents() {
    setTimeout(() => {
      if (this.state.page == 1) {
        document.getElementById("fatherFace").addEventListener("mousemove", this.changeFatherFace.bind(this))
        document.getElementById("fatherSkin").addEventListener("mousemove", this.changeFatherSkin.bind(this))
        document.getElementById("motherFace").addEventListener("mousemove", this.changeMotherFace.bind(this))
        document.getElementById("motherSkin").addEventListener("mousemove", this.changeMotherSkin.bind(this))
      } else if (this.state.page == 3) {
        document.getElementById("eyeColor").addEventListener("mousemove", this.changeEyecolor.bind(this))
        document.getElementById("eyebrows").addEventListener("mousemove", this.changeEyebrows.bind(this))
        document.getElementById("hair").addEventListener("mousemove", this.changeHair.bind(this))
        document.getElementById("hairColor").addEventListener("mousemove", this.changeHairColor.bind(this))
        document.getElementById("hairHighlight").addEventListener("mousemove", this.changeHairhighlight.bind(this))
        document.getElementById("facialHair").addEventListener("mousemove", this.changeFacialHair.bind(this))
        document.getElementById("facialHairColor").addEventListener("mousemove", this.changeFacialHairColor.bind(this))
      } else if (this.state.page == 4) {
        document.getElementById("facialHairColor").addEventListener("mousemove", this.changeFacialHairColor.bind(this))
      }
    }, 500)
  }

  next(e) {
    if (e) {
      this.setState({page: e.target.getAttribute("setpage")})
      if (this.state.page == 4) {
        if (this.state.name.length >= 3 && this.state.surname.length >= 3) {
          alt.emit("FaceFeatures:Finished", this.state.name, this.state.surname, this.state.data)
        }
      }
    }
    // Wait until component is loaded
    this.addEvents()
    this.forceUpdate()
  }

  back(e) {
    this.setState({page: e.target.getAttribute("setpage")})
    this.addEvents()
    this.forceUpdate()
  }

  changeMale(e) {
    this.setState({hairs: maleHair})
    var data = {...this.state.data}
    data.sex = 1;
    this.setState({data})
    this.forceUpdate()

    e.target.className = "btn btn-primary w-2/4 h-10 m-2 font-semibold text-shadow-md"
    document.getElementById("female").className = "btn btn-primary bg-blue-700 w-2/4 h-10 m-2 font-semibold text-shadow-md"
    document.getElementById("gender").innerText = "Männlich"
    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  changeFemale(e) {
    this.setState({hairs: femaleHair})
    var data = {...this.state.data}
    data.sex = 0;
    this.setState({data})
    this.forceUpdate()

    e.target.className = "btn btn-primary w-2/4 h-10 m-2 font-semibold text-shadow-md"
    document.getElementById("male").className = "btn btn-primary bg-blue-700 w-2/4 h-10 m-2 font-semibold text-shadow-md"
    document.getElementById("gender").innerText = "Weiblich"
    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  changeFatherSkin(e) {
    var data = {...this.state.data}
    data.skinFather = this.fatherSkinRef.current.cur;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  changeFatherFace() {
    var data = {...this.state.data}
    data.faceFather = this.fatherFaceRef.current.cur;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  changeMotherSkin(e) {
    var data = {...this.state.data}
    data.skinMother = this.motherSkinRef.current.cur;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  changeMotherFace() {
    var data = {...this.state.data}
    data.faceMother = this.motherFaceRef.current.cur;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  changeEyecolor() {
    var data = {...this.state.data}
    data.eyes = this.eyecolorRef.current.cur;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  changeEyebrows() {
    var data = {...this.state.data}
    data.eyebrows = this.eyebrowsRef.current.cur;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  changeHair() {
    var data = {...this.state.data}
    data.hair = this.hairRef.current.cur;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  changeHairColor() {
    var data = {...this.state.data}
    data.hairColor1 = this.hairColorRef.current.cur;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  changeHairhighlight() {
    var data = {...this.state.data}
    data.hairColor2 = this.hairHighlightRef.current.cur;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  changeFacialHair() {
    if (this.state.data.sex == 0) return
    var data = {...this.state.data}
    data.facialHair = this.facialHairRef.current.cur;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  changeFacialHairColor() {
    if (this.state.data.sex == 0) return
    var data = {...this.state.data}
    data.facialHairColor1 = this.facialHairColorRef.current.cur;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  faceMixChange(e) {
    var data = {...this.state.data}
    data.faceMix = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  skinMixChange(e) {
    var data = {...this.state.data}
    data.skinMix = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  ageingChange(e) {
    var data = {...this.state.data}
    data.ageing = e.target.value;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  noseWidthChange(e) {
    var data = {...this.state.data}
    data.noseWidth = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  noseHeightChange(e) {
    var data = {...this.state.data}
    data.noseHeight = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  noseLengthChange(e) {
    var data = {...this.state.data}
    data.noseLength = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  noseBridgeChange(e) {
    var data = {...this.state.data}
    data.noseBridge = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  noseBridgeShiftChange(e) {
    var data = {...this.state.data}
    data.noseBridgeShift = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  noseTipChange(e) {
    var data = {...this.state.data}
    data.noseTip = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  browHeightChange(e) {
    var data = {...this.state.data}
    data.browHeight = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  browWidthChange(e) {
    var data = {...this.state.data}
    data.browWidth = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  cheekboneHeightChange(e) {
    var data = {...this.state.data}
    data.cheekboneHeight = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  cheekboneWidthChange(e) {
    var data = {...this.state.data}
    data.cheekboneWidth = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  eyeWideChange(e) {
    var data = {...this.state.data}
    data.eyeWide = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  lipWideChange(e) {
    var data = {...this.state.data}
    data.lipWide = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  cheeksWidthChange(e) {
    var data = {...this.state.data}
    data.cheeksWidth = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  jawHeightChange(e) {
    var data = {...this.state.data}
    data.jawHeight = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  jawWidthChange(e) {
    var data = {...this.state.data}
    data.jawWidth = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  chinWidthChange(e) {
    var data = {...this.state.data}
    data.chinWidth = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  chinShapeChange(e) {
    var data = {...this.state.data}
    data.chinShape = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  chinPositionChange(e) {
    var data = {...this.state.data}
    data.chinPosition = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  chinLengthChange(e) {
    var data = {...this.state.data}
    data.chinLength = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  neckWidthChange(e) {
    var data = {...this.state.data}
    data.neckWidth = Math.round((parseFloat(e.target.value) / 100) * 100) / 100;
    this.setState({data})
    this.forceUpdate()

    setTimeout(() => {
      alt.emit("FaceFeatures:Update", this.state.data)
    }, 100);
  }

  nameChange(e) {
    let input = e.target.value.replace(/[^A-Za-z]/ig, "")
    this.setState({name: input})
    this.forceUpdate()
  }

  surnameChange(e) {
    let input = e.target.value.replace(/[^A-Za-z]/ig, "")
    this.setState({surname: input})
    this.forceUpdate()
  }

  fadeOut() {
    this.state.page = 5
    this.forceUpdate()
  }

  startScenario() {
    this.state.step = 4
    this.forceUpdate()
  }

  render() {
    if (this.state.page == 1) {
      return (
        <div>
          <div className="container ml-auto mr-4 mt-12 bg-black opacity-75 rounded-md shadow-xl inner-box-shadow" style={{height: "955px", width: "450px"}}>
            {/* Back, Next */}
            <div className="flex mx-auto bg-gray-900 h-16 rounded-t-md inner-box-shadow box-border border border-black">
              <button className="btn btn-secondary w-2/4 h-12 m-2 font-semibold text-shadow-md" setpage="1" onClick={this.back}>Zurück</button>
              <button className="btn btn-primary w-2/4 h-12 m-2 font-semibold text-shadow-md" setpage="2" onClick={this.next}>Weiter</button>
            </div>
            {/* Geschlecht */}
            <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
              <div className="flex">
                <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Geschlecht</p>
                <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md" id="gender">{this.state.gender == 1 ? "Männlich" : "Weiblich"}</p>
              </div>
              <div className="flex">
                <button className="btn btn-primary w-2/4 h-10 m-2 font-semibold text-shadow-md" id="male" onClick={this.changeMale}>Männlich</button>
                <button className="btn btn-primary w-2/4 h-10 m-2 font-semibold text-shadow-md" id="female" onClick={this.changeFemale}>Weiblich</button>
              </div>
            </div>
            {/* Gesicht vom Vater */}
            <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
              <div className="flex">
                <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Hautfarbe vom Vater</p>
                <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.skinFather} | {faceNames.length - 1}</p>
              </div>
              <div className="m-4" id="fatherSkin">
                <InputNumber ref={this.fatherSkinRef} type={1} values={faceNames} cur={this.state.data.skinFather} min={0} max={faceNames.length - 1}></InputNumber>
              </div>
            </div>
            {/* Haut vom Vater */}
            <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
              <div className="flex">
                <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Gesicht vom Vater</p>
                <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.faceFather} | {faceNames.length - 1}</p>
              </div>
              <div className="m-4" id="fatherFace">
                <InputNumber ref={this.fatherFaceRef} type={1} values={faceNames} cur={this.state.data.faceFather} min={0} max={faceNames.length - 1}></InputNumber>
              </div>
            </div>
            {/* Gesicht der Mutter */}
            <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
              <div className="flex">
                <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Gesicht der Mutter</p>
                <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.faceMother} | {faceNames.length - 1}</p>
              </div>
              <div className="m-4" id="motherFace">
                <InputNumber ref={this.motherFaceRef} type={1} values={faceNames} cur={this.state.data.faceMother} min={0} max={faceNames.length - 1}></InputNumber>
              </div>
            </div>
            {/* Haut der Mutter */}
            <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
              <div className="flex">
                <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Hautfarbe der Mutter</p>
                <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.skinMother} | {faceNames.length - 1}</p>
              </div>
              <div className="m-4" id="motherSkin">
                <InputNumber ref={this.motherSkinRef} type={1} values={faceNames} cur={this.state.data.skinMother} min={0} max={faceNames.length - 1}></InputNumber>
              </div>
            </div>
            {/* Skintone & Facemix */}
            <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
              <div className="flex">
                <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Gesichtsmix</p>
                <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.faceMix} | 1.0</p>
              </div>
              <div className="m-4">
                <input className="w-full" type="range" min="1" max="100" onChange={this.faceMixChange.bind(this)}/>
              </div>
              <div className="flex">
                <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Hautmix</p>
                <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.skinMix} | 1.0</p>
              </div>
              <div className="m-4">
                <input className="w-full" type="range" min="1" max="100" onChange={this.skinMixChange.bind(this)}/>
              </div>
            </div>
          </div>
        </div>
      )
    } else if (this.state.page == 2) {
      return (
        <div className="container ml-auto mr-4 mt-12 bg-black opacity-75 rounded-md shadow-xl inner-box-shadow" style={{height: "955px", width: "450px"}}>
          {/* Back, Next */}
          <div className="flex mx-auto bg-gray-900 h-16 rounded-t-md inner-box-shadow box-border">
            <button className="btn btn-secondary w-2/4 h-12 m-2 text-shadow-md" setpage="1" onClick={this.back}>Zurück</button>
            <button className="btn btn-primary w-2/4 h-12 m-2 text-shadow-md" setpage="3" onClick={this.next}>Weiter</button>
          </div>
          {/* Nase */}
          <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
            <div className="flex">
              <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Nasenbreite</p>
              <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">Nasenhöhe</p>
            </div>
            <div className="flex">
              <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.noseWidthChange.bind(this)}/>
              <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.noseHeightChange.bind(this)}/>
            </div>
            <div className="flex">
              <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Nasenlänge</p>
              <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">Nasenbrücke</p>
            </div>
            <div className="flex">
              <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.noseLengthChange.bind(this)}/>
              <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.noseBridgeChange.bind(this)}/>
            </div>
            <div className="flex">
              <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Nasenspitze</p>
              <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">Nasenkrümmung</p>
            </div>
            <div className="flex">
              <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.noseTipChange.bind(this)}/>
              <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.noseBridgeShiftChange.bind(this)}/>
            </div>
          </div>
          <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
            <div className="flex">
              <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Augenbrauenhöhe</p>
              <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">Augenbrauenbreite</p>
            </div>
            <div className="flex">
              <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.browHeightChange.bind(this)}/>
              <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.browWidthChange.bind(this)}/>
            </div>
          </div>
          <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
            <div className="flex">
              <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Wagenknochenhöhe</p>
              <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">Wangenknochenbreite</p>
            </div>
            <div className="flex">
              <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.cheekboneHeightChange.bind(this)}/>
              <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.cheekboneWidthChange.bind(this)}/>
            </div>
            <div className="flex">
                <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Wangenbreite</p>
                <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.cheeksWidth} | 1.0</p>
              </div>
            <div className="m-4">
              <input className="w-full" type="range" min="-100" max="100" onChange={this.cheeksWidthChange.bind(this)}/>
            </div>
          </div>
          <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
            <div className="flex">
              <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Augenweite</p>
              <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">Lippedicke</p>
            </div>
            <div className="flex">
              <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.eyeWideChange.bind(this)}/>
              <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.lipWideChange.bind(this)}/>
            </div>
          </div>
        </div>
      )
    } else if (this.state.page == 3) {
      return (
        <div className="container ml-auto mr-4 mt-12 bg-black opacity-75 rounded-md shadow-xl inner-box-shadow" style={{height: "955px", width: "450px"}}>
          {/* Back, Next */}
          <div className="flex mx-auto bg-gray-900 h-16 rounded-t-md inner-box-shadow box-border">
            <button className="btn btn-secondary w-2/4 h-12 m-2 text-shadow-md" setpage="2" onClick={this.back}>Zurück</button>
            <button className="btn btn-primary w-2/4 h-12 m-2 text-shadow-md" setpage="4" onClick={this.next}>Weiter</button>
          </div>
          {/* Augenfarbe */}
          <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
            <div className="flex">
              <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Augenfarbe</p>
              <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.eyes} | {eyeColors.length - 1}</p>
            </div>
            <div className="m-4" id="eyeColor">
              <InputNumber ref={this.eyecolorRef} type={1} values={eyeColors} cur={this.state.data.eyes} min={0} max={eyeColors.length - 1}></InputNumber>
            </div>
          </div>
          {/* Augenbrauen */}
          <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
            <div className="flex">
              <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Augenbrauen</p>
              <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.eyebrows} | {eyebrowNames.length - 1}</p>
            </div>
            <div className="m-4" id="eyebrows">
              <InputNumber ref={this.eyebrowsRef} type={1} values={eyebrowNames} cur={this.state.data.eyebrows} min={0} max={eyebrowNames.length - 1}></InputNumber>
            </div>
          </div>
          {/* Haare */}
          <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
            <div className="flex">
              <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Haare</p>
              <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.hair} | {this.state.hairs.length - 1}</p>
            </div>
            <div className="m-4" id="hair">
              <InputNumber ref={this.hairRef} type={1} values={this.state.hairs} cur={this.state.data.hair} min={0} max={this.state.hairs.length - 1}></InputNumber>
            </div>
            <div className="flex">
              <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Haarfarbe</p>
                <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.hairColor1} | {hairColor.length - 1}</p>
              </div>
            <div className="m-4" id="hairColor">
              <InputNumber ref={this.hairColorRef} type={1} values={hairColor} cur={this.state.data.hairColor1} min={0} max={hairColor.length - 1}></InputNumber>
            </div>
            <div className="flex">
              <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Haarhighlights</p>
              <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.hairColor2} | {overlayColors.length - 1}</p>
            </div>
            <div className="m-4" id="hairHighlight">
              <InputNumber ref={this.hairHighlightRef} type={1} values={overlayColors} cur={this.state.data.hairColor2} min={0} max={overlayColors.length - 1}></InputNumber>
            </div>
          </div>
          {/* Bart */}
          <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
            <div className="flex">
              <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Gesichtsbehaarung</p>
              <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.facialHair} | {facialHair.length - 1}</p>
            </div>
            <div className="m-4" id="facialHair">
              <InputNumber ref={this.facialHairRef} type={1} values={facialHair} cur={this.state.data.facialHair} min={0} max={facialHair.length - 1}></InputNumber>
            </div>
            <div className="flex">
              <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Farbe</p>
              <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.facialHairColor1} | {hairColor.length - 1}</p>
            </div>
            <div className="m-4" id="facialHairColor">
              <InputNumber ref={this.facialHairColorRef} type={1} values={hairColor} cur={this.state.data.facialHairColor1} min={0} max={hairColor.length - 1}></InputNumber>
            </div>
          </div>
        </div>
      )
    } else if (this.state.page == 4) {
      return (
        <div className="container ml-auto mr-4 mt-12 bg-black opacity-75 rounded-md shadow-xl inner-box-shadow" style={{height: "955px", width: "450px"}}>
          {/* Back, Next */}
          <div className="flex mx-auto bg-gray-900 h-16 rounded-t-md inner-box-shadow box-border">
            <button className="btn btn-secondary w-2/4 h-12 m-2 text-shadow-md" setpage="3" onClick={this.back}>Zurück</button>
            <button className="btn btn-primary w-2/4 h-12 m-2 text-shadow-md" setpage="5" onClick={this.next}>Einreisen</button>
          </div>
          <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
          <div className="flex">
            <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Kieferbreite</p>
            <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">Kieferhöhe</p>
          </div>
          <div className="flex">
            <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.jawWidthChange.bind(this)}/>
            <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.jawHeightChange.bind(this)}/>
          </div>
        </div>
        <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
          <div className="flex">
            <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Kinnlänge</p>
            <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">Kinnposition</p>
          </div>
          <div className="flex">
            <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.chinLengthChange.bind(this)}/>
            <input className="w-full m-4" type="range" min="0" max="100" onChange={this.chinPositionChange.bind(this)}/>
          </div>
          <div className="flex">
            <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Kinnbreite</p>
            <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">Kinnform</p>
          </div>
          <div className="flex">
            <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.chinWidthChange.bind(this)}/>
            <input className="w-full m-4" type="range" min="-100" max="100" onChange={this.chinShapeChange.bind(this)}/>
          </div>
        </div>
        <div className="mx-5 mt-5 bg-gray-900 h-auto inner-box-shadow box-border border border-black">
          <div className="flex">
            <p className="w-2/4 text-left h-10 ml-4 pt-3 font-medium text-base text-white text-shadow-md">Altersfalten</p>
            <p className="w-2/4 text-right h-10 mr-4 pt-3 font-medium text-base text-white text-shadow-md">{this.state.data.ageing} | 15</p>
          </div>
          <div className="m-4">
            <input className="w-full" type="range" min="0" max="15" onChange={this.ageingChange.bind(this)}/>
          </div>
        </div>
        <hr className="mt-6 mb-2 border-gray-900"></hr>
        {/* Charinfo */}
        <div className="p-4">
          <div className="mb-2 rounded bg-gray-800 border-l-4 text-white border-red-500 p-2">Bei der Wahl eines Charakternamens ist es ausdrücklich verboten Namen von realen Personen, Schauspielern oder Künstlern zu verwenden. Genauso ist jeder Name verboten, welcher gegen die Verhaltensnormen von eXo Roleplay verstößt! Wir empfehlen genau so nicht den echten Namen zu verwenden.</div>
          <p className="font-bold mt-4 mb-3 text-white">Vorname</p>
          <input className="bg-gray-400 text-gray-900 rounded w-full appearance-none py-2 px-3" pattern="[A-Za-z]" value={this.state.name} onChange={this.nameChange} type="text"/>
          <p className="font-bold mt-4 mb-3 text-white">Nachname</p>
          <input className="bg-gray-400 text-gray-900 rounded w-full appearance-none py-2 px-3" pattern="[A-Za-z]" value={this.state.surname} onChange={this.surnameChange} type="text"/>
        </div>
      </div>
      )
    } else {return null}
  }
}
