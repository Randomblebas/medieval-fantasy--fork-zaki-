// Мёртвый Космос, Licensed under custom terms with restrictions on public hosting and commercial use, full text: https://raw.githubusercontent.com/dead-space-server/space-station-14-fobos/master/LICENSE.TXT

using Robust.Shared.Audio;
using Robust.Shared.GameStates;

namespace Content.Shared.DeadSpace.Medieval.Lock.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(true)]
[Access(typeof(SharedMedievalLockSystem))]
public sealed partial class MedievalLockComponent : Component
{
    [DataField, AutoNetworkedField]
    public bool Locked = false;

    [DataField]
    public string KeyId = string.Empty;

    [DataField]
    public SoundSpecifier? AttachSound;
}
