import React, {Component} from "react"

export default class InputNumber extends Component {
  constructor(props) {
    super(props)

    const {cur, min, max, values, type} = this.props

    this.values = values
    this.cur = cur
    this.show = values[cur]
    this.min = min
    this.max = max
    this.type = type

    this.increment = this.increment.bind(this)
    this.decrement = this.decrement.bind(this)
  }

  increment(e) {
    if (this.cur == this.max) return
    this.cur++
    if (this.type == 1) {
      this.show = this.values[this.cur]
    } else {
      this.show = this.cur
    }
    this.forceUpdate()
  }

  decrement(e) {
    if (this.cur == this.min) return
    this.cur--
    if (this.type == 1) {
      this.show = this.values[this.cur]
    } else {
      this.show = this.cur
    }
    this.forceUpdate()
  }

  render() {
    return (
      <div>
        <div className="flex flex-row h-10 w-full rounded-lg relative bg-transparent mt-1">
          <button onClick={this.decrement} className="bg-gray-300 text-gray-600 hover:text-gray-700 hover:bg-gray-400 h-full w-20 rounded-l cursor-pointer outline-none">
            <span className="m-auto text-2xl font-thin">−</span>
          </button>
          <div className="w-full bg-gray-300">
            <p className="focus:outline-none mx-auto mt-2 text-center font-semibold text-md">{this.show}</p>
          </div>
          <button onClick={this.increment} className="bg-gray-300 text-gray-600 hover:text-gray-700 hover:bg-gray-400 h-full w-20 rounded-r cursor-pointer">
            <span className="m-auto text-2xl font-thin">+</span>
          </button>
        </div>
      </div>
    )
  }
}
