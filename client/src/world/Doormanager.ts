import { Singleton } from "../utils/Singleton";
import * as alt from 'alt-client';
import * as native from 'natives';

@Singleton
export class DoorManager {
  constructor() {
    this.loadEvents()
  }

  private loadEvents() {
    alt.onServer("DoorManager:setDoorStates", (doors: { hash: number, state: boolean }[]) => {
      alt.log(doors)
      doors.forEach(door => {
        native.doorSystemSetDoorState(door.hash, door.state ? 1 : 0, false, true)
      })
    })
  }
}

export default DoorManager;
