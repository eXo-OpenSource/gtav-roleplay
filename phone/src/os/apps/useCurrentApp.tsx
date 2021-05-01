import {useMemo, useCallback} from 'react';
import {useApps} from "./useApps";
import {useLocation} from "react-router-dom";
import {IApp} from "./config";

export const useCurrentApp = () => {
    const { apps } = useApps();
    const location = useLocation();
    
    const getApp: IApp = useMemo(() => apps.find((a) => a.path === location.pathname) || null, [apps, location]);
    return getApp;
}