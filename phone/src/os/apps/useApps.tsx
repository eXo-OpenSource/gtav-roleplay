import React, {useMemo, useCallback} from 'react';
import {APPS, IApp} from "./config";
import {createLazyAppIcon} from "./utils";
import {useNotifications} from "../notifications/useNotifications";

export const useApps = () => {
    const { icons } = useNotifications();
    const apps: IApp[] = useMemo(() => APPS.map((app) => {
        
        const SvgIcon = React.lazy(
            () => import(`./icons/svg/${app.id}`)
        );
        const AppIcon = React.lazy(
            () => import(`./icons/app/${app.id}`)
        );

        const NotificationIcon = createLazyAppIcon(SvgIcon);
        const Icon = createLazyAppIcon(AppIcon);
        
        return {
            ...app,
            notification: icons.find((i) => i.key === app.id),
            NotificationIcon,
            Icon,
            notificationIcon: (
                <NotificationIcon fontSize="small" />
            ),
            icon: <Icon />,
            
        }
    }), [icons]);

    const getApp = useCallback((id) => apps.find((a) => a.id === id) || null, [apps]);
    return { apps, getApp };
}

export const useApp = (id: string): IApp => {
    const { getApp } = useApps();
    return getApp(id);
};