import React from 'react';
import HomeIcon from '@material-ui/icons/Home';
import CloseIcon from '@material-ui/icons/Close';
import ArrowBackIcon from '@material-ui/icons/ArrowBackIos';
import {useHistory, useRouteMatch} from "react-router-dom";
import {useCurrentApp} from "../apps/useCurrentApp";

export const NavigationBar = () => {

    const history = useHistory();
    const {isExact} = useRouteMatch();
    const currentApp = useCurrentApp();

    return (
        <div className="bg-gray-700 z-10 py-1 px-1 ">
            <div
                className={"w-1/2 h-full mx-auto my-auto text-white flex items-center text-3xl justify-around " + (currentApp?.portrait ?? true ? "" : "flex-col-reverse")}>
                <CloseIcon fontSize="inherit" color="inherit" onClick={() => console.log("test")}/>
                <HomeIcon fontSize="inherit" color="inherit" onClick={() => !isExact && history.push("/")}/>
                <div className="text-2xl flex pl-2">
                    <ArrowBackIcon fontSize="inherit" color="inherit" onClick={() => !isExact && history.goBack()}/>
                </div>
            </div>
        </div>
    )
}

export default NavigationBar;
