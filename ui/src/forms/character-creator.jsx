import React, { Component } from "react"

// All necessecary tables (~71)
const fathers = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 42, 43, 44] // 24
const mothers = [21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 45] // 22

const fatherNames = ["Benjamin", "Daniel", "Joshua", "Noah", "Andrew", "Juan", "Alex", "Isaac", "Evan", "Ethan", "Vincent", "Angel", "Diego", "Adrian", "Gabriel", "Michael", "Santiago", "Kevin", "Louis", "Samuel", "Anthony", "Claude", "Niko", "John"];
const motherNames = ["Hannah", "Aubrey", "Jasmine", "Gisele", "Amelia", "Isabella", "Zoe", "Ava", "Camila", "Violet", "Sophia", "Evelyn", "Nicole", "Ashley", "Gracie", "Brianna", "Natalie", "Olivia", "Elizabeth", "Charlotte", "Emma", "Misty"];
const hairList = [ /*female*/ [{ ID: 0, Name: "Close Shave", Collection: "mpbeach_overlays", Overlay: "FM_Hair_Fuzz" }, { ID: 1, Name: "Short", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_001" }, { ID: 2, Name: "Layered Bob", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_002" }, { ID: 3, Name: "Pigtails", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_003" }, { ID: 4, Name: "Ponytail", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_004" }, { ID: 5, Name: "Braided Mohawk", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_005" }, { ID: 6, Name: "Braids", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_006" }, { ID: 7, Name: "Bob", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_007" }, { ID: 8, Name: "Faux Hawk", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_008" }, { ID: 9, Name: "French Twist", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_009" }, { ID: 10, Name: "Long Bob", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_010" }, { ID: 11, Name: "Loose Tied", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_011" }, { ID: 12, Name: "Pixie", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_012" }, { ID: 13, Name: "Shaved Bangs", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_013" }, { ID: 14, Name: "Top Knot", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_014" }, { ID: 15, Name: "Wavy Bob", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_015" }, { ID: 16, Name: "Messy Bun", Collection: "multiplayer_overlays", Overlay: "NGBea_F_Hair_000" }, { ID: 17, Name: "Pin Up Girl", Collection: "multiplayer_overlays", Overlay: "NGBea_F_Hair_001" }, { ID: 18, Name: "Tight Bun", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_007" }, { ID: 19, Name: "Twisted Bob", Collection: "multiplayer_overlays", Overlay: "NGBus_F_Hair_000" }, { ID: 20, Name: "Flapper Bob", Collection: "multiplayer_overlays", Overlay: "NGBus_F_Hair_001" }, { ID: 21, Name: "Big Bangs", Collection: "multiplayer_overlays", Overlay: "NGBea_F_Hair_001" }, { ID: 22, Name: "Braided Top Knot", Collection: "multiplayer_overlays", Overlay: "NGHip_F_Hair_000" }, { ID: 23, Name: "Mullet", Collection: "multiplayer_overlays", Overlay: "NGInd_F_Hair_000" }, { ID: 25, Name: "Pinched Cornrows", Collection: "mplowrider_overlays", Overlay: "LR_F_Hair_000" }, { ID: 26, Name: "Leaf Cornrows", Collection: "mplowrider_overlays", Overlay: "LR_F_Hair_001" }, { ID: 27, Name: "Zig Zag Cornrows", Collection: "mplowrider_overlays", Overlay: "LR_F_Hair_002" }, { ID: 28, Name: "Pigtail Bangs", Collection: "mplowrider2_overlays", Overlay: "LR_F_Hair_003" }, { ID: 29, Name: "Wave Braids", Collection: "mplowrider2_overlays", Overlay: "LR_F_Hair_003" }, { ID: 30, Name: "Coil Braids", Collection: "mplowrider2_overlays", Overlay: "LR_F_Hair_004" }, { ID: 31, Name: "Rolled Quiff", Collection: "mplowrider2_overlays", Overlay: "LR_F_Hair_006" }, { ID: 32, Name: "Loose Swept Back", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_000_F" }, { ID: 33, Name: "Undercut Swept Back", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_001_F" }, { ID: 34, Name: "Undercut Swept Side", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_002_F" }, { ID: 35, Name: "Spiked Mohawk", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_003_F" }, { ID: 36, Name: "Bandana and Braid", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_003" }, { ID: 37, Name: "Layered Mod", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_006_F" }, { ID: 38, Name: "Skinbyrd", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_004_F" }, { ID: 76, Name: "Neat Bun", Collection: "mpgunrunning_overlays", Overlay: "MP_Gunrunning_Hair_F_000_F" }, { ID: 77, Name: "Kurzer Bob", Collection: "mpgunrunning_overlays", Overlay: "MP_Gunrunning_Hair_F_001_F" } ], /* male */ [{ ID: 0, Name: "Close Shave", Collection: "mpbeach_overlays", Overlay: "FM_Hair_Fuzz" }, { ID: 1, Name: "Buzzcut", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_001" }, { ID: 2, Name: "Faux Hawk", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_002" }, { ID: 3, Name: "Hipster", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_003" }, { ID: 4, Name: "Side Parting", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_004" }, { ID: 5, Name: "Shorter Cut", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_005" }, { ID: 6, Name: "Biker", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_006" }, { ID: 7, Name: "Ponytail", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_007" }, { ID: 8, Name: "Cornrows", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_008" }, { ID: 9, Name: "Slicked", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_009" }, { ID: 10, Name: "Short Brushed", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_013" }, { ID: 11, Name: "Spikey", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_002" }, { ID: 12, Name: "Caesar", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_011" }, { ID: 13, Name: "Chopped", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_012" }, { ID: 14, Name: "Dreads", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_014" }, { ID: 15, Name: "Long Hair", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_015" }, { ID: 16, Name: "Shaggy Curls", Collection: "multiplayer_overlays", Overlay: "NGBea_M_Hair_000" }, { ID: 17, Name: "Surfer Dude", Collection: "multiplayer_overlays", Overlay: "NGBea_M_Hair_001" }, { ID: 18, Name: "Short Side Part", Collection: "multiplayer_overlays", Overlay: "NGBus_M_Hair_000" }, { ID: 19, Name: "High Slicked Sides", Collection: "multiplayer_overlays", Overlay: "NGBus_M_Hair_001" }, { ID: 20, Name: "Long Slicked", Collection: "multiplayer_overlays", Overlay: "NGHip_M_Hair_000" }, { ID: 21, Name: "Hipster Youth", Collection: "multiplayer_overlays", Overlay: "NGHip_M_Hair_001" }, { ID: 22, Name: "Mullet", Collection: "multiplayer_overlays", Overlay: "NGInd_M_Hair_000" }, { ID: 24, Name: "Classic Cornrows", Collection: "mplowrider_overlays", Overlay: "LR_M_Hair_000" }, { ID: 25, Name: "Palm Cornrows", Collection: "mplowrider_overlays", Overlay: "LR_M_Hair_001" }, { ID: 26, Name: "Lightning Cornrows", Collection: "mplowrider_overlays", Overlay: "LR_M_Hair_002" }, { ID: 27, Name: "Whipped Cornrows", Collection: "mplowrider_overlays", Overlay: "LR_M_Hair_003" }, { ID: 28, Name: "Zig Zag Cornrows", Collection: "mplowrider2_overlays", Overlay: "LR_M_Hair_004" }, { ID: 29, Name: "Snail Cornrows", Collection: "mplowrider2_overlays", Overlay: "LR_M_Hair_005" }, { ID: 30, Name: "Hightop", Collection: "mplowrider2_overlays", Overlay: "LR_M_Hair_006" }, { ID: 31, Name: "Loose Swept Back", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_000_M" }, { ID: 32, Name: "Undercut Swept Back", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_001_M" }, { ID: 33, Name: "Undercut Swept Side", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_002_M" }, { ID: 34, Name: "Spiked Mohawk", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_003_M" }, { ID: 35, Name: "Mod", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_004_M" }, { ID: 36, Name: "Layered Mod", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_005_M" }, { ID: 72, Name: "Flattop", Collection: "mpgunrunning_overlays", Overlay: "MP_Gunrunning_Hair_M_000_M" }, { ID: 73, Name: "Military Buzzcut", Collection: "mpgunrunning_overlays", Overlay: "MP_Gunrunning_Hair_M_001_M" } ]];
const maxHairColor = 64
const maxEyeColor = 12
const maxAgeing = 14
const maxEyebrows = 33
const maxBeards = 28
const maxMoles = 16
const eyeColors = ["Grün", "Emerald", "Hellblau", "Ozeanblau", "Hellbraun", "Dunkelbraun", "Haselnuss", "Dunkelgrau", "Hellgrau", "Pink", "Gelb", "Lila", "Blackout", "Shades of Gray", "Tequila Sunrise", "Atomar", "Warp", "ECola", "Space Ranger", "Ying Yang", "Bullseye", "Echse", "Drache", "Ausserirdisch", "Ziege", "Smiley", "Besessen", "Dämon", "Infiziert", "Alien", "Untot", "Zombie"];

class CharacterCreatorForm extends Component {

    constructor(props) {
        super(props)
        this.state = {
            step: "1",
            gender: 0,
            fatherName: "Benjamin",
            motherName: "Hannah",
            fatherID: 0,
            motherID: 21,
            skinInheritance: 0.50,
            lookInheritance: 0.50,
            hairID: 0,
            hairColor: 0,
            hairHighlights: 0,
            eyebrowID: 18,
            eyebrowColor: 0,
            eyebrowHighlight: 0,
            eyeColor: 0,
            beardID: 0,
            beardColor: 0,
            ageing: 0,
            moles: 0,
            name: "Saul",
            surname: "Badman"
        }
    }

    componentDidMount() {
        if ("alt" in window) {
            alt.on("FaceFeatures:StartScenario", this.startScenario.bind(this))
            alt.on("FaceFeatures:FadeOut", this.fadeOut.bind(this))
        }
    }

    updateAgeing(e) {
        this.setState({ ageing: e.target.value - 1})
        alt.emit("FaceFeatures:UpdateAgeing", this.state.ageing)
    }

    updateMoles(e) {
        this.setState({ moles: e.target.value - 1})
        alt.emit("FaceFeatures:UpdateMoles", this.state.moles)
    }

    updateSex(e) {
        let gender = parseInt(e.target.getAttribute("data-arg"))
        this.setState({ gender: gender })
        alt.emit("FaceFeatures:UpdateSex", gender)
    }

    updateEyeColor(e) {
        this.setState({ eyeColor: e.target.value - 1})
        alt.emit("FaceFeatures:UpdateEyes", this.state.eyeColor)
    }

    updateBeard(e) {
        let slider = e.target.getAttribute("data-arg")
        if (slider == "beards") {
            this.setState({ beardID: e.target.value - 1 })
        } else {
            this.setState({ beardColor: e.target.value - 1 })
        }
        alt.emit("FaceFeatures:UpdateBeard", this.state.beardID, this.state.beardColor)
    }

    updateHairs(e) {
        let slider = e.target.getAttribute("data-arg")
        if (slider == "hairs") {
            this.setState({ hairID: e.target.value - 1 })
        } else if (slider == "hairColor") {
            this.setState({ hairColor: e.target.value - 1 })
        } else if (slider == "hairHighlights") {
            this.setState({ hairHighlights: e.target.value - 1 })
        } else if (slider == "eyebrows") {
            this.setState({ eyebrowID: e.target.value - 1 })
        } else if (slider == "eyebrowColor") {
            this.setState({ eyebrowColor: e.target.value - 1 })
        }

        alt.emit("FaceFeatures:UpdateHairs", this.state.hairID,
            this.state.hairColor, this.state.hairHighlights, this.state.eyebrowID,
            this.state.eyebrowColor, this.state.eyebrowHighlight
        )
    }
    
    updateParent(e) {
        let parentType = e.target.getAttribute("data-arg")
        if (parentType == "father") {
            let father = fatherNames[e.target.value - 1]
            this.setState({ fatherName: father })
            this.setState({ fatherID: e.target.value - 1 })
        } else {
            let mother = motherNames[e.target.value - 1]
            this.setState({ motherName: mother })
            this.setState({ motherID: e.target.value - 1 })
        }
        alt.emit("FaceFeatures:UpdateParent", this.state.fatherID,
            this.state.motherID, this.state.skinInheritance,
            this.state.lookInheritance
        )
    }

    updateInheritance(e) {
        let slider = e.target.getAttribute("data-arg")
        if (slider == "skinInheritance") {
            let skin = e.target.value
            this.setState({ skinInheritance: parseFloat(skin) / 100.0 })
        } else {
            let look = e.target.value
            this.setState({ lookInheritance: parseFloat(look) / 100.0 })
        }
        alt.emit("FaceFeatures:UpdateParent", this.state.fatherID, 
            this.state.motherID, this.state.skinInheritance,
            this.state.lookInheritance
        )
    }

    onChangeSiteClick(e) {
        this.state.step = e.target.getAttribute("step")
        this.forceUpdate()
        if (this.state.step == "4") {
            if (this.state.name.length >= 3 && this.state.surname.length >= 3) {
                alt.emit("FaceFeatures:Finished", this.state.name, this.state.surname)
            }
        }
    }

    onChangeName(e) {
        var currentState = this.state
        currentState[e.target.name] = e.target.value.replace(/[^A-Za-z]/ig, "")
        this.setState({ data: currentState })
    }

    fadeOut() {
        this.state.step = "5"
        this.forceUpdate()
    }

    startScenario() {
        this.state.step = "4"
        this.forceUpdate()
    }

    render() {
        if (this.state.step === "1") {
            return (
                <div>
                    <div className="container ml-auto mr-4 mt-12 max-w-md text-gray-200">
                        <div className="card">
                            <div className="card-header"><b>Charaktererstellung</b> - Allgemein</div>
                            <div className="card-body overflow-y-hidden max-h-screen">
                                {/* Select gender */}
                                <p className="text-center font-bold mt-4 mb-3 flex">Mein Geschlecht ist...</p>
                                <div className="flex">
                                    <button className="w-2/4 bg-indigo-400 py-5 rounded-md rounded-r-none text-white text-center" data-arg="1" onClick={this.updateSex.bind(this)}>Männlich</button>
                                    <button className="w-2/4 bg-gray-700 py-5 rounded rounded-l-none text-white text-center" data-arg="0" onClick={this.updateSex.bind(this)}>Weiblich</button> 
                                </div>
                                <p className="font-bold mt-4 mb-3">Wie alt willst du im Gesicht aussehen?</p>
                                <input type="range" min="1" max={maxAgeing} class="slider" className="w-full rounded-lg bg-blue-700" data-arg="ageing" onChange={this.updateAgeing.bind(this)}></input>
                                <p className="font-bold mt-4 mb-3">Dein Vater ist {this.state.fatherName}</p>
                                <input type="range" min="1" max={fathers.length} class="slider" className="w-full rounded-lg bg-blue-700" data-arg="father" onChange={this.updateParent.bind(this)}></input>
                                <p className="font-bold mt-4 mb-3">Deine Mutter ist {this.state.motherName}</p>
                                <input type="range" min="1" max={mothers.length} class="slider" className="w-full rounded-lg bg-blue-700" data-arg="mother" onChange={this.updateParent.bind(this)}></input>
                                <p className="font-bold mt-4">Passe dein Aussehen an</p>
                                <p className="text-sm mb-4">Das Aussehen regelst Du indem du zum Elternteil ziehst. </p>
                                <div className="flex">
                                    <p className="font-bold mt-4 mb-3 flex px-6">← Mutter </p>
                                    <input type="range" min="1" max="100" class="slider" className="rounded-lg bg-blue-700 content-center mx-5" data-arg="skinInheritance" onChange={this.updateInheritance.bind(this)}></input>
                                    <p className="font-bold mt-4 mb-3 flex px-6">Vater →</p>
                                </div>
                                <p className="font-bold mt-4 mb-3">Passe deine Hautfarbe an</p>
                                <div className="flex">
                                    <p className="font-bold mt-4 mb-3 flex px-6">← Mutter</p>
                                    <input type="range" min="1" max="100" class="slider" className="rounded-lg bg-blue-700 content-center mx-5" data-arg="lookInheritance" onChange={this.updateInheritance.bind(this)}></input>
                                    <p className="font-bold mt-4 mb-3 flex px-6">Vater →</p>
                                </div>
                            </div>
                            <div className="card-footer">
                            <button className="btn btn-primary" step="2" onClick={this.onChangeSiteClick.bind(this)}>Weiter zur Optik</button>
                            </div>
                        </div>
                    </div>
                </div>
            )
        } else if (this.state.step === "2") {
            return (
                <div>
                <div className="container ml-auto mr-4 mt-12 max-w-md text-gray-200">
                    <div className="card">
                        <div className="card-header"><b>Charaktererstellung</b> - Behaarung</div>
                        <div className="card-body overflow-y-hidden max-h-screen">
                        <p className="text-sm">In der Charaktererstellung geht es um das natürliche Aussehen.</p>
                        <p className="text-sm mb-4">Make-Up und andere Accessoires sind im Spielverlauf anpassbar.</p>
                            <p className="font-bold mt-4 mb-3">Haare: {hairList[this.state.gender][this.state.hairID].Name}</p>
                            <input type="range" min="1" max={hairList[this.state.gender].length - 1} class="slider" className="w-full rounded-lg bg-blue-700" data-arg="hairs" onChange={this.updateHairs.bind(this)}></input>
                            <div className="flex">
                                <p className="mt-4 mb-3 font-bold w-1/2 rounded-lg content-center mr-8">Haarfarbe</p>
                                <p className="mt-4 mb-3 font-bold w-1/2 rounded-lg content-center">Highlights</p>
                            </div>
                            <div className="flex">
                                <input type="range" min="1" max={maxHairColor} class="slider" className="w-1/2 rounded-lg bg-blue-700 content-center mr-8" data-arg="hairColor" onChange={this.updateHairs.bind(this)}></input>
                                <input type="range" min="1" max={maxHairColor} class="slider" className="w-1/2 rounded-lg bg-blue-700 content-center" data-arg="hairHighlights" onChange={this.updateHairs.bind(this)}></input>
                            </div>
                            <div className="flex">
                                <p className="mt-4 mb-3 font-bold w-1/2 rounded-lg content-center mr-8">Augenbrauen</p>
                                <p className="mt-4 mb-3 font-bold w-1/2 rounded-lg content-center">Farbe</p>
                            </div>
                            <div className="flex">
                                <input type="range" min="1" max={maxEyebrows} class="slider" className="w-1/2 rounded-lg bg-blue-700 content-center mr-8" data-arg="eyebrows" onChange={this.updateHairs.bind(this)}></input>
                                <input type="range" min="1" max={maxHairColor} class="slider" className="w-1/2 rounded-lg bg-blue-700 content-center" data-arg="eyebrowColor" onChange={this.updateHairs.bind(this)}></input>
                            </div>
                            <p className="font-bold mt-4 mb-3">Suche eine Augenfarbe aus</p>
                            <input type="range" min="1" max={maxEyeColor} class="slider" className="w-full rounded-lg bg-blue-700" data-arg="eyes" onChange={this.updateEyeColor.bind(this)}></input>
                            <div className="flex">
                                <p className="mt-4 mb-3 font-bold w-1/2 rounded-lg content-center mr-8">Bart</p>
                                <p className="mt-4 mb-3 font-bold w-1/2 rounded-lg content-center">Farbe</p>
                            </div>
                            <div className="flex">
                                <input type="range" min="1" max={maxBeards} class="slider" className="w-1/2 rounded-lg bg-blue-700 content-center mr-8" data-arg="beards" onChange={this.updateBeard.bind(this)}></input>
                                <input type="range" min="1" max={maxHairColor} class="slider" className="w-1/2 rounded-lg bg-blue-700 content-center" data-arg="beardColor" onChange={this.updateBeard.bind(this)}></input>
                            </div>
                            <p className="font-bold mt-4 mb-3">Gesichtsmerkmale</p>
                            <div className="flex">
                                <input type="range" min="1" max={maxMoles} class="slider" className="w-full rounded-lg bg-blue-700 content-center" data-arg="moles" onChange={this.updateMoles.bind(this)}></input>
                            </div>
                        </div>
                        <div className="card-footer">
                            <button className="btn" step="1" onClick={this.onChangeSiteClick.bind(this)}>Zurück zu Allgemein</button>
                            <button className="btn btn-primary" step="3" onClick={this.onChangeSiteClick.bind(this)}>Weiter</button>
                        </div>
                    </div>
                </div>
            </div>
            )
        } else if (this.state.step === "3") {
            return (
                <div>
                <div className="container ml-auto mr-4 mt-12 max-w-md text-gray-200">
                    <div className="card">
                        <div className="card-header"><b>Charaktererstellung</b> - Über dich</div>
                        <div className="card-body overflow-y-hidden max-h-screen">
                            <div className="mb-2 rounded bg-red-100 border-l-4 text-red-700 border-red-500 p-2">Bei der Wahl eines Charakternamens ist es ausdrücklich verboten Namen von realen Personen, Schauspielern oder Künstlern zu verwenden. Genauso ist jeder Name verboten, welcher gegen die Verhaltensnormen von eXo Roleplay verstößt! Wir empfehlen genau so nicht den echten Namen zu verwenden.</div>
                            <p className="font-bold mt-4 mb-3 italic">Dein Vorname</p>
                            <input className="bg-gray-400 text-gray-900 rounded w-full appearance-none py-2 px-3" pattern="[A-Za-z]" value={this.state.name} id="name" name="name" onChange={this.onChangeName.bind(this)} type="text"/>
                            <p className="font-bold mt-4 mb-3 italic">Dein Nachname</p>
                            <input className="bg-gray-400 text-gray-900 rounded w-full appearance-none py-2 px-3" pattern="[A-Za-z]" value={this.state.surname} id="surname" name="surname" onChange={this.onChangeName.bind(this)} type="text"/>
                            <p className="text-sm mt-6">Mit dem Klick auf "Fortfahren" bestätigst Du, dass du das Regelwerk von eXo-Roleplay gelesen hast.</p>
                        </div>
                        <div className="card-footer">
                            <button className="btn" step="2" onClick={this.onChangeSiteClick.bind(this)}>Zurück zu Behaarung</button>
                            <button className="btn btn-primary" step="4" onClick={this.onChangeSiteClick.bind(this)}>Charakter erstellen</button>
                        </div>
                    </div>
                </div>
            </div>
            )
        } else if (this.state.step === "4") {
            return (
                <div className="absolute w-full h-full bg-black mt-0">
                    <div id="scenario" style={{marginTop: "25rem"}}>
                        <img className="logo-fadeIn mx-auto" src="https://forum.exo-reallife.de/wsc/images/styleLogo-09a90a2e08be66fccb657b42536567cf7dc373d9.png"></img>
                    </div>
                </div> 
            )
        } else if (this.state.step === "5") {
            return (
                <div className="absolute w-full h-full bg-black mt-0">
                    <div id="scenario" style={{marginTop: "25rem"}}>
                        <img className="logo-fadeOut mx-auto" src="https://forum.exo-reallife.de/wsc/images/styleLogo-09a90a2e08be66fccb657b42536567cf7dc373d9.png"></img>
                    </div>
                </div> 
            )
        } else { return null }
    }
}

export default CharacterCreatorForm