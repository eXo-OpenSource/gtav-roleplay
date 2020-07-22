import React, { Component } from 'react'

// All necessecary tables (~71)
export const fathers = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 42, 43, 44]; // 24
export const mothers = [21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 45]; // 22

export const fatherNames = ["Benjamin", "Daniel", "Joshua", "Noah", "Andrew", "Juan", "Alex", "Isaac", "Evan", "Ethan", "Vincent", "Angel", "Diego", "Adrian", "Gabriel", "Michael", "Santiago", "Kevin", "Louis", "Samuel", "Anthony", "Claude", "Niko", "John"];
export const motherNames = ["Hannah", "Aubrey", "Jasmine", "Gisele", "Amelia", "Isabella", "Zoe", "Ava", "Camila", "Violet", "Sophia", "Evelyn", "Nicole", "Ashley", "Gracie", "Brianna", "Natalie", "Olivia", "Elizabeth", "Charlotte", "Emma", "Misty"];
export const hairList = [ /*male*/ [{ ID: 0, Name: "Close Shave", Collection: "mpbeach_overlays", Overlay: "FM_Hair_Fuzz" }, { ID: 1, Name: "Buzzcut", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_001" }, { ID: 2, Name: "Faux Hawk", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_002" }, { ID: 3, Name: "Hipster", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_003" }, { ID: 4, Name: "Side Parting", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_004" }, { ID: 5, Name: "Shorter Cut", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_005" }, { ID: 6, Name: "Biker", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_006" }, { ID: 7, Name: "Ponytail", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_007" }, { ID: 8, Name: "Cornrows", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_008" }, { ID: 9, Name: "Slicked", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_009" }, { ID: 10, Name: "Short Brushed", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_013" }, { ID: 11, Name: "Spikey", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_002" }, { ID: 12, Name: "Caesar", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_011" }, { ID: 13, Name: "Chopped", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_012" }, { ID: 14, Name: "Dreads", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_014" }, { ID: 15, Name: "Long Hair", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_015" }, { ID: 16, Name: "Shaggy Curls", Collection: "multiplayer_overlays", Overlay: "NGBea_M_Hair_000" }, { ID: 17, Name: "Surfer Dude", Collection: "multiplayer_overlays", Overlay: "NGBea_M_Hair_001" }, { ID: 18, Name: "Short Side Part", Collection: "multiplayer_overlays", Overlay: "NGBus_M_Hair_000" }, { ID: 19, Name: "High Slicked Sides", Collection: "multiplayer_overlays", Overlay: "NGBus_M_Hair_001" }, { ID: 20, Name: "Long Slicked", Collection: "multiplayer_overlays", Overlay: "NGHip_M_Hair_000" }, { ID: 21, Name: "Hipster Youth", Collection: "multiplayer_overlays", Overlay: "NGHip_M_Hair_001" }, { ID: 22, Name: "Mullet", Collection: "multiplayer_overlays", Overlay: "NGInd_M_Hair_000" }, { ID: 24, Name: "Classic Cornrows", Collection: "mplowrider_overlays", Overlay: "LR_M_Hair_000" }, { ID: 25, Name: "Palm Cornrows", Collection: "mplowrider_overlays", Overlay: "LR_M_Hair_001" }, { ID: 26, Name: "Lightning Cornrows", Collection: "mplowrider_overlays", Overlay: "LR_M_Hair_002" }, { ID: 27, Name: "Whipped Cornrows", Collection: "mplowrider_overlays", Overlay: "LR_M_Hair_003" }, { ID: 28, Name: "Zig Zag Cornrows", Collection: "mplowrider2_overlays", Overlay: "LR_M_Hair_004" }, { ID: 29, Name: "Snail Cornrows", Collection: "mplowrider2_overlays", Overlay: "LR_M_Hair_005" }, { ID: 30, Name: "Hightop", Collection: "mplowrider2_overlays", Overlay: "LR_M_Hair_006" }, { ID: 31, Name: "Loose Swept Back", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_000_M" }, { ID: 32, Name: "Undercut Swept Back", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_001_M" }, { ID: 33, Name: "Undercut Swept Side", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_002_M" }, { ID: 34, Name: "Spiked Mohawk", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_003_M" }, { ID: 35, Name: "Mod", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_004_M" }, { ID: 36, Name: "Layered Mod", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_005_M" }, { ID: 72, Name: "Flattop", Collection: "mpgunrunning_overlays", Overlay: "MP_Gunrunning_Hair_M_000_M" }, { ID: 73, Name: "Military Buzzcut", Collection: "mpgunrunning_overlays", Overlay: "MP_Gunrunning_Hair_M_001_M" } ], /*female*/ [{ ID: 0, Name: "Close Shave", Collection: "mpbeach_overlays", Overlay: "FM_Hair_Fuzz" }, { ID: 1, Name: "Short", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_001" }, { ID: 2, Name: "Layered Bob", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_002" }, { ID: 3, Name: "Pigtails", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_003" }, { ID: 4, Name: "Ponytail", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_004" }, { ID: 5, Name: "Braided Mohawk", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_005" }, { ID: 6, Name: "Braids", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_006" }, { ID: 7, Name: "Bob", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_007" }, { ID: 8, Name: "Faux Hawk", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_008" }, { ID: 9, Name: "French Twist", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_009" }, { ID: 10, Name: "Long Bob", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_010" }, { ID: 11, Name: "Loose Tied", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_011" }, { ID: 12, Name: "Pixie", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_012" }, { ID: 13, Name: "Shaved Bangs", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_013" }, { ID: 14, Name: "Top Knot", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_014" }, { ID: 15, Name: "Wavy Bob", Collection: "multiplayer_overlays", Overlay: "NG_M_Hair_015" }, { ID: 16, Name: "Messy Bun", Collection: "multiplayer_overlays", Overlay: "NGBea_F_Hair_000" }, { ID: 17, Name: "Pin Up Girl", Collection: "multiplayer_overlays", Overlay: "NGBea_F_Hair_001" }, { ID: 18, Name: "Tight Bun", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_007" }, { ID: 19, Name: "Twisted Bob", Collection: "multiplayer_overlays", Overlay: "NGBus_F_Hair_000" }, { ID: 20, Name: "Flapper Bob", Collection: "multiplayer_overlays", Overlay: "NGBus_F_Hair_001" }, { ID: 21, Name: "Big Bangs", Collection: "multiplayer_overlays", Overlay: "NGBea_F_Hair_001" }, { ID: 22, Name: "Braided Top Knot", Collection: "multiplayer_overlays", Overlay: "NGHip_F_Hair_000" }, { ID: 23, Name: "Mullet", Collection: "multiplayer_overlays", Overlay: "NGInd_F_Hair_000" }, { ID: 25, Name: "Pinched Cornrows", Collection: "mplowrider_overlays", Overlay: "LR_F_Hair_000" }, { ID: 26, Name: "Leaf Cornrows", Collection: "mplowrider_overlays", Overlay: "LR_F_Hair_001" }, { ID: 27, Name: "Zig Zag Cornrows", Collection: "mplowrider_overlays", Overlay: "LR_F_Hair_002" }, { ID: 28, Name: "Pigtail Bangs", Collection: "mplowrider2_overlays", Overlay: "LR_F_Hair_003" }, { ID: 29, Name: "Wave Braids", Collection: "mplowrider2_overlays", Overlay: "LR_F_Hair_003" }, { ID: 30, Name: "Coil Braids", Collection: "mplowrider2_overlays", Overlay: "LR_F_Hair_004" }, { ID: 31, Name: "Rolled Quiff", Collection: "mplowrider2_overlays", Overlay: "LR_F_Hair_006" }, { ID: 32, Name: "Loose Swept Back", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_000_F" }, { ID: 33, Name: "Undercut Swept Back", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_001_F" }, { ID: 34, Name: "Undercut Swept Side", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_002_F" }, { ID: 35, Name: "Spiked Mohawk", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_003_F" }, { ID: 36, Name: "Bandana and Braid", Collection: "multiplayer_overlays", Overlay: "NG_F_Hair_003" }, { ID: 37, Name: "Layered Mod", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_006_F" }, { ID: 38, Name: "Skinbyrd", Collection: "mpbiker_overlays", Overlay: "MP_Biker_Hair_004_F" }, { ID: 76, Name: "Neat Bun", Collection: "mpgunrunning_overlays", Overlay: "MP_Gunrunning_Hair_F_000_F" }, { ID: 77, Name: "Kurzer Bob", Collection: "mpgunrunning_overlays", Overlay: "MP_Gunrunning_Hair_F_001_F" } ]];
export const maxHairColor = 64;
export const maxEyeColor = 12;
export const maxBlushColor = 27;
export const maxLipstickColor = 32;
export const eyeColors = ["Grün", "Emerald", "Hellblau", "Ozeanblau", "Hellbraun", "Dunkelbraun", "Haselnuss", "Dunkelgrau", "Hellgrau", "Pink", "Gelb", "Lila", "Blackout", "Shades of Gray", "Tequila Sunrise", "Atomar", "Warp", "ECola", "Space Ranger", "Ying Yang", "Bullseye", "Echse", "Drache", "Ausserirdisch", "Ziege", "Smiley", "Besessen", "Dämon", "Infiziert", "Alien", "Untot", "Zombie"];

class CharacterCreatorForm extends Component {

    constructor(props) {
        super(props)
        this.state = {
            gender: 0,
            fatherName: "Benjamin",
            motherName: "Hannah",
            fatherID: 0,
            motherID: 21,
            skinInheritance: 1.00,
            lookInheritance: 1.00
        }
    }

    componentDidMount() {
        if ("alt" in window) {
            //alt.on("FaceFeatures:UpdateSex", this.updateSex.bind(this))
        }
    }

    resetProperties() {
        this.setState({ gender: 0 })
        this.setState({ fatherName: "unselektiert" })
        this.setState({ motherName: "unselektiert" })
        this.setState({ fatherID: 0 })
        this.setState({ motherID: 21 })
        this.setState({ skinInheritance: 1.00 })
        this.setState({ lookInheritance: 1.00 })
    }

    updateSex(event) {
        let gender = parseInt(event.target.getAttribute('data-arg'))
        if (gender > 0) {
            alt.emit("FaceFeatures:UpdateSex", 1)
        } else {
            alt.emit("FaceFeatures:UpdateSex", 0)
        }
    }
    
    updateParent(event) {
        let parentType = event.target.getAttribute("data-arg")
        if (parentType == "father") {
            let father = fatherNames[event.target.value-1]
            this.setState({ fatherName: father })
            this.setState({ fatherID: event.target.value-1 })
            alt.emit("FaceFeatures:UpdateParent", this.state.fatherID, this.state.motherID, this.state.skinInheritance, this.state.lookInheritance)
        } else {
            let mother = motherNames[event.target.value-1]
            this.setState({ motherName: mother })
            this.setState({ motherID: event.target.value-1 })
            alt.emit("FaceFeatures:UpdateParent", this.state.fatherID, this.state.motherID, this.state.skinInheritance, this.state.lookInheritance)
        }
    }

    updateInheritance(event) {
        let slider = event.target.getAttribute("data-arg")
        if (slider == "skinInheritance") {
            let skin = event.target.value
            this.setState({ skinInheritance: parseFloat(skin) / 100.0 })
            alt.emit("FaceFeatures:UpdateParent", this.state.fatherID, this.state.motherID, this.state.skinInheritance, this.state.lookInheritance)
        } else if (slider == "lookInheritance") {
            let look = event.target.value
            this.setState({ lookInheritance: parseFloat(look) / 100.0 })
            alt.emit("FaceFeatures:UpdateParent", this.state.fatherID, this.state.motherID, this.state.skinInheritance, this.state.lookInheritance)
        }
    }
    
    render() {
        return (
            <div>
                <div className="container ml-auto mr-4 mt-12 max-w-md text-gray-200">
                    <div className="card">
                        <div className="card-header italic">Charaktererstellung</div>
                        <div className="card-body overflow-y-hidden max-h-screen">
                            {/* Select gender */}
                            <p className="text-center uppercase font-bold mt-4 mb-3 flex">Mein Geschlecht ist...</p>
                            <div className="flex">
                                <button className="w-2/4 bg-indigo-400 py-5 rounded-md rounded-r-none text-white text-center" data-arg="1" onClick={this.updateSex}>Männlich</button>
                                <button className="w-2/4 bg-gray-700 py-5 rounded rounded-l-none text-white text-center" data-arg="0" onClick={this.updateSex}>Weiblich</button> 
                            </div>
                            <p className="text-center uppercase font-bold mt-4 mb-3 flex">Dein Vater ist {this.state.fatherName}</p>
                            <input type="range" min="1" max={fathers.length} class="slider" id="myRange" className="w-full rounded-lg bg-blue-700" data-arg="father" onChange={this.updateParent.bind(this)}></input>
                            <p className="text-center uppercase font-bold mt-4 mb-3 flex">Deine Mutter ist {this.state.motherName}</p>
                            <input type="range" min="1" max={mothers.length} class="slider" id="myRange" className="w-full rounded-lg bg-blue-700" data-arg="mother" onChange={this.updateParent.bind(this)}></input>
                            <p className="text-center uppercase font-bold mt-4 mb-3 flex">Passe deine Hautfarbe an! {this.state.skinInheritance}</p>
                            <div className="flex">
                                <p className="uppercase font-bold mt-4 mb-3 flex px-6">← Mutter </p>
                                <input type="range" min="1" max="100" class="slider" id="myRange" className="rounded-lg bg-blue-700content-center mx-3" data-arg="skinInheritance" onChange={this.updateInheritance.bind(this)}></input>
                                <p className="uppercase font-bold mt-4 mb-3 flex px-6">Vater →</p>
                            </div>
                            <p className="text-center uppercase font-bold mt-4 mb-3 flex">Passe dein Aussehen an! {this.state.lookInheritance}</p>
                            <div className="flex">
                                <p className="uppercase font-bold mt-4 mb-3 flex px-6">← Mutter</p>
                                <input type="range" min="1" max="100" class="slider" id="myRange" className="rounded-lg bg-blue-700 content-center mx-3" data-arg="lookInheritance" onChange={this.updateInheritance.bind(this)}></input>
                                <p className="uppercase font-bold mt-4 mb-3 flex px-6">Vater →</p>
                            </div>
                        </div>
                        <div className="card-footer">
                            <button className="btn btn-primary" onClick={this.onLoginClick}>Charakter erstellen</button>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}

export default CharacterCreatorForm;