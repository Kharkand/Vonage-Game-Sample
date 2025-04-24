# Vonage Web Video SDK - Unity Integration Sample

This plugin demonstrates how to establish communication between Unity (running as WebGL in the browser) and the Vonage JavaScript SDK using Unity's JsInterop capabilities. It provides a pattern for sending commands and receiving events between your C# game code and the JavaScript video service running on the web page.

## How to run
Requisites:
- Vonage account to generate Video Chat credentials.
- [http server](https://runjs.app/blog/how-to-start-a-node-server) of your choice.

Once you have generated the Video Chat credentials from the [Vonage Dashboard](https://dashboard.vonage.com/), replace the values from `js/credentials.js` and run the project using your http server.


## Project Structure
### · unity-plugin
Contains all the code to handle In-Game and Web communications. The purpose of this code is to serve as an example on how to handle native communications between Unity and any Vonage Client SDK's. On this example, only the [Vonage JavaScript SDK](https://tokbox.com/developer/sdks/js/) has been taken into account.

`VideoCallManager` acts as the Unity's entry point for the Game to interact with the service. Also provides a series of events and methods to control the state of the service at any point.

### · index.html
Website that merges both Unity's [Unity](https://unity.com/demos/urp-3d-sample) and [Vonage](https://github.com/opentok/opentok-web-samples/tree/main/Basic%20Video%20Chat) Sample Projects. Loads the WebAssembly build files and Vonage SDK.

### · js
`app.js`Exposes the method to connect to the chat.

`credentials.js`To fill with the generated call credentials from the Vonage Dashboard. 

### · build
Contains the compiled WebAssembly of the [Unity 3D "The Cockpit" Demo](https://unity.com/demos/urp-3d-sample) and the custom Unity Plugin.
