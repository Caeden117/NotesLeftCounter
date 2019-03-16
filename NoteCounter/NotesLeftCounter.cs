using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace NotesLeftCounter
{
    public class NotesLeftCounter : MonoBehaviour
    {
        public int _noteCount { get; private set; }
        private TextMeshProUGUI _counter;

       
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
         //   gameObject.transform.position = new Vector3(-0.1f, 3.5f, 8f);
            Canvas canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            CanvasScaler cs = gameObject.AddComponent<CanvasScaler>();
            cs.scaleFactor = 10.0f;
            cs.dynamicPixelsPerUnit = 10f;
            GraphicRaycaster gr = gameObject.AddComponent<GraphicRaycaster>();
            gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
            gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);

            _counter = CustomUI.BeatSaber.BeatSaberUI.CreateText(canvas.transform as RectTransform, "Notes Remaining  " +
                Plugin.LevelData.GameplayCoreSceneSetupData.difficultyBeatmap.beatmapData.notesCount.ToString(), new Vector2(0, 0));
            _counter.alignment = TextAlignmentOptions.Center;
            _counter.transform.localScale *= .12f;
            _counter.fontSize = 2.5f;
            _counter.color = Color.white;
            _counter.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
            _counter.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);
            _counter.enableWordWrapping = false;
            _counter.transform.localPosition = new Vector3(-0.1f, 3.5f, 8f);
            _noteCount = Plugin.LevelData.GameplayCoreSceneSetupData.difficultyBeatmap.beatmapData.notesCount;
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
