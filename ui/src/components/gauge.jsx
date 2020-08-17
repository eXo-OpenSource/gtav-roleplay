import React, { useEffect, useRef } from "react";
import SvgGauge from "svg-gauge";

const defaultOptions = {
  animDuration: 1,
  showValue: false,
  initialValue: 0,
  max: 9000,
  value: 0,
  dialStartAngle: 150,
  dialEndAngle: 30,
  // Put any other defaults you want. e.g. dialStartAngle, dialEndAngle, radius, etc.
};

const Gauge = props => {
  const gaugeEl = useRef(null);
  const gaugeRef = useRef(null);
  useEffect(() => {
    if (!gaugeRef.current) {
      const options = { ...defaultOptions, ...props };
      gaugeRef.current = SvgGauge(gaugeEl.current, options);
      gaugeRef.current.setValue(options.initialValue);
    }
    gaugeRef.current.setValueAnimated(props.value, 1);
  }, [props]);

  return <div ref={gaugeEl} className="gauge-container" >{props.children}</div>;
};

export default Gauge;
