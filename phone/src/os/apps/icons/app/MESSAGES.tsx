import React from 'react';
import SvgIcon, { SvgIconProps } from '@material-ui/core/SvgIcon';

const MessagesAppIcon = (props: SvgIconProps) => {
    return (
        <SvgIcon {...props}>
            <svg
                xmlns="http://www.w3.org/2000/svg"
                fillRule="evenodd"
                strokeLinejoin="round"
                strokeMiterlimit="2"
                clipRule="evenodd"
                viewBox="0 0 176 178"
            >
                <path fill="none" d="M0 0H176V178H0z"></path>
                <clipPath id="_clip1">
                    <path d="M0 0H176V178H0z"></path>
                </clipPath>
                <g clipPath="url(#_clip1)">
                    <path
                        d="M 0,88.950396 C 0,22.237599 21.972718,0 87.890873,0 c 65.918147,0 87.890867,22.237599 87.890867,88.950396 0,66.712794 -21.97272,88.950394 -87.890867,88.950394 C 21.972718,177.90079 0,155.66319 0,88.950396"
                        fill="#23db2c"
                        id="path84" />
                    <path
                        d="m 88.633182,26.299999 c -35.52982,0 -64.440721,23.128754 -64.440721,51.55396 0,16.045149 9.250449,30.939221 25.374729,40.860181 2.502139,1.53386 4.221842,4.08149 4.739963,6.99283 1.590311,8.9918 -0.547981,18.46444 -3.736895,26.66214 10.638375,-5.1188 21.600785,-12.97969 30.302146,-20.52788 2.144927,-1.8572 5.007042,-2.6345 7.789533,-2.44003 h 0.0127 c 35.494993,0 64.389293,-23.12203 64.389293,-51.550604 0,-28.421843 -28.90094,-51.550597 -64.43076,-51.550597 z"
                        id="path111"
                        fill="#fff"/>
                </g>
            </svg>
        </SvgIcon>
    );
};

export default MessagesAppIcon;