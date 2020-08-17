import React, {Component} from "react";


export class Toast extends Component {

  constructor(props) {
    super(props);
    this.state = {
      toasts: [
      ]
    }
  }

  componentDidMount() {
    if("alt" in window) {
      alt.on("Toast:Add", this.addToast.bind(this))
      alt.on("Toast:Remove", this.removeToast.bind(this))
    } else {
      this.setState({
        toasts: [
          {id: "nearby-veh", title: "Auto: Osiris", text: "Drücke E um Respekt zu zollen"},
          {id: "nearby-player", title: "Spieler: gatno", text: "Drücke E um Respekt zu zollen"}
        ]
      })
    }
  }

  addToast(toast) {
    const toasts = this.state.toasts.filter(value => value.id !== toast.id);
    toasts.push(toast);
    this.setState({
      toasts: toasts
    });
  }

  removeToast(id) {
    this.setState({
      toasts: this.state.toasts.filter(value => value.id !== id)
    })
  }

  renderToasts() {
    return this.state.toasts.map(value => {
      return <div key={value.id} className="mb-2 text-center bg-gray-900 opacity-75 py-2 px-2 rounded-md toast-fade"
                style={{transition: "opacity 5s"}}>
        <div className="font-bold">{value.title}</div>
        <div>{value.text}</div>
      </div>;
    })
  }

  render() {
    return <div className="absolute top-0 right-0 container mt-3 text-white max-w-xs"
          style={{marginRight: "26rem"}}>
      {this.renderToasts()}
    </div>;
  }
}

export default Toast;
