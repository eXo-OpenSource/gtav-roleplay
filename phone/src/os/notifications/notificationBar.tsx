import React from 'react';
import {useCurrentApp} from "../apps/useCurrentApp";

export const NotificationBar = () => {

    const currentApp = useCurrentApp();

    return (
        <div
            className={"relative px-1 z-30 w-full " + (currentApp != null ? currentApp.backgroundColor + " " + currentApp.color : "bg-gray-500 bg-opacity-50")}
            style={{height: "25px"}}>
            NotificationBar
        </div>
    )
};