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
  edge.func(GetHardwareMessage)(null, (err, res)=>{
    if(err){
      console.log(err);
    }
    console.log(res);
  })
}

// 设置风扇转速
function setFanSpeed(fanName, speed){
  const SetFanSpeed = {
    assemblyFile: dllPath,
    typeName: "LibreHardwareMonitor.Entrypoint.NodeLibreHardwareMonitorLib",
    methodName: "GetHardwareMessage",
  };
  edge.func(SetFanSpeed)({fanName, speed}, (err, res) => {
    if (err) {
      console.log(err);
    }
    console.log(res);
  })
}

export default {
  getHardwareMessage,
  setFanSpeed
}
