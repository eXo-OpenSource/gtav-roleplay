import React, { Component } from 'react';

//Is there a better name?
const PetrolStation = () => {

  return (
    <div class="container flex flex-wrap pt-4 pb-10 m-auto mt-48 md:mt-15 lg:px-12 transform scale-75">
      <div class="card-header container text-3xl">Tankstelle</div>
      <div class="card-body container flex flex-col items-center">
        <div class="w-full px-0 lg:px-4 flex justify-center py-2">
          <h2 class="w-1/4 px-12 text-center md:text-2xl text-white">Preis Pro Liter</h2>
          <p class="text-3xl w-1/4 font-bold text-center bg-white ext-blue-500">$1.<span class="text-xl">764</span></p>


        </div>
        <div class="w-full px-0 lg:px-4 flex justify-center py-2">
          <h2 class="w-1/4 px-12 text-center md:text-2xl text-white">Getankte Liter</h2>
          <p class="text-3xl w-1/4 font-bold text-center bg-white ext-blue-500">8.<span class="text-xl">928</span></p>


        </div>
        <div class="w-full px-0 lg:px-4 flex justify-center py-2">
          <h2 class="w-1/4 px-12 text-center md:text-2xl text-white">Gesamt Preis</h2>
          <p class="text-3xl w-1/4 font-bold text-center bg-white ext-blue-500">$27.<span class="text-xl">209</span></p>


        </div>
        <button class="w-1/2 py-2 mt-8 font-semibold text-center uppercase bg-blue-600 border border-transparent rounded" >Auftanken</button>

      </div>
      <div class="card-footer container text-center text-white">Schließe das Fenster mit SPACE</div>
    </div>
  )
}

export default PetrolStation;
