using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelViewController
{
    [CreateAssetMenu(menuName = "ModelViewController/New Track", fileName = "New Beats Track.asset")]
    public class Track : ScriptableObject
    {
        [Header("Playback Settings")]
        [Tooltip("Number of beats per minute (bpm)")]
        [Range(30, 360)] public int BeatsPerMinute = 120;
        [HideInInspector] public List<int> Beats;

        static public int Inputs = 4;

        [Header("Random Settings")]

        [Tooltip("Number of preroll (empty) beats")]
        [Range(0, 10)] [SerializeField] private int _preRoll = 10;
        [Tooltip("Minimum number of beats per block")]
        [Range(1, 20)] [SerializeField] private int _minBlock = 2;
        [Tooltip("Maximum number of beats per block")]
        [Range(1, 20)] [SerializeField] private int _maxBlock = 5;
        [Tooltip("Minimum number of empty beats between blocks")]
        [Range(1, 20)] [SerializeField] private int _minInterval = 1;
        [Tooltip("Maximum number of empty beats between blocks")]
        [Range(1, 20)] [SerializeField] private int _maxInterval = 2;
        [Tooltip("Number of beats blocks")]
        [Range(1, 20)] [SerializeField] private int _blocks = 10;

        public void RandomizeBeats()
        {
            Beats = new List<int>();

            for (int i = 0; i < _preRoll; i++)
            {
                Beats.Add(-1);
            }

            for (int block = 0; block < _blocks; block++)
            {
                int blockLength = Random.Range(_minBlock, _maxBlock + 1);
                for (int i = 0; i < blockLength; i++)
                {
                    int beat = Random.Range(0, Inputs);
                    Beats.Add(beat);
                }

                if (block == _blocks - 1)
                {
                    break;
                }

                int intervalLength = Random.Range(_minInterval, _maxInterval + 1);
                for (int i = 0; i < intervalLength; i++)
                {
                    Beats.Add(-1);
                }
            }
        }
    }
}
