import React, { Component, useEffect, useRef } from "react"

class ProgressCircle extends Component {
	constructor(props) {
	super(props)

	const { radius, stroke } = this.props

	this.normalizedRadius = radius - stroke * 2
	this.circumference = this.normalizedRadius * 2 * Math.PI
	}

	render() {
		const { radius, stroke, progress } = this.props
		const strokeDashoffset = this.circumference - progress / 100 * this.circumference

		return (
			<svg
				className="absolute"
				style={{marginLeft: "-0.4rem", marginTop: "-0.4rem"}}
				height={radius * 2}
				width={radius * 2}
				>
				<circle
					className="absolute"
					stroke="#1F85DE"
					fill="transparent"
					strokeWidth={stroke}
					strokeDasharray={this.circumference + " " + this.circumference}
					style={{strokeDashoffset}}
					stroke-width={stroke}
					r={this.normalizedRadius}
					cx={radius}
					cy={radius}
				/>
			</svg>
		)
	}
}

export default ProgressCircle
