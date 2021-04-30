import {atom} from "recoil";

export const phoneState = {
    visibility: atom({
        key: "phoneVisibility",
        default: false,
    })
}