import React from 'react';
import {useCurrentApp} from "./os/apps/useCurrentApp";
import {usePhoneVisibility} from "./os/phone/usePhoneVisibility";

function PhoneWrapper({children}) {

    const currentApp = useCurrentApp();
    const { bottom } = usePhoneVisibility();
    
    const phoneWidth = currentApp?.portrait ?? true ? "20vw" : "58vh";
    const phoneHeight = currentApp?.portrait ?? true ? "58vh" : "20vw";
    return (

        <div className="fixed right-2 bottom-5 h-96 transition-all duration-500" style={{height: phoneHeight, width: phoneWidth, bottom}}>
            <div className="z-50 absolute rounded-lg bg-gray-900 transition-all duration-500" style={{height: phoneHeight, width: phoneWidth}}>
                <div id="phone"
                     className={"absolute inset-y-2 inset-x-1 bg-white flex rounded-md text-white bg-center " + (currentApp?.portrait ?? true ? "flex-col" : "flex-row")}
                         style={{backgroundImage: "url(https://i.imgur.com/QH57fsb.jpeg)"}}>
                    {children}
                </div>
            </div>
        </div>

    );

}

export default PhoneWrapper;
