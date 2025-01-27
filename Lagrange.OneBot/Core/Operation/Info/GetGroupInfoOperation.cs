using System.Text.Json;
using System.Text.Json.Nodes;
using Lagrange.Core;
using Lagrange.Core.Common.Interface.Api;
using Lagrange.OneBot.Core.Entity;
using Lagrange.OneBot.Core.Entity.Action;

namespace Lagrange.OneBot.Core.Operation.Info;

[Operation("get_group_info")]
public class GetGroupInfoOperation : IOperation
{
    public async Task<OneBotResult> HandleOperation(BotContext context, JsonObject? payload)
    {
        if (payload.Deserialize<OneBotGetGroupInfo>() is { } message)
        {
            var result = (await context.FetchGroups(message.NoCache)).FirstOrDefault(x => x.GroupUin == message.GroupId);

            if (result == null)
            {
                return new OneBotResult(null, -1, "failed");
            }

            return new OneBotResult(new OneBotGroup(result.GroupUin, result.GroupName), 0, "ok");
        }

        throw new Exception();
    }
}