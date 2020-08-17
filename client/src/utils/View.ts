import * as alt from 'alt-client';
import * as native from 'natives';
import { Cursor } from './Cursor';

export let currentView;

alt.on('view:ForceClose', () => {
  if (!currentView) return;
  currentView.close();
});

export class View {
  constructor() {
    if (alt.Player.local.getMeta('chat')) return;
    if (currentView === undefined) {
      currentView = this;
    }
    return currentView;
  }

  open(url, killControls = true, overlay = false) {
    if (currentView.view) return;
    alt.Player.local.setMeta('viewOpen', true);
    alt.emit('chat:Toggle');
    currentView.view = new alt.WebView(url);
    currentView.events = [];
    currentView.on('close', currentView.close);
    currentView.view.url = url;
    currentView.view.isVisible = true;
    currentView.view.focus();
    currentView.ready = true;
    if(!overlay) {
      Cursor.show(true);
      // native.displayRadar(false);
    }
    if (killControls) {
      currentView.gameControls = this.toggleGameControls.bind(this);
      currentView.interval = alt.setInterval(currentView.gameControls, 0);
    }
  }

  // Close view and hide.
  close() {
    if (!currentView.ready) return;
    currentView.ready = false;

    currentView.events.forEach(event => {
      currentView.view.off(event.name, event.func);
    });

    Cursor.show(false);
    // native.displayRadar(true);
    currentView.view.off('close', currentView.close);
    currentView.view.unfocus();
    currentView.view.destroy();
    currentView.view = undefined;
    alt.Player.local.setMeta('viewOpen', false);
    alt.emit('chat:Toggle');
    if (currentView.interval !== undefined) {
      alt.clearInterval(currentView.interval);
      currentView.interval = undefined;
    }
  }

  // Bind on events, but don't turn off.
  on(name, func) {
    if (currentView.view === undefined) return;
    if (currentView.events.includes(event => event.name === name)) return;
    const event = {
      name,
      func
    };
    currentView.events.push(event);
    currentView.view.on(name, func);
  }

  emit(name, ...args) {
    if (!currentView.view) return;
    currentView.view.emit(name, ...args);
  }

  toggleGameControls() {
    native.disableAllControlActions(0);
    native.disableAllControlActions(1);
  }
}
