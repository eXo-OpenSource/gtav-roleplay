import alt, {Player, Entity} from 'alt'
import * as native from "natives";

export default class PizzaDelivery {
    private pizza;

    constructor() {
        alt.on("syncedMetaChange", (entity: Entity, key: string, value: any) => {
            if (key == "JobPizza:GivePizza") {
                native.requestModel(604847691);
                if (this.pizza) native.deleteObject(this.pizza);
                native.freezeEntityPosition(entity.scriptID, false);
                alt.toggleGameControls(true);
                this.pizza = native.createObject(604847691, entity.pos.x, entity.pos.y, entity.pos.z, true, true, false);
                native.attachEntityToEntity(this.pizza, entity.scriptID, native.getPedBoneIndex(entity.scriptID, 0xeb95), 
                    0, 0, 0, 0, 0, 0, false, false, true, true, 2, true);
            } else if (key == "JobPizza:PlacePizza") {
                alt.setTimeout(() => native.detachEntity(this.pizza, true, true), 2000);
                alt.setTimeout(() => native.deleteObject(this.pizza), 10*1000);
            } else if (key == "JobPizza:TakePizza") {
                native.freezeEntityPosition(entity.scriptID, true);
                alt.toggleGameControls(false);
                alt.setTimeout(() => {
                    native.freezeEntityPosition(entity.scriptID, false);
                    alt.toggleGameControls(true);
                }, 5000)
            }
        })
    }
}