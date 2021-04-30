import {AppWrapper} from "../../components/appWrapper";
import React from "react";
import {useApps} from "../../os/apps/useApps";
import {AppIcon} from "../../components/appIcon";
import {Link} from "react-router-dom";

export const HomeApp = () => {

    const { apps } = useApps();
    return (
        <AppWrapper>
            <div className="px-1 mt-6 flex flex-col flex-grow">
                <div className="mx-auto text-center flex-col flex text-6xl flex-grow">
                    <span>20</span>
                    <span>50</span>
                    <span className="text-xl mt-2">Mittwoch, 29. April</span>
                </div>
                <div className="grid grid-cols-4 gap-4 text-center my-2 mx-1">
                    {apps.map((app) => (
                        <div key={app.id}>
                            <Link to={app.path}>
                                <AppIcon {...app}/>
                            </Link>
                        </div>
                    ))}
                </div>
            </div>
        </AppWrapper>
    );
};
