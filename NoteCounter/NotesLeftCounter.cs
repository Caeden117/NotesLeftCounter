using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace NotesLeftCounter
{
    public class NotesLeftCounter : MonoBehaviour
    {
        public int _noteCount { get; private set; }
        private TextMeshPro _counter;

       
        private void Awake()
        {
            try
            {
                Plugin.Log("Attempting to Initialize Note Counter");
                Init();
            }
            catch ( Exception ex)
            {
                Plugin.Log("Note Counter Done screwed up on initialization");
                Plugin.Log(ex.ToString());
            }
        }

        void Init()
        {
            _counter = gameObject.AddComponent<TextMeshPro>();
            _counter.text = "Notes Remaining  " + Plugin.LevelData.difficultyBeatmap.beatmapData.notesCount.ToString();
            _counter.fontSize = 3;
            _counter.color = Color.white;
            _counter.alignment = TextAlignmentOptions.Center;
            _counter.font = Resources.Load<TMP_FontAsset>("Teko-Medium SDF No Glow");
            _counter.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
            _counter.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);
            _counter.overflowMode = TextOverflowModes.Overflow;
            _counter.enableWordWrapping = false;
            _counter.rectTransform.localPosition = new Vector3(-0.1f, 3.5f, 8f);

            _noteCount = Plugin.LevelData.difficultyBeatmap.beatmapData.notesCount;
        }

        public void UpdateCounter()
        {
            if (_noteCount > 0)
                _noteCount--;
            if(_counter != null)
            _counter.text = "Notes Remaining  " + _noteCount.ToString();
        }
    }
}
