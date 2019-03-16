using IllusionPlugin;
using UnityEngine.SceneManagement;
using System;
using CustomUI;
using IllusionInjector;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CountersPlus.Custom;
namespace NotesLeftCounter
{
    public class Plugin : IPlugin
    {
        public string Name => "NotesLeftCounter";
        public string Version => "1.1.0";

        private BeatmapObjectSpawnController _spawnController;
        public static BS_Utils.Gameplay.LevelData LevelData { get; private set; }
        private NotesLeftCounter _notesLeftCounter;
        public static bool CountersPlusInstalled { get; private set; } = false;

        public void OnApplicationStart()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            NotesLeftCounterUI.ReadPrefs();

            if (PluginManager.Plugins.Any(x => x.Name == "Counters+"))
            {
                CountersPlusInstalled = true;
                AddCustomCounter();
            }

        }

        private void SceneManagerOnActiveSceneChanged(Scene oldScene, Scene newScene)
        {
            NotesLeftCounterUI.ReadPrefs();
            if (newScene.name == "GameCore")
            {
                if (_spawnController == null)
                    _spawnController = Resources.FindObjectsOfTypeAll<BeatmapObjectSpawnController>().FirstOrDefault();
                if (_spawnController == null) return;
                _spawnController.noteWasCutEvent -= _spawnController_noteWasCutEvent;
                _spawnController.noteWasMissedEvent -= _spawnController_noteWasMissedEvent;

                if (LevelData == null)
                    LevelData = BS_Utils.Plugin.LevelData;
                if (LevelData == null) return;


                if (NotesLeftCounterUI.ShowNoteCounter && LevelData.GameplayCoreSceneSetupData.practiceSettings == null)
                {
                    Log("Attempting to Create Note Counter");
                    _spawnController.noteWasCutEvent += _spawnController_noteWasCutEvent;
                    _spawnController.noteWasMissedEvent += _spawnController_noteWasMissedEvent;

                    _notesLeftCounter = new GameObject("NotesLeftCounter").AddComponent<NotesLeftCounter>();
                }

            }


        }

        private void _spawnController_noteWasMissedEvent(BeatmapObjectSpawnController arg1, NoteController noteController)
        {
            NoteData note = noteController.noteData;
            if(note.noteType != NoteType.Bomb && _notesLeftCounter != null)
                if(_notesLeftCounter != null)
                _notesLeftCounter.UpdateCounter();
        }

        private void _spawnController_noteWasCutEvent(BeatmapObjectSpawnController arg1, NoteController noteController, NoteCutInfo arg3)
        {
            NoteData note = noteController.noteData;
            if (note.noteType != NoteType.Bomb && _notesLeftCounter != null)
                if (_notesLeftCounter != null)
                    _notesLeftCounter.UpdateCounter();
        }


        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            if (scene.name == "MenuCore")
                NotesLeftCounterUI.CreateUI();

        }

        public void OnApplicationQuit()
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        public void OnLevelWasLoaded(int level)
        {

        }

        public void OnLevelWasInitialized(int level)
        {
        }

        public void OnUpdate()
        {
        }

        public void OnFixedUpdate()
        {
        }

        void AddCustomCounter()
        {
            Log("Creating Custom Counter");
            CustomCounter counter = new CustomCounter
            {
                JSONName = "notesLeftCounter", //Name in config system. Also used as an identifier. Don't plan on changing this.
                Name = "Notes Left", //Display name that will appear in the SettingsUI.
                Mod = this, //IPA Plugin. Will show up in Credits in the SettingsUI.
                Counter = "NotesLeftCounter", //Name of the GameObject that holds your Counter component. Used to hook into the Counters+ system.
            };
            CountersPlus.Config.ICounterPositions[] positions = new CountersPlus.Config.ICounterPositions[6] { CountersPlus.Config.ICounterPositions.AboveCombo, CountersPlus.Config.ICounterPositions.AboveHighway, CountersPlus.Config.ICounterPositions.AboveMultiplier, CountersPlus.Config.ICounterPositions.BelowCombo, CountersPlus.Config.ICounterPositions.BelowEnergy, CountersPlus.Config.ICounterPositions.BelowMultiplier } ;
            CustomCounterCreator.CreateCustomCounter(counter, positions);
        }


        public static void Log(string message)
        {
            Console.WriteLine("[{0}] {1}", "NoteCounter", message);
        }




    }
}
