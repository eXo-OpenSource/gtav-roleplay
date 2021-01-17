import alt from 'alt-client';
import * as native from 'natives';

export function loadCutscene(name: string, flags: number): Promise<void> {
  return new Promise<void>(resolve => {
    native.requestCutscene(name, flags)

    let interval = alt.setInterval(() => {
      if (native.hasCutsceneLoaded()) {
        resolve();
        alt.clearInterval(interval);
      }
    }, 5)
  })
}
