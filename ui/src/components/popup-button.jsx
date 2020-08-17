import React, { useEffect, useRef } from "react";

const PopupButton = props => {
  const callback = () => {
    if("alt" in window) {
      alt.emit("Popup:Click", props.name)
    }
  }
  return <div onClick={callback} className={(props.selected ? "selected" : null )+" popup-item blue font-bold "+props.color} >{props.name}</div>;
};

export default PopupButton;
