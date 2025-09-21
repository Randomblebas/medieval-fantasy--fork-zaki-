// Мёртвый Космос, Licensed under custom terms with restrictions on public hosting and commercial use, full text: https://raw.githubusercontent.com/dead-space-server/space-station-14-fobos/master/LICENSE.TXT

using Robust.Shared.Audio;

namespace Content.Server.DeadSpace.Medieval.Lock.Components;

[RegisterComponent]
public sealed partial class MedievalKeyDuplicatorComponent : Component
{
    [DataField]
    public string KeyId = string.Empty;

    [DataField]
    public float UseDuration = 15f;

    [DataField]
    public bool IsDuplicatorState = true;

    [DataField, ViewVariables(VVAccess.ReadOnly)]
    public EntityUid? Sound;

    [DataField]
    public SoundSpecifier? UseSound;
}
