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
        public enum Trigger { Missed, Correct, Wrong }

        [SerializeField] private RectTransform _left;
        [SerializeField] private RectTransform _right;
        [SerializeField] private RectTransform _up;
        [SerializeField] private RectTransform _down;
        [SerializeField] private RectTransform _empty;
        private RectTransform _rectTransform;
        private List<Image> _beatViews;
        private Vector2 _position;
        private float _beatViewSize;
        private float _spacing;

        public float position
        {
            get { return _position.y; }
            set
            {
                if (value != _position.y)
                {
                    _position.y = value;
                    _rectTransform.anchoredPosition = _position;
                }
            }
        }


        public void Init(Track track)
        {
            _rectTransform = (RectTransform)transform;
            _position = _rectTransform.anchoredPosition;
            _beatViewSize = _empty.rect.height;
            _spacing = GetComponent<VerticalLayoutGroup>().spacing;
            _beatViews = new List<Image>();

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
                Image view = GameObject.Instantiate(beats, transform).GetComponent<Image>();
                view.transform.SetAsFirstSibling();
                _beatViews.Add(view);
            }
        }

        private void Start()
        {
            Init(GameplayController.Instance.Track);
        }

        private void Update()
        {
            position -= (_beatViewSize + _spacing) * Time.deltaTime * GameplayController.Instance.SecondsPerBeat;
        }

        public void TriggerBeatView(int index, Trigger trigger)
        {
            switch (trigger)
            {
                case Trigger.Missed:
                    _beatViews[index].color = Color.gray;
                    //Debug.Break();
                    break;
                case Trigger.Correct:
                    _beatViews[index].color = Color.yellow;
                    break;
                case Trigger.Wrong:
                    _beatViews[index].color = Color.cyan;
                    break;
            }
        }
    }
}
