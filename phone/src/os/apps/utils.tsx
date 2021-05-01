import React, { Suspense } from 'react';
import AppsIcon from '@material-ui/icons/Apps';
import { SvgIconComponent } from '@material-ui/icons';

export const createLazyAppIcon = (
        Icon,
    ) => (props) => {
    return (
        <Suspense fallback={<AppsIcon {...props} />}>
            <Icon {...props} />
        </Suspense>
    );
};