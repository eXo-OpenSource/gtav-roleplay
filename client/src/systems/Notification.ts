import alt from "alt-client";
import * as native from 'natives';

alt.log('Loaded: client->utility->ped.mjs');

alt.onServer("sendNotification", text => {
    native.beginTextCommandThefeedPost("CELL_EMAIL_BCON");
    text.match(/.{1,99}/g).forEach(textBlock => {
        native.addTextComponentSubstringPlayerName(textBlock)
    });
    native.endTextCommandThefeedPostTicker(false, false);
})