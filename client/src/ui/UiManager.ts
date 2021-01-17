import * as alt from 'alt-client';
import { View } from "../utils/View";
import Chat from "./Chat";
import { Cursor } from "../utils/Cursor";
import { HUD } from "./HUD";
import { VehicleUI } from "./VehicleUI";
import { ATM } from "./ATM"
import { Farmer } from "../jobs/Farmer"
import { WoodCutter } from "../jobs/WoodCutter"
import Speedo from "./Speedo";
import Popup from "./Popup";
import { CarRent } from "../environment/CarRent"

const url = 'http://resource/cef/index.html#';

export default class UiManager {
  private static mainView: View = new View();

  static createUi() {
    //this.mainView = new View()
    this.mainView.open(url, false, true);

    this.loadEvents()
  }

  static loadEvents() {
    alt.log('Loaded: UI Manager Events');


    this.mainView.on("ready", () => alt.emitServer("ready"))

    alt.onServer("Toast:AddTimed", (name, text, time) => {
      this.insertTimedToast(name, text, time)
    })

    alt.onServer("Progress:Set", (val) => {
      this.emit("Progress:Set", val)
    })

    alt.onServer("Progress:Text", (text) => {
      this.emit("Progress:Text", text)
    })

    alt.onServer("Progress:Active", (toggle) => {
      this.emit("Progress:Active", toggle)
    })
  }

  static reset() {
    this.navigate("/", false)
    Cursor.show(false)
  }

  static emit(name, ...args) {
    this.mainView.emit(name, ...args);
  }

  static on(name, func) {
    this.mainView.on(name, func);
  }

  static writeChat(text: string) {
    Chat.pushLine(text);
  }

  static insertToast(id, title, text) {
    this.emit("Toast:Add", {
      id: id,
      title: title,
      text: text
    })
  }

  static insertTimedToast(title, text, time: number) {
    const id = (Math.random()*1000)
    this.insertToast(id, title, text)

    alt.setTimeout(() => {
      this.removeToast(id)
    }, time)
  }

  static removeToast(id) {
    this.emit("Toast:Remove", id)
  }

  static navigate(subUrl, cursor) {
    this.mainView.emit("locationChange", subUrl);
    if(cursor) {
      Cursor.show(true);
    }
  }

}
