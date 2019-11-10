import * as alt from "alt";
import * as native from 'natives';
import { Singleton } from "../utils/Singleton";
import { View } from "../utils/View";
import { Ped } from "../systems/Ped";
import { Camera } from "../utils/Camera";

export class FaceFeaturesUi {
    private url = 'http://resource/client/cef/faceFeatures/index.html';
    private webview: View;
    private ped: Ped;
    private camera: Camera;
    private cameraPoint: Vector3 = {
        x: -140.7032928466797,
        y: -644.9724975585938,
        z: 169.413232421875
    };
    private offsetPoint: Vector3 = {
        x: -130.7032928466797,
        y: -644.9724975585938,
        z: 169.413232421875
    };
    
    private playerPoint: Vector3 = {
        x: -140.45274353027344,
        y: -646.4044189453125,
        z: 168.813232421875
    };
    private lastHair= 0;
    
    constructor() {
        if (!this.webview) {
            this.webview = new View();
        }
        
        this.webview.open(this.url);
        this.webview.on('updateSex', this.updateSex);
        this.webview.on('updatePlayerFace', this.updatePlayerFace);
        this.webview.on('updateFaceDecor', this.updateFaceDecor);
        this.webview.on('updateFaceFeature', this.updateFaceFeature);
        this.webview.on('updateHair', this.updateHair);
        this.webview.on('updateEyes', this.updateEyes);
        this.webview.on('setPlayerFacialData', this.setPlayerFacialData);
        
            // Setup a temporary teleport.
        alt.emitServer('temporaryTeleport', this.offsetPoint);

        // Request these models if they're not already loaded.
        native.requestModel(native.getHashKey('mp_m_freemode_01'));
        native.requestModel(native.getHashKey('mp_f_freemode_01'));

        // Create a pedestrian to customize.
        this.ped = new Ped('mp_f_freemode_01', this.playerPoint);
        alt.log(this.ped.scriptID);

        native.setPedComponentVariation(this.ped.scriptID, 6, 1, 0, 0);

        // Set the head blend data to 0 to prevent texture issues.
        native.setPedHeadBlendData(this.ped.scriptID, 0, 0, 0, 0, 0, 0, 0, 0, 0, false);

        // Hide the player's model.
        native.setEntityAlpha(alt.Player.local.scriptID, 0, 0);

        // Setup the ped camera point.
        this.camera = new Camera(this.cameraPoint, 28);
        this.camera.pointAtBone(this.ped.scriptID, 31086, 0.05, 0, 0);
        this.camera.playerControlsEntity(this.ped.scriptID, true);

        // Update Hair Color Choices for Buttons
        this.updateHairColorChoices();

        native.addPedDecorationFromHashes(
            this.ped.scriptID,
            native.getHashKey('mpbeach_overlays'),
            native.getHashKey('fm_hair_fuzz')
        );

        alt.setTimeout(() => {
            this.webview.emit('sexUpdated', 0);
        }, 1000);
    }
    
    public clearPedBloodDamage() {
        native.clearPedBloodDamage(alt.Player.local.scriptID);
    }
    
    // Player Sex Updates, for model changes.
    private updateSex(value) {
        if (value === 0) {
            this.resetCamera('mp_f_freemode_01');
            this.webview.emit('sexUpdated', 0);
        } else {
            this.resetCamera('mp_m_freemode_01');
            this.webview.emit('sexUpdated', 1);
        }
    }
    
    // Player Face Updates; for head blend and such.
    private updatePlayerFace(valuesAsJSON) {
        const values = JSON.parse(valuesAsJSON);
        native.setPedHeadBlendData(
            this.ped.scriptID,
            values[0],
            values[2],
            0,
            values[1],
            values[3],
            0,
            values[4],
            values[5],
            0,
            false
        );
    }
    
    // Player Face Decor, SunDamage, Makeup, Lipstick, etc.
    private updateFaceDecor(id, colorType, dataAsJSON) {
        let results = JSON.parse(dataAsJSON);
        native.setPedHeadOverlay(this.ped.scriptID, id, results[0], results[1]);
    
        // Only if one color is present.
        if (results.length > 2 && results.length <= 3) {
            native.setPedHeadOverlayColor(
                this.ped.scriptID,
                id,
                colorType,
                results[2],
                results[2]
            );
        }
    
        // If two colors are present.
        if (results.length > 3) {
            native.setPedHeadOverlayColor(
                this.ped.scriptID,
                id,
                colorType,
                results[2],
                results[3]
            );
        }
    }
    
    private updateFaceFeature(id, value) {
        native.setPedFaceFeature(this.ped.scriptID, id, value);
    }
    
    // Set the hair style, color, texture, etc. from the webview.
    // 'Hair', HairColor', 'HairHighlights', 'HairTexture'
    private updateHair(dataAsJSON, overlayData) {
        let results = JSON.parse(dataAsJSON);
        if (this.lastHair !== results[0]) {
            this.lastHair = results[0];
    
            let hairTextureVariations = native.getNumberOfPedTextureVariations(
                this.ped.scriptID,
                2,
                results[0]
            );
            this.webview.emit('setHairTextureVariations', hairTextureVariations);
        }
    
        native.clearPedDecorations(this.ped.scriptID);
        if (overlayData) {
            native.addPedDecorationFromHashes(
                this.ped.scriptID,
                native.getHashKey(overlayData.collection),
                native.getHashKey(overlayData.overlay)
            );
        }
    
        native.setPedComponentVariation(this.ped.scriptID, 2, results[0], results[3], 2);
        native.setPedHairColor(this.ped.scriptID, results[1], results[2]);
        this.updateHairColorChoices();
    }
    
    // Set the eye color from the webview.
    private updateEyes(value) {
        native.setPedEyeColor(this.ped.scriptID, value);
    }
    
    private resetCamera(modelToUse) {
        // Delete the new ped.
        this.ped.destroy();
    
        // Re-Create the Ped
        this.ped = new Ped(modelToUse, this.playerPoint);
    
        // Lower User
        native.setPedComponentVariation(this.ped.scriptID, 6, 1, 0, 0);
    
        // Set Hair Fuzz
        native.addPedDecorationFromHashes(
            this.ped.scriptID,
            native.getHashKey('mpbeach_overlays'),
            native.getHashKey('fm_hair_fuzz')
        );
    
        // Set the head blend data to 0 to prevent weird hair texture glitches. Thanks Matspyder
        native.setPedHeadBlendData(this.ped.scriptID, 0, 0, 0, 0, 0, 0, 0, 0, 0, false);
    
        this.updateHairColorChoices();
    
        this.camera.pointAtBone(this.ped.scriptID, 31086, 0.05, 0, 0);
        this.camera.playerControlsEntity(this.ped.scriptID, true);
    }
    
    // Update the number of hair colors available.
    private updateHairColorChoices() {
        // Fetch number of styles from natives.
        let hairStyles = native.getNumberOfPedDrawableVariations(this.ped.scriptID, 2);
        let hairColors = native.getNumHairColors();
    
        // Emit the webview to update the style choices.
        this.webview.emit('stylesUpdate', hairStyles, hairColors);
    }
    
    private setPlayerFacialData(facialDataJSON) {
        alt.emitServer('face:SetFacialData', facialDataJSON);
    
        // Remove the CharacterCamera
        this.camera.destroy();
        this.webview.close();
        this.ped.destroy();
    
        // Hide the player's model.
        native.setEntityAlpha(alt.Player.local.scriptID, 255, 0);
    
        // Request the last location.
        alt.emitServer('utility:GoToLastLocation');
    }
    
    public cleanupSpawnedPed() {
        if (this.ped !== undefined) this.ped.destroy();
    }
    
};

