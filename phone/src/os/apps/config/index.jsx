import {DIALER_APP_PRIMARY_COLOR, DIALER_APP_TEXT_COLOR} from "../../../apps/dialer/dialer.theme";
import {DialerApp} from "../../../apps/dialer/dialer";
import {AppRoute} from "../appRoute";

export const APPS = [
    {
        id: 'DIALER',
        nameLocale: 'Telefon',
        backgroundColor: DIALER_APP_PRIMARY_COLOR,
        color: DIALER_APP_TEXT_COLOR,
        path: '/phone',
        Route: () => <AppRoute id="DIALER" path="/phone" component={DialerApp} />,
        portrait: true,
    }
]