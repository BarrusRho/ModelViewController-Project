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

            foreach (int b in track.Beats)
            {
                GameObject beats;

                switch (b)
                {
                    case 0:
                        beats = _left.gameObject;
                        break;
                    case 1:
                        beats = _right.gameObject;
                        break;
                    case 2:
                        beats = _up.gameObject;
                        break;
                    case 3:
                        beats = _down.gameObject;
                        break;
                    default:
                        beats = _empty.gameObject;
                        break;
                }
                Transform view = GameObject.Instantiate(beats, transform).transform;
                view.SetAsFirstSibling();
            }
        }

        private void Start()
        {
            Init(_track);
        }
    }
}
