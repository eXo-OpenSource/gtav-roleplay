import React from "react";
import {DIALER_APP_PRIMARY_COLOR, DIALER_APP_TEXT_COLOR} from "../../../apps/dialer/dialer.theme";
import {DialerApp} from "../../../apps/dialer/dialer";
import {AppRoute} from "../appRoute";
import {SvgIconProps} from "@material-ui/core";
import {INotificationIcon} from "../../notifications/notificationsProvider";
import {MessagesApp} from "../../../apps/messages/messages";
import {MESSAGES_APP_PRIMARY_COLOR, MESSAGES_APP_TEXT_COLOR} from "../../../apps/messages/messages.theme";

export interface IAppConfig {
    id: string;
    nameLocale: string;
    backgroundColor: string;
    color: string;
    path: string;
    Route: React.FC;
    portrait: boolean;
}

export type IApp = IAppConfig & {
    notification: INotificationIcon;
    icon: JSX.Element;
    notificationIcon: JSX.Element;
    NotificationIcon: React.FC<SvgIconProps>;
    Icon: React.FC<SvgIconProps>;
};
export const APPS : IAppConfig[] = [
    {
        id: 'DIALER',
        nameLocale: 'Telefon',
        backgroundColor: DIALER_APP_PRIMARY_COLOR,
        color: DIALER_APP_TEXT_COLOR,
        path: '/phone',
        Route: () => <AppRoute id="DIALER" path="/phone" component={DialerApp} />,
        portrait: true,
    },
    {
        id: 'MESSAGES',
        nameLocale: 'Nachrichten',
        backgroundColor: MESSAGES_APP_PRIMARY_COLOR,
        color: MESSAGES_APP_TEXT_COLOR,
        path: '/messages',
        Route: () => <AppRoute id="MESSAGES" path="/messages" component={MessagesApp} />,
        portrait: true,
    }
]