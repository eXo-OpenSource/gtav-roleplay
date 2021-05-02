import * as alt from 'alt-client';
import * as native from 'natives';

alt.onServer("DoorManager:setDoorStates", (doors: { hash: number, state: boolean }[]) => {
  alt.log(doors)
  doors.forEach(door => {
    native.doorSystemSetDoorState(door.hash, door.state ? 1 : 0, false, true)
  })
})
