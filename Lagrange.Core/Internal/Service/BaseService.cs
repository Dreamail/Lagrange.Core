using Lagrange.Core.Common;
using Lagrange.Core.Internal.Event.Protocol;
using Lagrange.Core.Internal.Packets;
using Lagrange.Core.Utility.Binary;

namespace Lagrange.Core.Internal.Service;

internal class BaseService<TEvent> : IService where TEvent : ProtocolEvent
{
    protected virtual bool Parse(byte[] input, BotKeystore keystore, BotAppInfo appInfo, BotDeviceInfo device, 
        out TEvent output, out List<ProtocolEvent>? extraEvents)
    {
        extraEvents = null;
        return (output = null!) != null;
    }

    protected virtual bool Build(TEvent input, BotKeystore keystore, BotAppInfo appInfo, BotDeviceInfo device, 
        out BinaryPacket output, out List<BinaryPacket>? extraPackets)
    {
        extraPackets = null;
        return (output = null!) != null;
    }

    bool IService.Parse(byte[] input, BotKeystore keystore, BotAppInfo appInfo, BotDeviceInfo device,
        out ProtocolEvent? output, out List<ProtocolEvent>? extraEvents)
    {
        bool result = Parse(input, keystore, appInfo, device, out var @event, out extraEvents);
        output = @event;
        return result;
    }

    bool IService.Build(ProtocolEvent input, BotKeystore keystore, BotAppInfo appInfo, BotDeviceInfo device,
        out BinaryPacket? output, out List<BinaryPacket>? extraPackets) =>
        Build((TEvent) input, keystore, appInfo, device, out output, out extraPackets);
}