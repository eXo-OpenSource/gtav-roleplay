import React, { useEffect, useRef } from "react";

const PopupColLabel = props => {
	return <div className="popup-item">
		<div className="float-left">{props.left}</div>
		<div className="float-right">{props.right}</div>
	</div>;
};

export default PopupColLabel;
