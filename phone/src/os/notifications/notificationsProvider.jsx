import React, { createContext, useState, useCallback, useMemo, useRef, useEffect } from 'react';

export const NotificationsContext = createContext(null);

export const NotificationsProvider = ({children}) => {

    const [notifications, setNotifications] = useState([]);
    
    const icons = useMemo(
        () =>
            notifications.reduce((icons, curr) => {
                const find = icons.findIndex((i) => i.key === curr.app);
                if (find !== -1) {
                    icons[find].badge++;
                    return icons;
                }
                icons.unshift({ key: curr.app, icon: curr.notificationIcon, badge: 1 });
                return icons;
            }, []),
        [notifications],
    );
    return (
        <NotificationsContext.Provider
            value={{
                icons,
            }}
        >
            {children}
        </NotificationsContext.Provider>
    );
}