import {useEffect} from "react";
import {useSetRecoilState} from "recoil";
import {phoneState} from "./state";

export const usePhoneService = () => {

    const setVisibility = useSetRecoilState(phoneState.visibility);
    // Let client know UI is ready to accept events
    useEffect(() => {
        alt.emit("Phone:UiReady")
        
        alt.on("Phone:SetVisibility", setVisibility.bind(this))
    }, [setVisibility]);
};