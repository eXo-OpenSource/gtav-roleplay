import React, {Component} from "react"

class CarRent extends Component {
  constructor(props) {
    super(props)

    this.rentCar = this.rentCar.bind(this)
  }

  rentCar(e) {
    let vehType = e.target.getAttribute("type")
    let price = e.target.getAttribute("price")
    alt.emit("CarRent:Rent", vehType, Number(price))
  }

  render() {
    return (
      <div class="container flex flex-wrap pt-4 pb-10 m-auto mt-48 md:mt-15 lg:px-12 xl:px-16">
        <div class="w-full px-0 lg:px-4">
          <h2 class="px-12 font-bold text-center md:text-2xl text-blue-700">Rent-A-Car Autovermietung</h2>
          <p class="py-1 text-sm text-center text-blue-700 mb-10">Willkommen bei Rent-A-Car - schließe das Fenster mit <b>SPACE</b></p>
          <div class="flex flex-wrap items-center justify-center py-4 pt-0">
            <div class="w-full p-4 md:w-1/2 lg:w-1/4 plan-card">
            <label class="flex flex-col rounded-lg shadow-lg group relative bg-white cursor-pointer hover:shadow-2xl">
              <div class="w-full px-4 py-12 rounded-t-lg card-section-1">
              <h3 class="mx-auto text-base font-semibold text-center underline text-blue-500 group-hover:text-white">Faggio</h3>
              <p class="text-5xl font-bold text-center group-hover:text-white text-blue-500">$75.<span class="text-3xl">00</span></p>
              <p class="text-xs text-center uppercase group-hover:text-white text-blue-500">Despawn nach 1 Stunde</p>
              </div>
              <div class="flex flex-col items-center justify-center w-full h-full py-6 rounded-b-lg bg-blue-500">
              <p class="text-xl text-white">1 Stunde</p>
              <button price="75" type="Faggio" class="w-5/6 py-2 mt-2 font-semibold text-center uppercase bg-white border border-transparent rounded text-blue-500" onClick={this.rentCar}>Mieten</button>
              </div>
            </label>
            </div>

            <div class="w-full p-4 md:w-1/2 lg:w-1/4">
            <label class="flex flex-col rounded-lg shadow-lg relative cursor-pointer hover:shadow-2xl">
              <div class="w-full px-4 py-8 rounded-t-lg bg-blue-500">
                <h3 class="mx-auto text-base font-semibold text-center underline text-white group-hover:text-white">BMX
                </h3>
                <p class="text-5xl font-bold text-center text-white">$25.<span class="text-3xl">00</span>
                </p>
                <p class="text-xs text-center uppercase text-white">Despawn nach 1 Stunde</p>
                </div>
                <div class="flex flex-col items-center justify-center w-full h-full py-12 rounded-b-lg bg-blue-700">
                <p class="text-xl text-white">1 Stunde</p>
                <button price="25" type="Bmx" class="w-5/6 py-2 mt-2 font-semibold text-center uppercase bg-white border border-transparent rounded text-blue-500" onClick={this.rentCar}>Jetzt mieten</button>
              </div>
            </label>
            </div>

            <div class="w-full p-4 md:w-1/2 lg:w-1/4 plan-card">
            <label class="flex flex-col rounded-lg shadow-lg group card-group relative bg-white hover:bg-jblue-secondary cursor-pointer hover:shadow-2xl">
              <div class="w-full px-4 py-12 rounded-t-lg card-section-1">
                <h3 class="mx-auto text-base font-semibold text-center underline text-blue-500 group-hover:text-white">Asbo</h3>
                <p class="text-5xl font-bold text-center group-hover:text-white text-blue-500">$125.<span class="text-3xl">00</span></p>
                <p class="text-xs text-center uppercase group-hover:text-white text-blue-500">Despawn nach 1 Stunde</p>
              </div>
              <div class="flex flex-col items-center justify-center w-full h-full py-6 rounded-b-lg bg-blue-500">
              <p class="text-xl text-white">1 Stunde</p>
              <button price="125" type="Asbo" class="w-5/6 py-2 mt-2 font-semibold text-center uppercase bg-white border border-transparent rounded text-blue-500" onClick={this.rentCar}>Mieten</button>
              </div>
            </label>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default CarRent;
