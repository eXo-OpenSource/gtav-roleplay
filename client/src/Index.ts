import * as alt from "alt-client";

import { Singleton } from "./utils/Singleton";
import './ui/UiManager';
// import { log } from "util";
//extensions
import "./extensions/Blip"

// systems
import "./systems/Vehicle";
import "./systems/Notification";
import "./systems/Interaction";
import "./systems/Streamer";
import "./systems/Animations"
import "./jobs/WasteCollector";
import "./jobs/LawnMower";
import "./jobs/PizzaDelivery";
import "./world/IPLManager";
import "./world/Doormanager";

//events
import './events/keyup'
import './events/ui'

//ui
import "./ui/Chat"
import "./ui/VehicleUI";
import "./ui/ATM"
import "./jobs/Farmer"
import "./jobs/WoodCutter"
import "./ui/Speedo";
import "./ui/Popup";
import "./ui/Hud";
import "./environment/CarRent";
import "./environment/Cityhall";
import "./environment/Drivingschool";
import "./ui/RegisterLogin";

import "./utils/DevCommands";


