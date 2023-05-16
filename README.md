# node-librehardwaremonitor

A NodeJS library based on `LibreHardwareMonitor` for obtaining computer hardware information.

# Installing

## npm

```shell
npm i node-librehardwaremonitor
```

## yarn

```shell
yarn add node-librehardwaremonitor
```

# Usage

typescript

```ts
import librehardwaremonitor from "node-librehardwaremonitor";

setInterval(async ()=>{
    const result = await librehardwaremonitor.getHardwareMessage();
    console.log(result);
}, 1000)
```

javascript

```js
const librehardwaremonitor = require("node-librehardwaremonitor");

setInterval(async ()=>{
    const result = await librehardwaremonitor.getHardwareMessage();
    console.log(result);
}, 1000)
```

# API

## getHardwareMessage()

Used to obtain information about computer hardware.

## setFanSpeed(fanName: string, speed: number)

Used to set the speed of the fan. The value of speed is speedThe percentage of full speed of the fan.