import React, { useEffect, useRef } from "react";

const PopupButton = props => {
	const callback = () => {
		if("alt" in window) {
			alt.emit("Popup:Click", props.name)
		}
	}
	return <div onClick={callback} className="popup-item hover:bg-blue-400 hover:text-black">{props.name}</div>;
};

export default PopupButton;
