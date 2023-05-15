import edge from "edge-js";
import electronEdge from "electron-edge-js";
import { resolve } from "path";

//const dllPath = resolve(__dirname, "../LibreHardwareMonitor/bin/Debug/net472/LibreHardwareMonitorLibNode.dll");
const dllPath = resolve(__dirname, "./bin/LibreHardwareMonitorLibNode.dll");
const runInElectron = window.process.versions['electron'];

// 获取硬件信息
function getHardwareMessage(): Promise<any> {
  const GetHardwareMessage = {
   assemblyFile: dllPath,
   typeName: "LibreHardwareMonitor.Entrypoint.NodeLibreHardwareMonitorLib",
   methodName: "GetHardwareMessage",
  };

  return new Promise((resolve, reject) => {
    if (runInElectron){
      electronEdge.func(GetHardwareMessage)(null, (err, res) => {
        if (err) {
          reject(err);
        } else {
          resolve(JSON.parse(res as string));
        }
      })
    }else{
      edge.func(GetHardwareMessage)(null, (err, res) => {
        if (err) {
          reject(err);
        } else {
          resolve(JSON.parse(res as string));
        }
      })
    }
  });
}

// 设置风扇转速
function setFanSpeed(fanName: string, speed: number): Promise<boolean | Error>{
  const SetFanSpeed = {
    assemblyFile: dllPath,
    typeName: "LibreHardwareMonitor.Entrypoint.NodeLibreHardwareMonitorLib",
    methodName: "SetFanSpeed",
  };

  return new Promise((resolve, reject)=>{
    if (runInElectron) {
      electronEdge.func(SetFanSpeed)({ fanName, speed }, (err, res) => {
        if (err) {
          reject(err);
        } else {
          resolve(res as boolean);
        }
      })
    }else {
      edge.func(SetFanSpeed)({ fanName, speed }, (err, res) => {
        if (err) {
          reject(err);
        } else {
          resolve(res as boolean);
        }
      })
    }
  });
}

export default {
  getHardwareMessage,
  setFanSpeed
}
