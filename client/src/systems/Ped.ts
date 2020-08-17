import alt, { Vector3 } from 'alt-client';
import * as native from 'natives';
import { distance } from '../utils/Vector';
import { getEntityFromRaycast } from '../utils/Raycast';

alt.log('Loaded: client->utility->ped.mjs');

let peds = [];

export class Ped {
  public scriptID: any;
  public serverID: any;

  public fleeFromPlayer: any;

  constructor(model: string, pos: Vector3, serverID: number = undefined, fleeFromPlayer: boolean = false) {
    native.requestModel(native.getHashKey(model));
    this.scriptID = native.createPed(
      1,
      native.getHashKey(model),
      pos.x,
      pos.y,
      pos.z,
      0,
      false,
      false
    );
    native.taskSetBlockingOfNonTemporaryEvents(this.scriptID, true);
    native.setBlockingOfNonTemporaryEvents(this.scriptID, true);
    native.setPedFleeAttributes(this.scriptID, 0, false);
    native.setPedCombatAttributes(this.scriptID, 17, true);

    if (serverID !== undefined && serverID !== null) {
      this.serverID = serverID;
      native.setPedAsEnemy(this.scriptID, true);
      native.setEntityInvincible(this.scriptID, true);
    }

    this.fleeFromPlayer = fleeFromPlayer;
    if (this.fleeFromPlayer) {
      peds.push(this);
    }
  }

  destroy() {
    native.deleteEntity(this.scriptID);
  }

  update() {
    if (!this.fleeFromPlayer) return;

    let clearLOS;
    const local = alt.Player.local;

    if (native.isAimCamActive()) {
      const [hit, aimedEntity] = native.getEntityPlayerIsFreeAimingAt(local.scriptID,
        local.scriptID
      );
      alt.log(`Aiming At: ${aimedEntity}`);
      if (hit && aimedEntity === this.scriptID) {
        const forwardVector = native.getEntityForwardVector(local.scriptID);
        alt.emitServer('ped:FleeFromPlayer', this.serverID, forwardVector);
        return;
      }
    }

    const pedPos = native.getEntityCoords(this.scriptID, false);
    const dist = distance(local.pos, pedPos);

    if (dist > 5) return;
    if (!native.isEntityOnScreen(this.scriptID)) return;

    clearLOS = native.hasEntityClearLosToEntityInFront(local.scriptID, this.scriptID);
    if (!clearLOS) return;

    const entity = getEntityFromRaycast(4, true);
    if (!entity) return;
    if (entity === this.scriptID) {
      const forwardVector = native.getEntityForwardVector(local.scriptID);
      alt.emitServer('ped:FleeFromPlayer', this.serverID, forwardVector);
    }
  }
}

alt.onServer('ped:DeleteByID', id => {
  const index = peds.find(ped => ped && ped.serverID && ped.serverID === id);
  if (index <= -1) return;
  peds.splice(index, 1);
});

alt.onServer('ped:CreatePedByID', (model, pos, id, fleeFromPlayer) => {
  if (peds.includes(ped => ped.serverID && ped.serverID === id)) {
    const index = peds.find(ped => ped && ped.serverID && ped.serverID === id);
    peds[index].destroy();
    peds.splice(index, 1);
  }
  new Ped(model, pos, id, fleeFromPlayer);
});

alt.onServer('ped:MoveTo', (serverID, pos) => {
  const index = peds.findIndex(ped => ped && ped.serverID && ped.serverID === serverID);
  if (index <= -1) return;
  alt.log('Should move a ped at this point...');
});

alt.on('peds:Delete', () => {
  peds.forEach(ped => {
    ped.destroy();
  });
});

alt.setInterval(() => {
  if (peds.length <= 0) return;
  peds.forEach(ped => {
    ped.update();
  });
}, 500);
