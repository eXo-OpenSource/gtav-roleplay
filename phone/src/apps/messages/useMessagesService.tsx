import {useLocation} from "react-router-dom";
import {useCallback, useEffect} from "react";
import {useNotifications} from "../../os/notifications/useNotifications";

export const useMessagesService = () => {
    const { pathname } = useLocation();
    const { removeId, addNotification, addNotificationAlert } = useNotifications();

    const handleMessageBroadcast = useCallback(
        ({ embed, title, content }) => {
            
            addNotificationAlert({
                app: "MESSAGES",
                content: content,
                title: title
            })
        },
        [pathname, addNotificationAlert],
    );
    
    useEffect(() => {
        alt.on("Phone:Messages:Incoming", handleMessageBroadcast.bind(this))
    }, [handleMessageBroadcast])
}