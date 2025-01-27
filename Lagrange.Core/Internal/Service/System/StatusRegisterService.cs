using Lagrange.Core.Common;
using Lagrange.Core.Internal.Event.Protocol;
using Lagrange.Core.Internal.Event.Protocol.System;
using Lagrange.Core.Internal.Packets.System;
using Lagrange.Core.Utility.Binary;
using Lagrange.Core.Utility.Extension;
using ProtoBuf;

namespace Lagrange.Core.Internal.Service.System;

[EventSubscribe(typeof(StatusRegisterEvent))]
[Service("trpc.qq_new_tech.status_svc.StatusService.Register")]
internal class StatusRegisterService : BaseService<StatusRegisterEvent>
{
    protected override bool Build(StatusRegisterEvent input, BotKeystore keystore, BotAppInfo appInfo, BotDeviceInfo device,
        out BinaryPacket output, out List<BinaryPacket>? extraPackets)
    {
        var packet = new ServiceRegister
        {
            Guid = device.Guid.ToByteArray().Hex(true),
            Type = 0,
            CurrentVersion = appInfo.CurrentVersion,
            Field4 = 0,
            LocaleId = 2052,
            Online = new OnlineOsInfo
            {
                User = device.DeviceName,
                Os = appInfo.Kernel,
                OsVer = device.SystemKernel,
                VendorName = "",
                OsLower = appInfo.VendorOs,
            },
            SetMute = 0,
            RegisterVendorType = 0,
            RegType = 1,
        };
        using var stream = new MemoryStream();
        Serializer.Serialize(stream, packet);

        output = new BinaryPacket(stream);
        extraPackets = null;
        return true;
    }

    protected override bool Parse(byte[] input, BotKeystore keystore, BotAppInfo appInfo, BotDeviceInfo device,
        out StatusRegisterEvent output, out List<ProtocolEvent>? extraEvents)
    {
        var response = Serializer.Deserialize<ServiceRegisterResponse>(input.AsSpan());

        output = StatusRegisterEvent.Result(response.Message ?? "OK");
        extraEvents = null;
        return true;
    }
}