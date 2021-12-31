using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ModelViewController
{
    [CustomEditor(typeof(Track))]
    public class TrackEditor : Editor
    {
        private Track _track;
        private Vector2 _position;
        private bool _displayBeatsData;

        public void OnEnable()
        {
            _track = (Track)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_track.Beats == null)
            {
                return;
            }

            if (_track.Beats.Count == 0)
            {
                EditorGUILayout.HelpBox("Empty Track", MessageType.Info);

                if (GUILayout.Button("Generate Random Track", EditorStyles.miniButton) == true)
                {
                    _track.RandomizeBeats();
                }
            }
            else
            {
                if (GUILayout.Button("Update Random Track", EditorStyles.miniButton) == true)
                {
                    _track.RandomizeBeats();
                }

                _displayBeatsData = EditorGUILayout.Foldout(_displayBeatsData, "Display Beats");

                if (_displayBeatsData == true)
                {
                    _position = EditorGUILayout.BeginScrollView(_position);

                    for (int i = 0; i < _track.Beats.Count; i++)
                    {
                        _track.Beats[i] = EditorGUILayout.IntSlider(_track.Beats[i], -1, Track.Inputs - 1);
                    }

                    EditorGUILayout.EndScrollView();
                }
            }
            EditorUtility.SetDirty(_track);
        }
    }
}
