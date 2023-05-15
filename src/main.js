"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var edge_js_1 = require("edge-js");
var path_1 = require("path");
var dllPath = (0, path_1.resolve)(__dirname, "../LibreHardwareMonitor/bin/Debug/net472/LibreHardwareMonitorLibNode.dll");
// 获取硬件信息
function getHardwareMessage() {
    var GetHardwareMessage = {
        assemblyFile: dllPath,
        typeName: "LibreHardwareMonitor.Entrypoint.NodeLibreHardwareMonitorLib",
        methodName: "GetHardwareMessage",
    };
    return new Promise(function (resolve, reject) {
        edge_js_1.default.func(GetHardwareMessage)(null, function (err, res) {
            if (err) {
                reject(err);
            }
            else {
                resolve(JSON.parse(res));
            }
        });
    });
}
// 设置风扇转速
function setFanSpeed(fanName, speed) {
    var SetFanSpeed = {
        assemblyFile: dllPath,
        typeName: "LibreHardwareMonitor.Entrypoint.NodeLibreHardwareMonitorLib",
        methodName: "SetFanSpeed",
    };
    return new Promise(function (resolve, reject) {
        edge_js_1.default.func(SetFanSpeed)({ fanName: fanName, speed: speed }, function (err, res) {
            if (err) {
                reject(err);
            }
            else {
                resolve(res);
            }
        });
    });
}
exports.default = {
    getHardwareMessage: getHardwareMessage,
    setFanSpeed: setFanSpeed
};
