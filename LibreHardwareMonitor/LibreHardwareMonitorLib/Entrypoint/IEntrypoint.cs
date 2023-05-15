using System.Threading.Tasks;

namespace LibreHardwareMonitor.Entrypoint
{
    internal interface IEntrypoint
    {
        Task<object> GetHardwareMessage(dynamic input);
        Task<object> SetFanSpeed(dynamic input);
    }
}
