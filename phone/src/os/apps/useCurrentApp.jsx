import {useMemo, useCallback} from 'react';
import {useApps} from "./useApps";
import {useLocation} from "react-router-dom";

export const useCurrentApp = () => {
    const { apps } = useApps();
    const location = useLocation();
    
    const getApp = useMemo(() => apps.find((a) => a.path === location.pathname) || null, [apps, location]);
    return getApp;
}