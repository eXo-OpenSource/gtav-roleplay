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
    

    public constructor(uiManager) {
        this.uiManager = uiManager
        this.uiManager.navigate("/charactercreator", true)

        // Events
        this.uiManager.on("FaceFeatures:UpdateSex", this.updateSex.bind(this))
        this.uiManager.on("FaceFeatures:UpdateParent", this.updateParent.bind(this))

        native.requestModel(native.getHashKey("mp_m_freemode_01"))
        native.requestModel(native.getHashKey("mp_f_freemode_01"))

        // Teleport for customization
        if (!this.testPed) {
            this.testPed = new Ped("mp_m_freemode_01", this.playerPoint)
        }
        
        alt.emitServer("temporaryTeleport", this.teleportPosition)
        
        this.camera = new Camera(this.cameraPoint, 28)
        this.camera.pointAtBone(this.testPed.scriptID, 31086, 0.05, 0, 0)
        this.camera.playerControlsEntity(this.testPed.scriptID, true)
    }

    // Update sex
    private updateSex(value) {
        if (value == 0) {
            this.resetCamera("mp_f_freemode_01")
        } else {
            this.resetCamera("mp_m_freemode_01")
        } 
    }

    // Update parent
    private updateParent(fatherID, motherID, skin, look) {
        native.setPedHeadBlendData(this.testPed.scriptID, fatherID, motherID, 0, fatherID, motherID, 0, skin, look, 0, false)
    }

    private resetCamera(modelToUse) {
        this.testPed.destroy()

        this.testPed = new Ped(modelToUse, this.playerPoint)

        native.setPedComponentVariation(this.testPed.scriptID, 6, 1, 0, 0)

        native.addPedDecorationFromHashes(
            this.testPed.scriptID,
            native.getHashKey("mpbeach_overlays"),
            native.getHashKey("fm_hair_fuzz")
        )

        // Set the head blend data to 0 to prevent weird hair texture glitches. Thanks Matspyder
        native.setPedHeadBlendData(this.testPed.scriptID, 0, 0, 0, 0, 0, 0, 0, 0, 0, false)

        this.camera.pointAtBone(this.testPed.scriptID, 31086, 0.05, 0, 0)
        this.camera.playerControlsEntity(this.testPed.scriptID, true)
    }
}
