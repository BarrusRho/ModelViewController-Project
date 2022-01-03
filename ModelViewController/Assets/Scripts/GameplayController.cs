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
        private int _current;
        public int Current
        {
            get { return _current;}
            set
            {
                if (value != _current)
                {
                    _current = value;

                    if (_current == _track.Beats.Count)
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
        }

        private void NextBeat()
        {
            Debug.Log("Tick");
            Current++;
        }
        #endregion
    }
}
