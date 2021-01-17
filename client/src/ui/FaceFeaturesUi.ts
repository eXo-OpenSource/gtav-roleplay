import alt from "alt-client"
import * as native from "natives"
import UiManager from "./UiManager"
import { Ped } from "../systems/Ped"
import { Camera } from "../utils/Camera"
import { Player, Vector3 } from 'alt-client';
import { loadCutscene } from "../utils/Cutscene";

export class FaceFeaturesUi {
  private testPed: Ped
  private camera: Camera
  private cameraPoint = new Vector3(
    -813.67474,
    175.17363,
    76.72888
  )

  private playerPoint = new Vector3(
    -811.67474,
    175.17363,
    76.72888
  )

  private model = "mp_m_freemode_01"
  private gender = 0
  private name = ""
  private surname = ""

  private prevData = {sex: 1}

  public constructor() {
    UiManager.navigate("/charactercreator", true)

    // Events
    UiManager.on("FaceFeatures:Update", this.update.bind(this))
    UiManager.on("FaceFeatures:Finished", this.finished.bind(this))

    native.requestModel(native.getHashKey("mp_m_freemode_01"))
    native.requestModel(native.getHashKey("mp_f_freemode_01"))

    if (!this.testPed) {
      this.testPed = new Ped(this.model, this.playerPoint)
    }

    native.setFocusPosAndVel(this.playerPoint.x, this.playerPoint.y,
      this.playerPoint.z, this.playerPoint.x, this.playerPoint.y, this.playerPoint.z)

    this.resetCamera(this.model)
  }

  private resetCamera(modelToUse) {
    this.testPed.destroy()
    if (this.camera) this.camera.destroy()

    this.testPed = new Ped(modelToUse, this.playerPoint)
    alt.setTimeout(() => {native.freezeEntityPosition(this.testPed.scriptID, true)}, 1000)

    this.camera = new Camera(this.cameraPoint, 38)
    this.camera.pointAtBone(this.testPed.scriptID, 31086, 0.05, 0, 0)
    this.camera.playerControlsEntity(this.testPed.scriptID, true)
  }

  async update(data) {
    native.clearPedBloodDamage(this.testPed.scriptID);
    native.clearPedDecorations(this.testPed.scriptID);
    native.setPedHeadBlendData(this.testPed.scriptID, 0, 0, 0, 0, 0, 0, 0, 0, 0, false);

    this.model = data.sex === 0 ? "mp_f_freemode_01" : "mp_m_freemode_01"
    if (data.sex != this.prevData.sex) this.resetCamera(this.model)

    native.setPedHeadBlendData(this.testPed.scriptID, 0, 0, 0, 0, 0, 0, 0, 0, 0, false);
    native.setPedHeadBlendData(
        this.testPed.scriptID,
        data.faceFather,
        data.faceMother,
        0,
        data.skinFather,
        data.skinMother,
        0,
        parseFloat(data.faceMix),
        parseFloat(data.skinMix),
        0,
        false
    );

    // Hair
    const collection = native.getHashKey(data.hairOverlay.collection);
    const overlay = native.getHashKey(data.hairOverlay.overlay);
    native.addPedDecorationFromHashes(this.testPed.scriptID, collection, overlay);
    native.setPedComponentVariation(this.testPed.scriptID, 2, data.hair, 0, 0);
    native.setPedHairColor(this.testPed.scriptID, data.hairColor1, data.hairColor2);

    // Facial Hair
    native.setPedHeadOverlay(this.testPed.scriptID, 1, data.facialHair, data.facialHairOpacity);
    native.setPedHeadOverlayColor(this.testPed.scriptID, 1, 1, data.facialHairColor1, data.facialHairColor1);

    // Eyebrows
    native.setPedHeadOverlay(this.testPed.scriptID, 2, data.eyebrows, 1);
    native.setPedHeadOverlayColor(this.testPed.scriptID, 2, 1, data.eyebrowsColor1, data.eyebrowsColor1);

    // Ageing
    native.setPedHeadOverlay(this.testPed.scriptID, 3, data.ageing, 1);

    // Eyes
    native.setPedEyeColor(this.testPed.scriptID, data.eyes);

    // Facefeatures:

    // Nose
    native.setPedFaceFeature(this.testPed.scriptID, 0, data.noseWidth) // nose width
    native.setPedFaceFeature(this.testPed.scriptID, 1, data.noseHeight) // nose height
    native.setPedFaceFeature(this.testPed.scriptID, 2, data.noseLength) // nose length
    native.setPedFaceFeature(this.testPed.scriptID, 3, data.noseBridge) // nose bridge
    native.setPedFaceFeature(this.testPed.scriptID, 4, data.noseTip) // nose tip
    native.setPedFaceFeature(this.testPed.scriptID, 5, data.noseBridgeShift) // nose bridge
    native.setPedFaceFeature(this.testPed.scriptID, 6, data.browHeight) // brow height
    native.setPedFaceFeature(this.testPed.scriptID, 7, data.browWidth) // brow width
    native.setPedFaceFeature(this.testPed.scriptID, 8, data.cheekboneHeight) // cheekbone height
    native.setPedFaceFeature(this.testPed.scriptID, 9, data.cheekboneWidth) // cheekbone width
    native.setPedFaceFeature(this.testPed.scriptID, 10, data.cheeksWidth) // cheeks width
    native.setPedFaceFeature(this.testPed.scriptID, 11, data.eyeWide) // eyes wide
    native.setPedFaceFeature(this.testPed.scriptID, 12, data.lipWide) // lips wide
    native.setPedFaceFeature(this.testPed.scriptID, 13, data.jawWidth) // jaw width
    native.setPedFaceFeature(this.testPed.scriptID, 14, data.jawHeight) // jaw height
    native.setPedFaceFeature(this.testPed.scriptID, 15, data.chinLength) // chin length
    native.setPedFaceFeature(this.testPed.scriptID, 16, data.chinPosition) // chin position
    native.setPedFaceFeature(this.testPed.scriptID, 17, data.chinWidth) // chin width
    native.setPedFaceFeature(this.testPed.scriptID, 18, data.chinShape) // chin shape
    native.setPedFaceFeature(this.testPed.scriptID, 19, data.neckWidth) // neck width

    if (data.sex === 0) {
        native.setPedComponentVariation(this.testPed.scriptID, 3, 15, 0, 0); // arms
        native.setPedComponentVariation(this.testPed.scriptID, 4, 14, 0, 0); // pants
        native.setPedComponentVariation(this.testPed.scriptID, 6, 35, 0, 0); // shoes
        native.setPedComponentVariation(this.testPed.scriptID, 8, 15, 0, 0); // shirt
        native.setPedComponentVariation(this.testPed.scriptID, 11, 15, 0, 0); // torso
    } else {
        native.setPedComponentVariation(this.testPed.scriptID, 3, 15, 0, 0); // arms
        native.setPedComponentVariation(this.testPed.scriptID, 4, 14, 0, 0); // pants
        native.setPedComponentVariation(this.testPed.scriptID, 6, 34, 0, 0); // shoes
        native.setPedComponentVariation(this.testPed.scriptID, 8, 15, 0, 0); // shirt
        native.setPedComponentVariation(this.testPed.scriptID, 11, 91, 0, 0); // torso
    }

    this.prevData = data;
}

  private applyData(_data) {
    const data = [
      this.name, this.surname,
      this.gender, _data.faceFather, _data.faceMother, parseFloat(_data.skinFather), parseFloat(_data.skinMother),
      _data.faceMix, _data.skinMix, _data.freckles || 0, _data.eyes, _data.hair, _data.hairColor1, _data.hairColor2,
      _data.eyebrows, _data.eyebrowsColor1, _data.ageing, _data.facialHair, _data.facialHairColor1,
      _data.noseWidth, _data.noseHeight, _data.noseLength, _data.noseBridge, _data.noseTip, _data.noseBridgeShift,
      _data.browHeight, _data.browWidth, _data.cheekboneHeight, _data.cheekboneWidth, _data.cheeksWidth, _data.eyeWide,
      _data.lipWide, _data.jawWidth, _data.jawHeight, _data.chinLength, _data.chinPosition, _data.chinWidth, _data.chinShape,
      _data.neckWidth, this.model
    ]
    alt.emitServer("FaceFeatures:ApplyData", JSON.stringify(data))
    this.testPed.destroy()
    this.camera.destroy()
  }


  // Finished
  private finished(name, surname, data) {
    this.name = name
    this.surname = surname

    this.applyData(data);

    native.prefetchSrl("GTAO_INTRO_MALE")
    native.setCutsceneEntityStreamingFlags(this.gender == 1 ? "MP_Female_Character" : "MP_Male_Character", 0, 1)
    loadCutscene("mp_intro_concat", 8).then(() => {
      native.registerEntityForCutscene(Player.local.scriptID, this.gender == 1 ? "MP_Female_Character" : "MP_Male_Character", 0, 0, 0)
      native.registerEntityForCutscene(0, this.gender == 0 ? "MP_Female_Character" : "MP_Male_Character", 3, native.getHashKey(this.gender == 1 ? "mp_f_freemode_01" : "mp_m_freemode_01"), 0)
      native.beginSrl()
      native.startCutscene(4)
      alt.setTimeout(() => UiManager.emit("FaceFeatures:FadeOut"), 5000)
      alt.setTimeout(() => UiManager.reset(), 11000)
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
}

alt.on("syncedMetaChange", (player: Player, key: string, value: any) => {
  if (key == "faceFeatures.Data") {
    var data = JSON.parse(value)

    native.clearPedBloodDamage(player.scriptID);
    native.clearPedDecorations(player.scriptID);
    native.setPedHeadBlendData(player.scriptID, 0, 0, 0, 0, 0, 0, 0, 0, 0, false);

    native.setPedHeadBlendData(
      player.scriptID,
      data[1], data[2], 0,
      data[4], data[5], 0, data[7],
      data[8], 0, false
    );

    native.setPedComponentVariation(player.scriptID, 2, data[11], 0, 0);
    native.setPedHairColor(player.scriptID, data[12], data[13]);

    // Facial Hair
    native.setPedHeadOverlay(player.scriptID, 1, data[17], 1);
    native.setPedHeadOverlayColor(player.scriptID, 1, 1, data[18], data[18]);

    // Eyebrows
    native.setPedHeadOverlay(player.scriptID, 2, data[14], 1);
    native.setPedHeadOverlayColor(player.scriptID, 2, 1, data[15], data[15]);

    // Ageing
    native.setPedHeadOverlay(player.scriptID, 3, data.ageing, 1);

    // Eyes
    native.setPedEyeColor(player.scriptID, data[10]);

    // Facefeatures
    native.setPedFaceFeature(player.scriptID, 0, data[19]) // nose width
    native.setPedFaceFeature(player.scriptID, 1, data[20]) // nose height
    native.setPedFaceFeature(player.scriptID, 2, data[21]) // nose length
    native.setPedFaceFeature(player.scriptID, 3, data[22]) // nose bridge
    native.setPedFaceFeature(player.scriptID, 4, data[23]) // nose tip
    native.setPedFaceFeature(player.scriptID, 5, data[24]) // nose bridge
    native.setPedFaceFeature(player.scriptID, 6, data[25]) // brow height
    native.setPedFaceFeature(player.scriptID, 7, data[26]) // brow width
    native.setPedFaceFeature(player.scriptID, 8, data[27]) // cheekbone height
    native.setPedFaceFeature(player.scriptID, 9, data[28]) // cheekbone width
    native.setPedFaceFeature(player.scriptID, 10, data[29]) // cheeks width
    native.setPedFaceFeature(player.scriptID, 11, data[30]) // eyes wide
    native.setPedFaceFeature(player.scriptID, 12, data[31]) // lips wide
    native.setPedFaceFeature(player.scriptID, 13, data[32]) // jaw width
    native.setPedFaceFeature(player.scriptID, 14, data[33]) // jaw height
    native.setPedFaceFeature(player.scriptID, 15, data[34]) // chin length
    native.setPedFaceFeature(player.scriptID, 16, data[35]) // chin position
    native.setPedFaceFeature(player.scriptID, 17, data[36]) // chin width
    native.setPedFaceFeature(player.scriptID, 18, data[37]) // chin shape
    native.setPedFaceFeature(player.scriptID, 19, data[38]) // neck width
  }
})
