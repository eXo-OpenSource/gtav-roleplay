import React, { useEffect, useRef } from "react";

const PopupHeader = props => {
  return <div className="popup-item h-9 leading-9 bg-gray-800">
    <div className="float-left">{props.left}</div>
    <div className="float-right">{props.right}</div>
  </div>;
};

export default PopupHeader;
