import alt, {Player, Entity} from 'alt-client'
import * as native from "natives";

class PizzaDelivery {
  private static pizza;
  private static player = alt.Player.local;

  static handleSyncedMetaChange(entity: Entity, key: string, value: any) {
    if (key == "JobPizza:GivePizza") {
      native.requestModel(604847691);
      if (PizzaDelivery.pizza) native.deleteObject(PizzaDelivery.pizza);
      native.freezeEntityPosition(entity.scriptID, false);
      alt.toggleGameControls(true);
      PizzaDelivery.pizza = native.createObject(604847691, entity.pos.x, entity.pos.y, entity.pos.z, true, true, false);
      native.attachEntityToEntity(PizzaDelivery.pizza, entity.scriptID, native.getPedBoneIndex(entity.scriptID, 0xeb95),
        0, 0, 0, 0, 0, 0, false, false, true, true, 2, true);
    } else if (key == "JobPizza:PlacePizza") {
      alt.setTimeout(() => native.detachEntity(PizzaDelivery.pizza, true, true), 2000);
      alt.setTimeout(() => native.deleteObject(PizzaDelivery.pizza), 10 * 1000);
    } else if (key == "JobPizza:RemovePizza") {
      if (!PizzaDelivery.pizza) return
      native.detachEntity(PizzaDelivery.pizza, true, true)
      native.deleteObject(PizzaDelivery.pizza)
    } else if (key == "JobPizza:TakePizza") {
      native.freezeEntityPosition(entity.scriptID, true);
      alt.toggleGameControls(false);
      alt.setTimeout(() => {
        native.freezeEntityPosition(entity.scriptID, false);
        alt.toggleGameControls(true);
      }, 5000)
    }
  }

  static handleEveryTick() {
    if (native.getVehiclePedIsEntering(PizzaDelivery.player.scriptID) && PizzaDelivery.pizza || native.isPedSittingInAnyVehicle(PizzaDelivery.player.scriptID)) {
      if (!PizzaDelivery.pizza) return
      native.detachEntity(PizzaDelivery.pizza, true, true)
      native.deleteObject(PizzaDelivery.pizza)
    }
  }
}

alt.on("syncedMetaChange", PizzaDelivery.handleSyncedMetaChange)
alt.everyTick(PizzaDelivery.handleEveryTick)
