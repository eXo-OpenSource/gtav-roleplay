import {useContext} from "react";
import {soundContext} from "./soundProvider";

export const useSoundProvider = () => {
    return useContext(soundContext);
}