# Contributing
1. Clone the repo into the desired place
2. Download and install the latest Alt:V Server from https://altv.mp/#/downloads, `Release Candiate`
3. Copy the files into the desired place, e.g. Alt:V client install directory, `ALTV_DIR/server`
4. Copy the `build/server.cfg` into `ALTV_DIR/server/server.cfg`
5. Go into the `ALTV_DIR/server/resources` dir and open the command prompt
6. For the server type: `mklink /J "exov" "REPO_DIR/server/bin/server/netcoreapp3.1"`
7. For the client type: `mklink /J "exov-client" "REPO_DIR/client"`
8. In the repo root create a folder called `bin` and launch a command prompt in it
9. Type: `mklink /J "altv" "ALTV_DIR/server"`
10. Open `Exo Roleplay.sln` and build the project with `Ctrl+Shift+B`
11. Start `altv-server.exe`

# UI Development To Knows
* Development in Browser: `npm run dev`
* Build vor Alt:V Server: `npm run build`