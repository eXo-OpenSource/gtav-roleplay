import React, {Component} from "react"

export default class InputNumber extends Component {
  constructor(props) {
    super(props)

    const {current, min, max, values, type} = this.props

    this.values = values
    this.current = current
    this.show = values[current]
    this.min = min
    this.max = max
    this.type = type

    console.log(this.min + " | " + this.max)

    this.increment = this.increment.bind(this)
    this.decrement = this.decrement.bind(this)
  }

  increment(e) {
    if (this.current == this.max) return
    this.current++
    if (this.type == 1) {
      this.show = this.values[this.current]
    } else {
      this.show = this.current
    }
    this.forceUpdate()
    console.log(this.current + " | " + this.max)

  }

  decrement(e) {
    if (this.current == this.min) return
    this.current--
    if (this.type == 1) {
      this.show = this.values[this.current]
      console.log(this.show)
    } else {
      this.show = this.current
    }
    this.forceUpdate()
    console.log(this.current + " | " + this.max)
  }

  render() {
    return (
      <div>
        <div className="flex flex-row h-10 w-full rounded-lg relative bg-transparent mt-1">
          <button onClick={this.decrement} className=" bg-gray-300 text-gray-600 hover:text-gray-700 hover:bg-gray-400 h-full w-20 rounded-l cursor-pointer outline-none">
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
