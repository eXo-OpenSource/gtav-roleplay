import {AppWrapper} from "../../components/appWrapper";
import React from "react";
import {Call} from "@material-ui/icons";

export const DialerApp = () => {
    return (
        <AppWrapper>
            <div className="px-1 flex flex-col flex-grow bg-white text-black divide-y">
                <div className="mx-auto text-center flex-col flex text-6xl flex-grow">

                    <span className="text-xl mt-2">Latest Calls TODO</span>
                </div>
                
                <div className="text-center my-2 my-1 py-2 h-4/6 text-xl">
                    <div className="grid grid-cols-3 gap-4 h-5/6">
                        {['1', '2', '3', '4', '5', '6', '7', '8', '9', '*','0','#'].map(value => (
                            <span key={value} className="m-2">{value}</span>
                        ))}
                    </div>
                    <div className="rounded-full bg-green text-5xl">
                        <Call fontSize="inherit" className="rounded-full bg-green-500 p-2 text-white"/>
                    </div>
                </div>
            </div>
        </AppWrapper>
    );
};