import { resolve } from "path";
const runInElectron = process.versions['electron'];

const edgePackage = runInElectron ? "electron-edge-js" : "edge-js";


//const dllPath = resolve(__dirname, "../LibreHardwareMonitor/bin/Debug/net472/LibreHardwareMonitorLibNode.dll");
const pathDll = resolve(__dirname, "./bin/LibreHardwareMonitorLibNode.dll");
const dllPath = pathDll.indexOf("app.asar") > -1 ? pathDll.replace("app.asar", "app.asar.unpacked"): pathDll;
// 获取硬件信息
export async function getHardwareMessage(): Promise<any> {
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
export async function setFanSpeed(fanName: string, speed: number): Promise<boolean | Error>{
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
