import React, {Component} from "react";
import PopupButton from "../components/popup-button";

const ComponentType = {
	Button: 1,
	Label: 2,
}

export class Popup extends Component {

	constructor(props) {
		super(props);
		this.state = {
			active: false,
			title: "Job",
			items: [
				{id: 1, name: "Starten"},
				{id: 1, name: "Schließen"}
			]
		}
	}

	componentDidMount() {

		document.addEventListener("keyup", this.onKeyUp.bind(this), false)
	}

	onKeyUp(event) {
		if(!this.state.active) return;

		switch(event.keyCode) {
			case 38: //up
				break
			case 40: //down
				break
		}
	}

	renderPopupContent() {
		return this.state.items.map((value, key) => {
			switch (value.id) {
				case ComponentType.Button:
					return <PopupButton key={key} name={value.name} />
			}
		})
	}

	render() {
		return !this.state.active ? null : <div className="container ml-auto max-w-sm mt-24 mr-4">
			<div className="card">
				<div className="card-header py-2">{this.state.title}</div>
				<div className="card-body pl-0 pr-0">
					{this.renderPopupContent()}
				</div>
				<div className="card-footer py-1" />
			</div>
		</div>;
	}
}

export default Popup;
