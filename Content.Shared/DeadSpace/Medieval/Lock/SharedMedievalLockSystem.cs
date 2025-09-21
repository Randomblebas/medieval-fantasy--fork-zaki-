// Мёртвый Космос, Licensed under custom terms with restrictions on public hosting and commercial use, full text: https://raw.githubusercontent.com/dead-space-server/space-station-14-fobos/master/LICENSE.TXT

using Content.Shared.DeadSpace.Medieval.Lock.Components;
using Content.Shared.Doors;
using Robust.Shared.Containers;

namespace Content.Shared.DeadSpace.Medieval.Lock;

public abstract partial class SharedMedievalLockSystem : EntitySystem
{
    [Dependency] private readonly SharedContainerSystem _container = default!;
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<MedievalLockableComponent, BeforeDoorOpenedEvent>(OnBeforeDoorOpened);
    }

    private void OnBeforeDoorOpened(EntityUid uid, MedievalLockableComponent component, BeforeDoorOpenedEvent args)
    {

        if (!TryComp<MedievalLockComponent>(uid, out var lockComp))
        {
            var lockEnt = GetLock(uid);

            if (lockEnt != null && TryComp<MedievalLockComponent>(lockEnt, out lockComp) && !lockComp.Locked)
                return;
        }

        args.Cancel();
    }

    public bool IsLocked(EntityUid uid, MedievalLockComponent? component = null)
    {
        if (!Resolve(uid, ref component))
            return false;

        return component.Locked;
    }

    public bool TryUnlock(EntityUid uid, string key, MedievalLockComponent? component = null)
    {
        if (!Resolve(uid, ref component))
            return false;

        if (String.IsNullOrEmpty(component.KeyId))
            return false;

        if (component.KeyId != key)
            return false;

        if (!component.Locked)
            return false;

        UnLock(uid, component);

        return true;
    }

    public bool TryLock(EntityUid uid, string key, MedievalLockComponent? component = null)
    {
        if (!Resolve(uid, ref component))
            return false;

        if (String.IsNullOrEmpty(component.KeyId))
            return false;

        if (component.KeyId != key)
            return false;

        if (component.Locked)
            return false;

        Lock(uid, component);

        return true;
    }

    public void Lock(EntityUid uid, MedievalLockComponent? component = null)
    {
        if (!Resolve(uid, ref component))
            return;

        component.Locked = true;
        Dirty(uid, component);
    }

    public void UnLock(EntityUid uid, MedievalLockComponent? component = null)
    {
        if (!Resolve(uid, ref component))
            return;

        component.Locked = false;
        Dirty(uid, component);
    }

    public void SetKey(EntityUid uid, string key, MedievalLockComponent? component = null)
    {
        if (!Resolve(uid, ref component))
            return;

        component.KeyId = key;
        Dirty(uid, component);
    }

    public EntityUid? GetLock(EntityUid target)
    {
        if (!_container.TryGetContainer(target, "lock-container", out var lockContainer))
            return null;

        if (lockContainer is not ContainerSlot slot)
            return null;

        return slot.ContainedEntity;
    }
}
