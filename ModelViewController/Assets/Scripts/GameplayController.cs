using System.Collections;
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
        private WaitForSeconds _waitAndStop;
        private bool _isLevelCompleted;
        private bool _hasBeatPlayed;
        ///<summary>
        ///The current Track.
        ///</summary> 
        public Track Track { get { return _track; } }
        public float SecondsPerBeat { get; private set; }
        public float BeatsPerSeconds { get; private set; }
        private TrackView _trackView;
        private static GameplayController _instance;
        public static GameplayController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (GameplayController)GameObject.FindObjectOfType(typeof(GameplayController));
                }
                return _instance;
            }
        }

        #region MonoBehaviour Methods
        private void Awake()
        {
            _instance = this;
            SecondsPerBeat = Track.BeatsPerMinute / 60f;
            BeatsPerSeconds = 60f / Track.BeatsPerMinute;
            _waitAndStop = new WaitForSeconds(BeatsPerSeconds * 2);
            _trackView = FindObjectOfType<TrackView>();
            if (_trackView == null)
            {
                Debug.LogWarning("No TrackView found in current scene");
            }
        }

        private void OnDestroy()
        {
            _instance = null;
        }

        private void Start()
        {
            InvokeRepeating("NextBeat", 0f, BeatsPerSeconds);
        }

        private void Update()
        {
            if (_hasBeatPlayed || _isLevelCompleted)
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
                        _isLevelCompleted = true;
                        StartCoroutine(WaitAndStop());
                    }
                }
            }
        }
        private void PlayBeat(int input)
        {
            _hasBeatPlayed = true;

            if (_track.Beats[CurrentBeat] == -1)
            {
                //Debug.Log(string.Format("{0} played out of time", input));
            }
            else if (_track.Beats[CurrentBeat] == input)
            {
                //Debug.Log(string.Format("{0} played correctly", input));
                _trackView.TriggerBeatView(CurrentBeat, TrackView.Trigger.Correct);
            }
            else
            {
                //Debug.Log(string.Format("{0} played, {1} is expected", input, _track.Beats[CurrentBeat]));
                _trackView.TriggerBeatView(CurrentBeat, TrackView.Trigger.Wrong);
            }
        }

        private void NextBeat()
        {
            //Debug.Log("Tick");

            if (!_hasBeatPlayed && _track.Beats[CurrentBeat] != -1)
            {
                //Debug.Log(string.Format("{0} missed", _track.Beats[CurrentBeat]));
                _trackView.TriggerBeatView(CurrentBeat, TrackView.Trigger.Missed);
            }
            _hasBeatPlayed = false;
            CurrentBeat++;
        }

        private IEnumerator WaitAndStop()
        {
            yield return _waitAndStop;
            enabled = false;
        }
        #endregion
    }
}
