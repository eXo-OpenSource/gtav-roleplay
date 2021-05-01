import {Route} from "react-router-dom";
import {AppWithStartup} from "./appWithStartup";

export const AppRoute = ({ id, component: Component, ...rest }) => {
    return (
        <Route
            {...rest}
            render={() => (
                <AppWithStartup id={id}>
                    <Component />
                </AppWithStartup>
            )}
        />
    );
};