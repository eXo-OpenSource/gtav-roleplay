import * as alt from 'alt-client'
import * as native from 'natives'

alt.onServer("IPLManager:requestIPL", (ipls: string[]) => {
  ipls.forEach(ipl => {
    alt.requestIpl(ipl)
  })
})

alt.onServer("IPLManager:removeIPL", (ipls: string[]) => {
  ipls.forEach(ipl => {
    alt.removeIpl(ipl)
  })
})
