using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModelViewController
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    [RequireComponent(typeof(ContentSizeFitter))]
    [RequireComponent(typeof(RectTransform))]
    public class TrackView : MonoBehaviour
    {
        [SerializeField] private Track _track;

        [SerializeField] private RectTransform _left;
        [SerializeField] private RectTransform _right;
        [SerializeField] private RectTransform _up;
        [SerializeField] private RectTransform _down;

        [SerializeField] private RectTransform _empty;

        private RectTransform _rectTransform;

        public void Init(Track track)
        {
            _rectTransform = (RectTransform)transform;
        }

        private void Start()
        {
            Init(_track);
        }
    }
}
