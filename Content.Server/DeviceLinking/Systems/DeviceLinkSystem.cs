using Content.Server.DeviceLinking.Components;
﻿using Content.Server.DeviceLinking.Events;
using Content.Server.DeviceNetwork;
using Content.Server.DeviceNetwork.Components;
using Content.Server.DeviceNetwork.Systems;
using Content.Shared.DeviceLinking;

namespace Content.Server.DeviceLinking.Systems;

public sealed class DeviceLinkSystem : SharedDeviceLinkSystem
{
    [Dependency] private readonly DeviceNetworkSystem _deviceNetworkSystem = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<DeviceLinkSinkComponent, DeviceNetworkPacketEvent>(OnPacketReceived);
    }

    public override void Update(float frameTime)
    {
        var query = EntityQueryEnumerator<DeviceLinkSinkComponent>();

        while (query.MoveNext(out var component))
        {
            if (component.InvokeLimit < 1)
            {
                component.InvokeCounter = 0;
                continue;
            }

            if(component.InvokeCounter > 0)
                component.InvokeCounter--;
        }
    }

}
