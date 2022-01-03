using UnityEngine;

namespace ModelViewController
{
    public class GameplayController : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private KeyCode _left;
        [SerializeField] private KeyCode _right;
        [SerializeField] private KeyCode _up;
        [SerializeField] private KeyCode _down;

        [Header("Track")]
        [Tooltip("Beats Track to play")]
        [SerializeField] private Track _track;
        private bool _isCompleted;
        private bool _hasPlayed;
        ///<summary>
        ///The current Track.
        ///</summary> 
        public Track Track { get { return _track; } }
        public float SecondsPerBeat { get; private set; }
        public float BeatsPerSeconds { get; private set; }

        #region MonoBehaviour Methods
        private void Awake()
        {
            SecondsPerBeat = Track.BeatsPerMinute / 60f;
            BeatsPerSeconds = 60f / Track.BeatsPerMinute;
        }

        private void Start()
        {
            InvokeRepeating("NextBeat", 0f, BeatsPerSeconds);
        }

        private void Update()
        {
            if (_hasPlayed || _isCompleted)
            {
                return;
            }

            if (Input.GetKeyDown(_left))
            {
                PlayBeat(0);
            }
            if (Input.GetKeyDown(_right))
            {
                PlayBeat(1);
            }
            if (Input.GetKeyDown(_up))
            {
                PlayBeat(2);
            }
            if (Input.GetKeyDown(_down))
            {
                PlayBeat(3);
            }
        }
        #endregion

        #region Gameplay
        private int _currentBeat;
        public int CurrentBeat
        {
            get { return _currentBeat; }
            set
            {
                if (value != _currentBeat)
                {
                    _currentBeat = value;

                    if (_currentBeat == _track.Beats.Count)
                    {
                        CancelInvoke("NextBeat");
                        _isCompleted = true;
                    }
                }
            }
        }
        private void PlayBeat(int input)
        {
            Debug.Log(input);

            if (_track.Beats[CurrentBeat] == -1)
            {
                Debug.Log(string.Format("{0} played out of time", input));
            }
            else if (_track.Beats[CurrentBeat] == input)
            {
                Debug.Log(string.Format("{0} played correctly", input));
            }
            else
            {
                Debug.Log(string.Format("{0} played, {1} is expected", input, _track.Beats[CurrentBeat]));
            }
        }

        private void NextBeat()
        {
            Debug.Log("Tick");

            if (!_hasPlayed && _track.Beats[CurrentBeat] != -1)
            {
                Debug.Log(string.Format("{0} missed", _track.Beats[CurrentBeat]));
            }
            _hasPlayed = false;
            CurrentBeat++;
        }
        #endregion
    }
}
