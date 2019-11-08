import * as alt from "alt";

let cursorCount = 0;

export class Cursor {
    static show(value) {
        if (value) {
            cursorCount += 1;
            alt.showCursor(true);
            return;
        }

        cursorCount -= 1;
        if (cursorCount <= -1) {
            cursorCount = 0;
            return;
        }

        try {
            alt.showCursor(false);
        } catch (e) {}
    }

}