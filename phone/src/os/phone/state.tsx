import {atom} from "recoil";

export const phoneState = {
    visibility: atom<boolean>({
        key: "phoneVisibility",
        default: false,
    })
}