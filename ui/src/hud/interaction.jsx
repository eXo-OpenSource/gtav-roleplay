import React, { Component } from "react"

function getTanFromDegrees(degrees) {
  return Math.tan(degrees * Math.PI / 180);
}

class Interaction extends Component {
  constructor(props) {
    super(props)

    this.state = {
      show: false,
      currentSelection: 'Schließen',
      items: [],
      borders: []
    }

    this.centerRef = React.createRef()
  }

  componentDidMount() {
    window.addEventListener("mousemove", this.mouseMove.bind(this))
    if ('alt' in window) {
      alt.on('InteractionMenu:UpdateItems', this.updateUI.bind(this))
      alt.on('InteractionMenu:ToggleShow', (toggle) => {
        this.setState({show: toggle})
      })
    } else {
      this.setState({
        show: true,
        items: [
          {
            title: "Fahrzeuginfo",
            img: "info"
          },
          {
            title: "Licht an/aus",
            img: "light"
          },
          {
            title: "Auf-/ Zuschließen",
            img: "key"
          },
          {
            title: "Schließen",
            img: "close"
          },
          {
            title: "Motor an/aus",
            img: "engine"
          },
          {
            title: "Motorhaube auf/zu",
            img: "engineHood"
          },
          {
            title: "Kofferraum auf/zu",
            img: "trunk"
          }
        ]
      }, this.calculateBorders)
    }
  }

  componentWillUnmount() {
    window.removeEventListener("mousemove", this.mouseMove)
  }

  calculateBorders() {
    const bunds = this.state.items.map((value, index) => {
      const centralRot = 360 / this.state.items.length * index
      let m = 0;
      if (centralRot !== 0) {
        m = getTanFromDegrees(180 - centralRot)
      }
      return m
    })
    const finished = []
    bunds.forEach((value, index) => {
      finished[index] = {
        left: value
      }
      if(index !== 0) {
        finished[index-1].right = value;
      }
      if(index === bunds.length-1) {
        finished[index].right = bunds[0]
      }
    })
    this.setState({bounds: finished})
  }

  mouseMove(ev) {
    if(!this.centerRef.current) return
    const {pageX, pageY} = ev
    const {x, y, height, width} = this.centerRef.current.getBoundingClientRect()
    const centerY = y + height/2
    const centerX = x + width/2

    this.state.bounds.forEach((value, index) => {
      const leftRot = 360 / this.state.items.length * index
      let rightRot = 360 / this.state.items.length * (index+1)
      if (index === this.state.bounds.length - 1) {
        rightRot = 0
      }

      if(pageY < centerY && (leftRot > 180 && rightRot > 180)) return
      if(pageY > centerY && (leftRot < 180 && rightRot < 180)) return
      let boundLeft = -value.left*(pageX-centerX)+centerY
      let boundRight = -value.right*(pageX-centerX)+centerY

      if(boundLeft < 0) boundLeft = 0
      if(boundRight < 0) boundRight = 0

      if(value.right < 0 && boundRight < centerY && rightRot > 180) boundRight = centerY

      if(leftRot <= 180 && leftRot > 0) {
        if((value.left >= 0 && pageY > boundLeft) || (value.left < 0 && pageY < boundLeft)) {
          this.checkRightBorder(pageY, boundRight, rightRot, index, value)
        }
      } else {
        if((value.left >= 0 && pageY < boundLeft) || (value.left < 0 && pageY > boundLeft)) {
          this.checkRightBorder(pageY, boundRight, rightRot, index, value)
        }
      }
    })

  }

  checkRightBorder(pageY, boundRight, rightRot, index, value) {
    if(rightRot <= 180 && rightRot > 0) {
      if((value.right >= 0 && pageY < boundRight) || (value.right < 0 && pageY > boundRight)) {
        this.setState({
          currentSelection: this.state.items[index].title
        })
        if ("alt" in window) {
          alt.emit("InteractionMenu:CurrentSelection",this.state.items[index].title)
        }
      }
    } else {
      if((value.right >= 0 && pageY > boundRight) || (value.right < 0 && pageY < boundRight)) {
        this.setState({
          currentSelection: this.state.items[index].title
        })
        if ("alt" in window) {
          alt.emit("InteractionMenu:CurrentSelection",this.state.items[index].title)
        }
      }
    }
  }

  updateUI(items) {
    this.setState({
      items: items
    }, this.calculateBorders)
  }

  renderItems(radius, centerRadius) {
    return this.state.items.map((value, index) => {
      const centralRot = 360 / this.state.items.length
      const left = centralRot * index
      const skew = 90 - centralRot
      return <li key={value.title} className="absolute overflow-hidden"
                 style={{"transform": "rotate(" + left + "deg) skew(" + skew + "deg)","transformOrigin": "bottom right",
                   "width": centralRot > 90 ? "100%" : "50%", "height": centralRot > 90 ? "100%" : "50%", "bottom": centralRot > 90 ? "50%" : "initial", "right": centralRot > 90 ? "50%" : "initial",
                   "backgroundColor": this.state.currentSelection === value.title ? "rgba(0, 0, 0, 0.54)" :"rgba(0, 0, 0, 0.24)"}}>
        <div className="rounded-full" style={{"width": "200%", "height": "200%", "transformOrigin": "50% 50%",
        "transform": "skew(" + (-skew) + "deg) rotate(" + ((centralRot/2)-90) + "deg)"}}>
          <div className="absolute w-full text-center" style={{"top": "calc((" + (centralRot > 90 ? "50% +" : "") + radius + " - 2*"+ centerRadius + ") /2 - 2em/2)"}}>
            <div style={{"transform": "rotate(" + (-left - ((centralRot/2)-90)) + "deg)"}}>
              <img className="w-24 h-24 mx-auto mt-3 shadow-img" src={"https://static.exo.cool/exov-static/images/vehicles/interaction/" + value.img + ".png"}/>
            </div>
          </div>
        </div>
      </li>
    })
  }

  render() {
    const radius = "300px"
    const centerRadius = "100px"
    if(this.state.show) {
      return (
        <div className="select-none absolute inset-x-0 mx-auto overflow-hidden rounded-full"
             style={{"width": "600px", "height": "600px", "top": "20%"}}>
          <ul className="rounded-full m-0 p-0"
              style={{"width": "calc(2*" + radius + ")", "height": "calc(2*" + radius + ")"}}>
            {this.renderItems(radius, centerRadius)}
          </ul>
          <div ref={this.centerRef} className="rounded-full absolute bg-blue-400 flex items-center" style={{
            "top": "calc(50% - " + centerRadius + ")",
            "left": "calc(50% - " + centerRadius + ")",
            "width": "calc(2*" + centerRadius + ")",
            "height": "calc(2*" + centerRadius + ")"
          }}>
            <div className="text-center text-bold text-gray-200 text-3xl mx-auto">{this.state.currentSelection}</div>
          </div>
        </div>
      )
    } else {
      return null;
    }
  }
}

export default Interaction
