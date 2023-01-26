using Content.Shared.PDA;

namespace Content.Server.PDA.Ringer
{
    [RegisterComponent]
    public sealed class RingerComponent : Component
    {
        private Note[] _ringtone = new Note[SharedRingerSystem.RingtoneLength];

        [DataField("ringtone")]
        public Note[] Ringtone
        {
            get => _ringtone;
            set => _ringtone = value ?? throw new ArgumentNullException(nameof(value));
        }

        private float _timeElapsed = 0;

        [DataField("timeElapsed")]
        public float TimeElapsed
        {
            get => _timeElapsed;
            set => _timeElapsed = Math.Max(value, 0);
        }

        private int _noteCount = 0;

        /// <summary>
        /// Keeps track of how many notes have elapsed if the ringer component is playing.
        /// </summary>
        [DataField("noteCount")]
        public int NoteCount
        {
            get => _noteCount;
            set => _noteCount = Math.Max(value, 0);
        }

        private float _range = 3f;

        /// <summary>
        /// How far the sound projects in metres.
        /// </summary>
        [ViewVariables(VVAccess.ReadWrite)]
        [DataField("range")]
        public float Range
        {
            get => _range;
            set => _range = Math.Max(value, 0);
        }

        private float _volume = -4f;

        [ViewVariables(VVAccess.ReadWrite)]
        [DataField("volume")]
        public float Volume
        {
            get => _volume;
            set => _volume = value;
        }
    }

    [RegisterComponent]
    public sealed class ActiveRingerComponent : Component {}
}
