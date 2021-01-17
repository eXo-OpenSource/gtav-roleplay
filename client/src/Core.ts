import * as alt from "alt-client";
import { Singleton } from "./utils/Singleton";
import UiManager from './ui/UiManager';
// import { log } from "util";
import "./systems/Vehicle";
import "./systems/Notification";
import Interaction from "./systems/Interaction";
import "./systems/Streamer";
import WasteCollector from "./jobs/WasteCollector";
import LawnMower from "./jobs/LawnMower";
import PizzaDelivery from "./jobs/PizzaDelivery";
import IPLManager from "./world/IPLManager";
import DoorManager from "./world/Doormanager";
//extensions
import "./extensions/Blip"

//events
import './events/keyup'
import './events/ui'

//ui
import "./HUD";
import "./VehicleUI";
import "./ATM"
import "../jobs/Farmer"
import "../jobs/WoodCutter"
import "./Speedo";
import "./Popup";
import "../environment/CarRent"


import "./utils/DevCommands";

@Singleton
export class Core {
  private interaction = new Interaction();
  private waste = new WasteCollector();
  private lawn = new LawnMower();
  private pizza = new PizzaDelivery();
  private iplManager = new IPLManager()
  private doorManager = new DoorManager();

  constructor() {
    alt.log('Loaded: client.mjs');

    UiManager.createUi()

    alt.on('consoleCommand', () => {
      alt.log('consoleCommand');
      alt.emitServer("ClientConnectionComplete", "Test")
    });
  }
}
