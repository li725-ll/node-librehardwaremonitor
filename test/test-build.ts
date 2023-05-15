import LibreHardwareMonitor from "../dist/main";

async function test() {
  const message = await LibreHardwareMonitor.getHardwareMessage();
  const result = await LibreHardwareMonitor.setFanSpeed("", 1);

  if (message && result != undefined) {
    console.log("success");
  } else {
    console.log("fail");
  }
}

test();
