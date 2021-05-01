import { memo, useEffect } from 'react';

const Component = ({ children, id }) => {
    useEffect(() => {
        //TODO send app open to altv
        alt.emit("Phone:App:Startup", id)
    }, [id]);
    return children;
};

const AppWithStartup = memo(Component, () => true);

export { AppWithStartup };