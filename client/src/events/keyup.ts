import alt from 'alt-client';
import { CarRent } from '../environment/CarRent';
import { VehicleController } from '../systems/Vehicle';
import Chat from '../ui/Chat';
import { VehicleUI } from '../ui/VehicleUI';

export const KEYS = {
    G: 0x47,
    E: 69,
    X: 88,
    T: 0x54,
    ESC: 0x1B,
    SLASH: 0xBF,
    F7: 118,
    SPACE: 32
}

export const KEY_BINDS = {
    [KEYS.G]: {
        keyup: VehicleController.enterAsPassenger
    },
    [KEYS.ESC]: {
        keyup: Chat.closeChat
    },
    [KEYS.T]: {
        keyup: Chat.openChat
    },
    [KEYS.F7]: {
        keyup: Chat.hide
    },
    [KEYS.X]: {
        keydown: VehicleUI.activateInteractionMenu,
        keyup: VehicleUI.deactivateInteractionMenu,
    },
    [KEYS.SPACE]: {
        keyup: CarRent.closeUI,
    }
}

alt.on("keyup", (key: number) => {
    
    if(!KEY_BINDS[key])
        return;

    KEY_BINDS[key].keyup()
});

alt.on("keydown", (key: number) => {
    if(!KEY_BINDS[key])
    return;


    if(KEY_BINDS[key].keydown == null)
        return;

    KEY_BINDS[key].keydown()
});

