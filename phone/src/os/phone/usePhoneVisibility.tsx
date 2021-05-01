import {useRecoilState} from "recoil";
import {phoneState} from "./state";
import {useEffect, useMemo, useState} from "react";

export const usePhoneVisibility = () => {
    const [visibility, setVisibility] = useRecoilState(phoneState.visibility)

    const [notifVisibility, setNotifVisibility] = useState(false);
    

    useEffect(() => {
        if (visibility) {
            setNotifVisibility(false);
        }
    }, [visibility]);

    const bottom = useMemo(() => {
        if (!visibility) {
            return `-60vh`;
        }
        return '1.25rem';
    }, [visibility]);

    return {
        bottom,
        visibility: notifVisibility || visibility,
    };
}