import alt, { Vector3 } from 'alt-client';
import {Cursor} from "../utils/Cursor";

export class Phone {

  static readonly view : alt.WebView = new alt.WebView("http://resource/phone/index.html#");
  static opened: boolean = false;

  constructor() {
    Phone.view.on("Phone:UiReady", () => {})
    Phone.view.on("Phone:ClosePhone", Phone.closePhone.bind(this))
  }

  static openPhone() {
    if(Phone.opened) return;
    Cursor.show(true);
    alt.toggleGameControls(false)
    Phone.view.emit("Phone:SetVisibility", true)
    Phone.view.focus()
    Phone.opened = true;
  }

  static closePhone() {
    if(!Phone.opened) return;
    Cursor.show(false);
    alt.toggleGameControls(true)
    Phone.view.emit("Phone:SetVisibility", false)
    Phone.view.unfocus()
    Phone.opened = false;
  }
}
