import React, {Component} from "react";
import PopupButton from "../components/popup-button";
import PopupLabel from "../components/popup-label";
import PopupColLabel from "../components/popup-collabel";
import PopupHeader from "../components/popup-header";

const ComponentType = {
	Button: 1,
	Label: 2,
	ColLabel: 3,
	Header: 4,

}

export class Popup extends Component {

	constructor(props) {
		super(props);
		this.state = {
			active: false,
			selectedIndex: 0,
			title: "Job",
			items: [
				{id: 3, left: "Momentaner Job", right: "Müllabfuhr"},
				{id: 1, name: "Job kündigen", color: "red"},
				{id: 2, name: ""},
				{id: 4, left: "Müllabfuhr", right: "bis zu 4 Spieler"},
				{id: 1, name: "Alleine Arbeiten"},
				{id: 1, name: "Team zusammenstellen"},
				{id: 1, name: "Schließen", color: "red"}
			]
		}
	}

	componentDidMount() {
		if("alt" in window) {
			alt.on("Popup:Data", (data) => {
				this.setState(data);
			})
			alt.on("Popup:Close", () => {
				this.setState({
					active: false
				})
			})
		}
		document.addEventListener("keyup", this.onKeyUp.bind(this), false)
	}

	onKeyUp(event) {
		if(!this.state.active) return;

		switch(event.keyCode) {
			case 38: //up
				this.previous()
				break
			case 40: //down
				this.next()
				break
			case 13: //Enter
				if(this.state.items[this.state.selectedIndex].id === 1 && "alt" in window) {
					alt.emit("Popup:Click", this.state.items[this.state.selectedIndex].name)
				}
				break
		}
	}

	next() {
		this.setState({
			selectedIndex: this.state.selectedIndex + 1 >= this.state.items.length? 0: this.state.selectedIndex+1
		}, () => {
			if(this.state.items[this.state.selectedIndex].id !== 1) {
				this.next()
			}
		})
	}

	previous() {
		this.setState({
			selectedIndex: this.state.selectedIndex <= 0 ? this.state.items.length-1: this.state.selectedIndex-1
		}, () => {
			if(this.state.items[this.state.selectedIndex].id !== 1) {
				this.previous()
			}
		})
	}

	renderPopupContent() {
		return this.state.items.map((value, key) => {
			switch (value.id) {
				case ComponentType.Button:
					return <PopupButton key={key} name={value.name} color={value.color} selected={key === this.state.selectedIndex}/>
				case ComponentType.Label:
					return <PopupLabel key={key} name={value.name} />
				case ComponentType.ColLabel:
					return <PopupColLabel key={key} left={value.left} right={value.right} />
				case ComponentType.Header:
					return <PopupHeader key={key} left={value.left} right={value.right} />
			}
		})
	}

	render() {
		return !this.state.active ? null : <div className="absolute top-0 right-0 container ml-auto max-w-sm mt-24 mr-4">
			<div className="card">
				<div className="card-header py-2">{this.state.title}</div>
				<div className="card-body py-2 pl-0 pr-0">
					{this.renderPopupContent()}
				</div>
				<div className="card-footer py-1" />
			</div>
		</div>;
	}
}

export default Popup;
