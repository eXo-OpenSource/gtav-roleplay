import * as alt from "alt"
import * as native from "natives"
import { UiManager } from "./UiManager"
import { Ped } from "../systems/Ped"
import { Camera } from "../utils/Camera"
import { Vector3 } from "natives"

const url = "http://resource/cef/index.html#/charactercreator"

export class FaceFeaturesUi {
    private uiManager: UiManager
    private testPed: Ped
    private camera: Camera
    private cameraPoint: Vector3 = {
        x: -140.7032928466797,
        y: -644.9724975585938,
        z: 169.413232421875
    }
    private teleportPosition: Vector3 = {
        x: -130.7032928466797,
        y: -644.9724975585938,
        z: 169.413232421875
    }

    private playerPoint: Vector3 = {
        x: -140.45274353027344,
        y: -646.4044189453125,
        z: 168.813232421875
    }
    
    private model = "mp_m_freemode_01"
    private gender = 1
    private fatherID = 0
    private motherID = 21
    private skin = 0.5
    private look = 0.5
    private moleID = 0
    private blushID = 0
    private eyeColor = 0
    private hairID = 0
    private hairColor = 0
    private hairHighlight = 0
    private eyebrowID = 0
    private eyebrowColor = 0
    private age = 0
    private beard = 0
    private beardColor = 0
    private name = "Saul"
    private surname = "Badman"
    
    public constructor(uiManager) {
        this.uiManager = uiManager
        this.uiManager.navigate("/charactercreator", true)

        // Events
        this.uiManager.on("FaceFeatures:UpdateSex", this.updateSex.bind(this))
        this.uiManager.on("FaceFeatures:UpdateParent", this.updateParent.bind(this))
        this.uiManager.on("FaceFeatures:UpdateHairs", this.updateHairs.bind(this))
        this.uiManager.on("FaceFeatures:UpdateEyes", this.updateEyeColor.bind(this))
        this.uiManager.on("FaceFeatures:UpdateBeard", this.updateBeard.bind(this))
        this.uiManager.on("FaceFeatures:UpdateAgeing", this.updateAgeing.bind(this))
        this.uiManager.on("FaceFeatures:UpdateMoles", this.updateMoles.bind(this))
        this.uiManager.on("FaceFeatures:Finished", this.finished.bind(this))

        native.requestModel(native.getHashKey("mp_m_freemode_01"))
        native.requestModel(native.getHashKey("mp_f_freemode_01"))

        // Teleport for customization
        if (!this.testPed) {
            this.testPed = new Ped(this.model, this.playerPoint)
        }
        
        alt.emitServer("temporaryTeleport", this.teleportPosition)
        
        this.camera = new Camera(this.cameraPoint, 28)
        this.camera.pointAtBone(this.testPed.scriptID, 31086, 0.05, 0, 0)
        this.camera.playerControlsEntity(this.testPed.scriptID, true)
    }

    // Update sex
    private updateSex(value) {
        this.model = value == 0 ? "mp_f_freemode_01" : "mp_m_freemode_01"
        this.gender = value
        this.resetCamera(this.model)
        this.updateHeadOverlay()
    }

    // Update parent
    private updateParent(_fatherID, _motherID, _skin, _look) {
        this.fatherID = _fatherID
        this.motherID = _motherID
        this.skin = _skin
        this.look = _look
        this.resetCamera(this.model)
        this.updateHeadOverlay()
    }
    
    private updateHeadOverlay() {
        native.setPedHeadOverlay(this.testPed.scriptID, 9, this.moleID, 255)
        native.setPedHeadOverlay(this.testPed.scriptID, 9, this.blushID, 255)
        native.setPedEyeColor(this.testPed.scriptID, this.eyeColor)
        native.setPedComponentVariation(this.testPed.scriptID, 2, this.hairID, 0, 0)
        native.setPedHairColor(this.testPed.scriptID, this.hairColor, this.hairHighlight)
        native.setPedHeadOverlay(this.testPed.scriptID, 2, this.eyebrowID, 255)
        native.setPedHeadOverlayColor(this.testPed.scriptID, 2, 1, this.eyebrowColor, this.eyebrowColor)
        native.setPedHeadOverlay(this.testPed.scriptID, 3, this.age, 255)
        if (this.gender == 1) {
            native.setPedHeadOverlay(this.testPed.scriptID, 1, this.beard, 255)
            native.setPedHeadOverlayColor(this.testPed.scriptID, 1, 1, this.beardColor, this.beardColor)
        }
        native.setPedHeadBlendData(this.testPed.scriptID, 
            this.fatherID, this.motherID, 0,
            this.fatherID, this.motherID, 0, this.skin,
            this.look, 0, false
        )
    }

    // Update moles & freckles
    private updateMoles(_moleID, _blushID) {
        this.moleID = _moleID
        this.blushID = _blushID
        this.updateHeadOverlay()
    }

    // Update eye color
    private updateEyeColor(_eyeColor) {
        this.eyeColor = _eyeColor
        this.updateHeadOverlay()
    }

    // Update hairs
    private updateHairs(_hairID, _hairColor, _hairHighlight, _eyebrowID, _eyebrowColor, _eyebrowHighlight) {
        this.hairID = _hairID
        this.hairColor = _hairColor
        this.hairHighlight = _hairHighlight
        this.eyebrowID = _eyebrowID
        this.eyebrowColor = _eyebrowColor
        this.updateHeadOverlay()
    }

    // Update ageing
    private updateAgeing(_age) {
        this.age = _age
        this.updateHeadOverlay()
    }

    // Update beard
    private updateBeard(_beard, _beardColor) {
        this.beard = _beard
        this.beardColor = _beardColor
        this.updateHeadOverlay()
    }

    // Finished
    private finished(_name, _surname) {
        this.name = _name
        this.surname = _surname
    }

    private resetCamera(modelToUse) {
        this.testPed.destroy()

        this.testPed = new Ped(modelToUse, this.playerPoint)

        native.setPedComponentVariation(this.testPed.scriptID, 6, 1, 0, 0)
        native.setPedComponentVariation(this.testPed.scriptID, 6, 1, 0, 0)

        native.addPedDecorationFromHashes(
            this.testPed.scriptID,
            native.getHashKey("mpbeach_overlays"),
            native.getHashKey("fm_hair_fuzz")
        )

        native.setPedHeadBlendData(this.testPed.scriptID, 
            0, 21, 0, 0, 21,
            0, 0, 0, 0, false
        )

        native.setPedHeadOverlay(this.testPed.scriptID, 2, 0, 255)
        native.setPedHeadOverlayColor(this.testPed.scriptID, 2, 1, 1, 1)

        this.camera.pointAtBone(this.testPed.scriptID, 31086, 0.05, 0, 0)
        this.camera.playerControlsEntity(this.testPed.scriptID, true)
    }
}
