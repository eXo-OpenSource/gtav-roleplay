import './wdyr';
import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import {HashRouter} from "react-router-dom";
import {Phone} from "./phone";
import {NotificationsProvider} from "./os/notifications/notificationsProvider";
import {RecoilRoot} from "recoil";

ReactDOM.render(
    <HashRouter>
        <RecoilRoot>
            <NotificationsProvider>
                <Phone/>
            </NotificationsProvider>
        </RecoilRoot>
    </HashRouter>,
    document.getElementById('root')
);