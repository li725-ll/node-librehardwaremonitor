const { resolve } = require("path");
const fs = require("fs");

const targetPath = resolve(__dirname, "../dist/bin");
const binPath = resolve(__dirname, "../LibreHardwareMonitor/bin/Debug/net472");

function copyFile(filePath, filename) {
  return function (copyToDir) {
    const outFile = resolve(copyToDir, filename);
    if (fs.existsSync(outFile)) {
      fs.chmodSync(outFile, fs.statSync(outFile).mode | 146)
    }
    fs.writeFileSync(resolve(copyToDir, filename), fs.readFileSync(filePath));
  };
}

const binFilenames = fs.readdirSync(binPath);
for (const filename of binFilenames) {
  copyFile(resolve(binPath, filename), filename)(targetPath);
}
