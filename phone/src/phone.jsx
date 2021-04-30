import React from 'react';
import InjectDebugData from "./os/debug/injectDebugData";
import PhoneWrapper from "./phoneWrapper";
import {NotificationBar} from "./os/notifications/notificationBar";
import NavigationBar from "./os/navigation/navigationBar";
import {Route} from "react-router-dom";
import {HomeApp} from "./apps/home/home";
import {useApps} from "./os/apps/useApps";
import {useCurrentApp} from "./os/apps/useCurrentApp";
import {usePhoneService} from "./os/phone/usePhoneService";

export const Phone = () => {
    
    usePhoneService();

    const {apps} = useApps();
    const currentApp = useCurrentApp();

    return (
        <div>
            <PhoneWrapper>
                {currentApp?.portrait ?? true ? <>
                        <NotificationBar/>
                        <div className="w-full flex flex-col flex-grow" style={{height: "90%"}}>
                            <>
                                <Route exact path="/" component={HomeApp}/>
                                {apps.map((App) => (
                                    <App.Route key={App.id}/>
                                ))}
                            </>
                        </div>
                        <NavigationBar/>
                    </> 
                    :
                    <>
                        <div className="flex flex-col flex-grow">
                            <NotificationBar/>
                            <div className="w-full flex flex-col flex-grow">
                                <>
                                    <Route exact path="/" component={HomeApp}/>
                                    {apps.map((App) => (
                                        <App.Route key={App.id}/>
                                    ))}
                                </>
                            </div>
                        </div>
                        <NavigationBar/>
                    </>
                }
            </PhoneWrapper>
        </div>
    )
};
Phone.whyDidYouRender = true

InjectDebugData([{
    name: "Phone:SetVisibility",
    data: true,
}]);