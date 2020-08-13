import alt, {Player, Entity} from 'alt'
import * as native from "natives";

export default class PizzaDelivery {
    private pizza;

    constructor() {
        alt.on("syncedMetaChange", (entity: Entity, key: string, value: any) => {
            if (key == "JobPizza:GiveObject") {
                native.requestModel(604847691);
                this.pizza = native.createObject(604847691, entity.pos.x, entity.pos.y, entity.pos.z, true, true, false)
                native.attachEntityToEntity(this.pizza, entity.scriptID, native.getPedBoneIndex(entity.scriptID, 0xeb95), 
                    0, 0, 0, 0, 0, 180, false, false, true, true, 2, true);
            } else if (key == "JobPizza:PlaceObject") {
                native.detachEntity(this.pizza, true, true);
                alt.setTimeout(() => native.deleteObject(this.pizza), 30*100);
            }
        })
    }
}