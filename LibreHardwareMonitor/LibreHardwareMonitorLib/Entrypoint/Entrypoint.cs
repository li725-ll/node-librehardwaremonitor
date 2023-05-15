using System.Threading.Tasks;

namespace LibreHardwareMonitor.Entrypoint
{
    internal class NodeLibreHardwareMonitorLib: IEntrypoint {
        private Core _core;
        public NodeLibreHardwareMonitorLib() {
            _core = new Core();
        }
        public async Task<object> GetHardwareMessage(dynamic input) {
            return _core.GetHardwareMessage();
        }

        public async Task<object> SetFanSpeed(dynamic input) {
            return _core.SetFanSpeed(input.fanName, input.speed);
        }
    }
}
