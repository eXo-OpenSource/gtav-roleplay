import * as alt from "alt-client"
import * as native from "natives"
import { UiManager } from "./UiManager"
import { Ped } from "../systems/Ped"
import { Camera } from "../utils/Camera"
import { Vector3 } from "natives"
import {Player} from 'alt-client';
import {loadCutscene} from "../systems/Cutscene";

export class FaceFeaturesUi {
  private uiManager: UiManager
  private testPed: Ped
  private camera: Camera
  private cameraPoint: Vector3 = {
    x: -813.67474,
    y: 175.17363,
    z: 76.72888
  }

  private playerPoint: Vector3 = {
    x: -811.67474,
    y: 175.17363,
    z: 76.72888
  }

  private model = "mp_m_freemode_01"
  private gender = 1
  private fatherID = 0
  private motherID = 21
  private skin = 50
  private look = 50
  private moleID = 0
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
    this.uiManager.on("FaceFeatures:UpdateHairColor", this.updateHairColor.bind(this))
    this.uiManager.on("FaceFeatures:UpdateHairHighlights", this.updateHairColor.bind(this))
    this.uiManager.on("FaceFeatures:UpdateEyebrows", this.updateEyebrows.bind(this))
    this.uiManager.on("FaceFeatures:UpdateEyebrowColor", this.updateEyebrowColor.bind(this))
    this.uiManager.on("FaceFeatures:UpdateEyes", this.updateEyeColor.bind(this))
    this.uiManager.on("FaceFeatures:UpdateBeard", this.updateBeard.bind(this))
    this.uiManager.on("FaceFeatures:UpdateAgeing", this.updateAgeing.bind(this))
    this.uiManager.on("FaceFeatures:UpdateMoles", this.updateMoles.bind(this))
    this.uiManager.on("FaceFeatures:Finished", this.finished.bind(this))

    native.requestModel(native.getHashKey("mp_m_freemode_01"))
    native.requestModel(native.getHashKey("mp_f_freemode_01"))

    if (!this.testPed) {
      this.testPed = new Ped(this.model, this.playerPoint)
    }

    native.setFocusPosAndVel(this.playerPoint.x, this.playerPoint.y,
      this.playerPoint.z, this.playerPoint.x, this.playerPoint.y, this.playerPoint.z)

    this.resetCamera(this.model)
    this.freezePed()
  }

  private freezePed() {
    //alt.setTimeout(() => native.freezeEntityPosition(this.testPed.scriptID, true), 2500)
  }

  private applyFaceFeatures(ped) {
    native.setPedHeadBlendData(ped,
      this.fatherID, this.motherID, 0,
      this.fatherID, this.motherID, 0, this.look,
      this.skin, 0, false
    )
    native.setPedHeadOverlay(ped, 9, this.moleID, 255)
    native.setPedEyeColor(ped, this.eyeColor)
    native.setPedComponentVariation(ped, 2, this.hairID, 0, 0)
    native.setPedHairColor(ped, this.hairColor, this.hairHighlight)
    native.setPedHeadOverlay(ped, 2, this.eyebrowID, 255)
    native.setPedHeadOverlayColor(ped, 2, 1, this.eyebrowColor, this.eyebrowColor)
    native.setPedHeadOverlay(ped, 3, this.age, 255)
    if (this.gender == 1) {
      native.setPedHeadOverlay(ped, 1, this.beard, 255)
      native.setPedHeadOverlayColor(ped, 1, 1, this.beardColor, this.beardColor)
    }
  }

  // Update sex
  private updateSex(gender) {
    this.model = gender == 0 ? "mp_f_freemode_01" : "mp_m_freemode_01"
    this.gender = gender
    this.resetCamera(this.model)
    native.setPedHeadBlendData(this.testPed.scriptID,
      this.fatherID, this.motherID, 0,
      this.fatherID, this.motherID, 0, this.look,
      this.skin, 0, false
    )
  }

  // Update parent
  private updateParent(fatherID = this.fatherID, motherID = this.motherID, skin = this.skin, look = this.look) {
    this.fatherID = fatherID
    this.motherID = motherID
    this.skin = skin
    this.look = look
    this.preventFaceBugs()
    native.setPedHeadBlendData(this.testPed.scriptID, this.fatherID, this.motherID, 0,
      this.fatherID, this.motherID, 0, this.look, this.skin, 0, false
    )
  }

  private updateEyebrows(eyebrows = this.eyebrowID) {
    this.eyebrowID = eyebrows - 1
    native.setPedHeadOverlay(this.testPed.scriptID, 2, this.eyebrowID, 255)
  }

  private updateEyebrowColor(color = this.eyebrowColor) {
    this.eyebrowColor = color - 1
    native.setPedHeadOverlayColor(this.testPed.scriptID, 2, 1, this.eyebrowColor, this.eyebrowColor)
  }

  private updateHairColor(hairColor = this.hairColor, hairHighlight = this.hairHighlight) {
    this.hairColor = hairColor - 1
    this.hairHighlight = hairHighlight - 1
    native.setPedHairColor(this.testPed.scriptID, this.hairColor, this.hairHighlight)
  }

  private updateMoles(moleID = this.moleID) {
    this.moleID = moleID - 1
    native.setPedHeadOverlay(this.testPed.scriptID, 9, this.moleID, 255)
  }

  private updateEyeColor(eyeColor = this.eyeColor) {
    this.eyeColor = eyeColor - 1
    native.setPedEyeColor(this.testPed.scriptID, this.eyeColor)
  }

  private updateHairs(hairs = this.hairID) {
    this.hairID = hairs - 1
    native.setPedComponentVariation(this.testPed.scriptID, 2, this.hairID, 0, 0)
  }

  private updateAgeing(age = this.age) {
    this.age = age - 1
    native.setPedHeadOverlay(this.testPed.scriptID, 3, this.age, 255)
  }

  private updateBeard(beard = this.beard, beardColor = this.beardColor) {
    this.beard = beard - 1
    this.beardColor = beardColor - 1
    if (this.gender == 1) {
      native.setPedHeadOverlay(this.testPed.scriptID, 1, this.beard, 255)
      native.setPedHeadOverlayColor(this.testPed.scriptID, 1, 1, this.beardColor, this.beardColor)
    }
  }

  private applyData() {
    const data = [
      this.name, this.surname,
      this.gender, this.fatherID, this.motherID, this.skin.toFixed(1), this.look.toFixed(1),
      this.moleID, this.eyeColor, this.hairID, this.hairColor, this.hairHighlight,
      this.eyebrowID, this.eyebrowColor, this.age, this.beard, this.beardColor
    ]
    this.applyFaceFeatures(alt.Player.local.scriptID)
    alt.emitServer("FaceFeatures:ApplyData", JSON.stringify(data))
    this.testPed.destroy()
    this.camera.destroy()
  }


  // Finished
  private finished(_name, _surname) {
    this.name = _name
    this.surname = _surname

    this.applyData()

    native.prefetchSrl("GTAO_INTRO_MALE")
    loadCutscene("mp_intro_concat", 8).then(() => {
      native.registerEntityForCutscene(Player.local.scriptID, this.gender == 1 ? "MP_Female_Character" : "MP_Male_Character", 0, 0, 0)
      native.registerEntityForCutscene(0, this.gender == 0 ? "MP_Female_Character" : "MP_Male_Character", 3, native.getHashKey(this.gender == 1 ? "mp_f_freemode_01" : "mp_m_freemode_01"), 0)
      native.beginSrl()
      native.startCutscene(4)
      alt.setTimeout(() => this.uiManager.emit("FaceFeatures:FadeOut"), 5000)
      alt.setTimeout(() => this.uiManager.reset(), 11000)
      alt.setTimeout(() => native.doScreenFadeOut(1000), 30000)
      alt.setTimeout(() => {
        native.stopCutsceneImmediately()
        native.playSoundFrontend(-1, 'CONTINUE', 'HUD_FRONTEND_DEFAULT_SOUNDSET', true)
        alt.emitServer("Player:SetPosition", -1038.7649, -2739.2966, 13.828613, 0, 0, 45)
        alt.emitServer("Ui:ShowUi", false)
        native.endSrl()
        native.clearFocus()
        native.doScreenFadeIn(1000)
      }, 33000)
    })
  }

  private preventFaceBugs() {
    native.setPedHeadBlendData(this.testPed.scriptID, 0, 0, 0, 0, 0, 0, 0, 0, 0, false);
  }

  private resetCamera(modelToUse) {
    this.testPed.destroy()

    this.testPed = new Ped(modelToUse, this.playerPoint)

    this.camera = new Camera(this.cameraPoint, 38)
    this.camera.pointAtBone(this.testPed.scriptID, 31086, 0.05, 0, 0)
    this.camera.playerControlsEntity(this.testPed.scriptID, true)

    this.preventFaceBugs()
  }
}

alt.on("syncedMetaChange", (player: Player, key: string, value: any) => {
  if (key == "faceFeatures.Data") {
    var data = JSON.parse(value)

    native.setPedHeadBlendData(player.scriptID,
      data[1], data[2], 0,
      data[4], data[5], 0, data[7],
      data[8], 0, false
    )
    native.setPedHeadOverlay(player.scriptID, 9, data[9], 255)
    native.setPedEyeColor(player.scriptID, data[10])
    native.setPedComponentVariation(player.scriptID, 2, data[11], 0, 0)
    native.setPedHairColor(player.scriptID, data[12], data[13])
    native.setPedHeadOverlay(player.scriptID, 2, data[14], 255)
    native.setPedHeadOverlayColor(player.scriptID, 2, 1, data[15], data[15])
    native.setPedHeadOverlay(player.scriptID, 3, data[16], 255)
    native.setPedHeadOverlay(player.scriptID, 1, data[17], 255)
    native.setPedHeadOverlayColor(player.scriptID, 1, 1, data[18], data[18])
  }
})
