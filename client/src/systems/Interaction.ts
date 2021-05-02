import alt, {Entity, Player} from 'alt-client';
import * as native from 'natives';
import {distance} from "../utils/Vector";
import UiManager from "../ui/UiManager";
import {KEYS} from "../events/keyup";

alt.log('Loaded: client->utility->vehicle.mjs');

export interface InteractionInstance {
  id: string
  title: string;
  text: string;
  callback: string;
  key: number;
}

const keys: number[] = [KEYS.E, 74, 75]

export class Interaction {
  private static currInteraction: InteractionInstance[] = [];

  static hideInteraction(id) {
    UiManager.removeToast(id)
    Interaction.currInteraction = Interaction.currInteraction.filter(value => value.id !== id);
  }

  static accessInteraction(key) {
    if (!Interaction.currInteraction) return;

    const interactionForKey = Interaction.currInteraction.find(value => value.key == key);
    if (!interactionForKey) return;

    alt.emitServer(interactionForKey.callback);
  }

  static showInteraction(id, title, text, callback) {
    let interactKey: number;
    let idx = 0;
    do {
      interactKey = keys[idx] ?? 69;
      idx++;
      if(idx > keys.length - 1 ) {
        idx = 0;
      }
    } while (Interaction.currInteraction.find(value => value.key == interactKey) !== undefined)
    Interaction.currInteraction = Interaction.currInteraction.filter(value => value.id !== id);
    Interaction.currInteraction.push({
      id: id,
      title: title,
      text: text,
      callback: callback,
      key: interactKey //E
    });
    UiManager.insertToast(id, title, "Drücke " + String.fromCharCode(interactKey) + " um zu Interagieren")
  }
}

alt.onServer("Interaction:Show", Interaction.showInteraction)
alt.onServer("Interaction:Hide", Interaction.hideInteraction)
alt.on("keyup", Interaction.accessInteraction)
