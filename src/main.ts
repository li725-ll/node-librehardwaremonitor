import edge from "edge-js";
import { resolve } from "path";

const dllPath = resolve(__dirname, "../LibreHardwareMonitor/bin/Debug/net472/LibreHardwareMonitorLibNode.dll");

// 获取硬件信息
function getHardwareMessage(){
  const GetHardwareMessage = {
   assemblyFile: dllPath,
   typeName: "LibreHardwareMonitor.Entrypoint.NodeLibreHardwareMonitorLib",
   methodName: "GetHardwareMessage",
  };

  return new Promise((resolve, reject) => {
    edge.func(GetHardwareMessage)(null, (err, res) => {
      if (err) {
        reject(err);
      }else {
        resolve(JSON.parse(res as string));
      }
    })
  });
}

// 设置风扇转速
function setFanSpeed(fanName: string, speed: number){
  const SetFanSpeed = {
    assemblyFile: dllPath,
    typeName: "LibreHardwareMonitor.Entrypoint.NodeLibreHardwareMonitorLib",
    methodName: "SetFanSpeed",
  };

  return new Promise((resolve, reject)=>{
    edge.func(SetFanSpeed)({ fanName, speed },(err, res)=>{
      if (err){
        reject(err);
      }else {
        resolve(res);
      }
    })
  });
}

export default {
  getHardwareMessage,
  setFanSpeed
}
