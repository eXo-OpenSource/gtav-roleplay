import React, { useEffect, useRef } from "react";
import ProgressBar from "progressbar.js";

const defaultOptions = {
	strokeWidth: 4,
	easing: 'easeInOut',
	duration: 1400,
	color: '#FFEA82',
	trailColor: '#eee',
	trailWidth: 1,
	svgStyle: {width: '100%', height: '100%'},
	text: {
		style: {
			// Text color.
			// Default: same as stroke color (options.color)
			color: '#ffff',
			position: 'relative',
			'text-align': 'center',
			padding: 0,
			margin: 0,
			transform: null
		},
		autoStyleContainer: false
	},
	from: {color: '#FFEA82'},
	to: {color: '#ED6A5A'},
	step: (state, bar) => {
		bar.setText(Math.round(bar.value() * 100) + ' %');
	}

};

const Progressbar = props => {
	const barEl = useRef(null);
	const barRef = useRef(null);
	useEffect(() => {
		if (!barRef.current) {
			const options = { ...defaultOptions, ...props };
			barRef.current = new ProgressBar.Line(barEl.current, options)
		}
		barRef.current.animate(props.value);
	}, [props]);

	return <div ref={barEl} className="bar-container" >{props.children}</div>;
};

export default Progressbar;
