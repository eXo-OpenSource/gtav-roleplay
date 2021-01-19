import alt, {BaseObject, PointBlip, Vector3} from 'alt-client';
import Blip from "../extensions/Blip";
import * as natives from "natives";
import native from "natives";

let entities: StreamedEntity[] = [];

let currentlyLoading = false;

async function loadModel(model) {
  return new Promise(resolve => {

    if (!natives.isModelValid(model))
      return resolve(false);

    const int = alt.setInterval(() => {
      if(!currentlyLoading) {
        if (natives.hasModelLoaded(model)) {
          alt.clearInterval(int)
          return resolve(true);
        }

        natives.requestModel(model);
        currentlyLoading = true;

        let interval = alt.setInterval(() => {
            if (natives.hasModelLoaded(model)) {
              resolve(true);
              currentlyLoading = false;
              alt.clearInterval(int)
              alt.clearInterval(interval);
            }
          },
          5);
      }
    }, 100);
  });
}

export interface StreamedEntity {
  id: number,
  type: number,
  position: Vector3,
  handle?: BaseObject|number,
  data: any
}

interface AttachOptions {
  entity: number,
  boneId: number,
  x: number,
  y: number,
  z: number,
  xRot: number,
  yRot: number,
  zRot: number,
  vertexIndex: number,
}

alt.onServer("entitySync:create", (entityId, entityType, position, currEntityData) => {

  if(currEntityData) {
    if(entityType === 0) {
      let blip =  new PointBlip(position.x, position.y, position.z)
      blip.sprite = currEntityData.sprite;
      blip.name = currEntityData.name;
      entities.push({
        id: entityId,
        type: entityType,
        position: position,
        data: currEntityData,
        handle: blip
      })
    } else if(entityType === 1) {
      // alt.log("create collaborate and listen")
      loadModel(currEntityData.model).then(() => {
        let handle = natives.createObject( currEntityData.model, position.x, position.y,
          position.z, false, false, false );
        entities.push({
          id: entityId,
          type: entityType,
          position: position,
          data: currEntityData,
          handle: handle
        })
        //alt.log("Handle:" +handle)
        natives.setEntityVisible(handle, true, false)
        natives.setObjectTextureVariation(handle, 0)
        natives.setEntityCollision(handle, false, true);
      });
    } else if(entityType === 2) {
      loadModel(currEntityData.model).then(() => {
        let handle = natives.createPed(1, currEntityData.model, position.x, position.y, position.z,
          currEntityData.heading, false, false)
        entities.push({
          id: entityId,
          type: entityType,
          position: position,
          data: currEntityData,
          handle: handle
        })
        if(currEntityData.static) {
          native.taskSetBlockingOfNonTemporaryEvents(handle, true);
          native.setBlockingOfNonTemporaryEvents(handle, true);
          native.setPedFleeAttributes(handle, 0, false);
          native.setEntityInvincible(handle, true)
          native.setPedCanBeTargetted(handle, false)
          alt.setTimeout(() => native.freezeEntityPosition(handle, true), 500)
        }
      })
    }
  } else {
    const thisEntity = entities.find(value => value.id === entityId && value.type == entityType);
    if(!thisEntity) return;
    if(entityType === 0) {
      thisEntity.handle = new Blip(thisEntity);
    } else if (entityType === 1) {
      loadModel(thisEntity.data.model).then(() => {
        let handle = natives.createObject( thisEntity.data.model, thisEntity.position.x, thisEntity.position.y,
          position.z, false, false, false );
        // alt.log("Handle:" +handle)
        thisEntity.handle = handle;
        natives.setEntityVisible(handle, true, false)
        natives.setObjectTextureVariation(handle, 0)
        natives.setEntityCollision(handle, false, true);
      });
    }
  }
})

alt.onServer("entitySync:remove", (entityId, entityType) => {

  const entity = entities.find(value => value.id === entityId && value.type == entityType);
  if(entityType === 0){
    let handle:BaseObject = <BaseObject>entity.handle;
    handle.destroy()
  } else if(entityType === 1) {
    // alt.log(JSON.stringify(entity))
    natives.deleteObject(<number>entity?.handle)
  } else if(entityType === 2) {
    natives.deleteEntity(<number>entity?.handle)
  }
  entity.handle = null;
})

alt.onServer("entitySync:updatePosition", (entityId: number, entityType, postition) => {
  entities.find(value => value.id === entityId && value.type == entityType).position = postition;
});

alt.onServer("entitySync:updateData", (entityId: number, entityType, newData) => {
  for (const key in newData) {
    entities.find(value => value.id === entityId && value.type == entityType).data[key] = newData[key];
  }
  if(entityType === 1) {
    let data = entities.find(value => value.id === entityId && value.type == entityType).data;
    // alt.log(JSON.stringify(data))
    if(data.attachToEntity) {
      const attachOptions: AttachOptions = data.attachToEntity;
      natives.attachEntityToEntity(<number>entities.find(value => value.id === entityId && value.type == entityType).handle, alt.Player.getByID(attachOptions.entity).scriptID,
        natives.getPedBoneIndex(alt.Player.local.scriptID, attachOptions.boneId), attachOptions.x, attachOptions.y, attachOptions.z, attachOptions.xRot, attachOptions.yRot, attachOptions.zRot, true, false, false, true, attachOptions.vertexIndex, true)
    }
  }
});

alt.onServer("entitySync:clearCache", (entityId, entityType) => {
  let idx = entities.findIndex(value => value.id === entityId && value.type == entityType)

  entities.splice(idx, 1)
})

alt.onServer("Animation:Start",(anim, dict, flag) => {
  if(!natives.hasAnimDictLoaded(dict)) natives.requestAnimDict(dict)
  const interval = alt.setInterval(() => {
    if(natives.hasAnimDictLoaded(dict)) {
      natives.taskPlayAnim(alt.Player.local.scriptID, dict, anim, 1, -1, -1, flag, 1, false, false, false)
      alt.clearInterval(interval);
    }
  }, 10);
})

alt.onServer("Scenario:Start",(scenario) => {
  natives.taskStartScenarioInPlace(alt.Player.local.scriptID, scenario, 0, true)
})

alt.onServer("Animation:Clear", () => {
  natives.clearPedTasks(alt.Player.local.scriptID);
  if(!alt.Player.local.vehicle) {
    natives.clearPedSecondaryTask(alt.Player.local.scriptID)
  }
})

alt.onServer("Animation:ForceClear", () => {
  natives.clearPedTasksImmediately(alt.Player.local.scriptID);
})
