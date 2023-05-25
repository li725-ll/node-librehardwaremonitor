import { resolve } from "path";
const runInElectron = process.versions['electron'];

const edgePackage = runInElectron ? "electron-edge-js" : "edge-js";


//const dllPath = resolve(__dirname, "../LibreHardwareMonitor/bin/Debug/net472/LibreHardwareMonitorLibNode.dll");
const dllPath = resolve(__dirname, "./bin/LibreHardwareMonitorLibNode.dll");

// 获取硬件信息
async function getHardwareMessage(): Promise<any> {
  const edge = await import(edgePackage);
  const GetHardwareMessage = {
   assemblyFile: dllPath,
   typeName: "LibreHardwareMonitor.Entrypoint.NodeLibreHardwareMonitorLib",
   methodName: "GetHardwareMessage",
  };

  return new Promise((resolve, reject) => {
    edge.func(GetHardwareMessage)(null, (err: unknown, res: unknown) => {
      if (err) {
        reject(err);
      } else {
        resolve(JSON.parse(res as string));
      }
    })
  });
}

// 设置风扇转速
async function setFanSpeed(fanName: string, speed: number): Promise<boolean | Error>{
  const edge = await import(edgePackage);
  const SetFanSpeed = {
    assemblyFile: dllPath,
    typeName: "LibreHardwareMonitor.Entrypoint.NodeLibreHardwareMonitorLib",
    methodName: "SetFanSpeed",
  };

  return new Promise((resolve, reject)=>{
    edge.func(SetFanSpeed)({ fanName, speed }, (err: unknown, res: unknown) => {
      if (err) {
        reject(err);
      } else {
        resolve(res as boolean);
      }
    })
  });
}

export default {
  getHardwareMessage,
  setFanSpeed
}
