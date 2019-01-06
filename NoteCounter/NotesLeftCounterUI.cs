using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomUI.GameplaySettings;
using IllusionPlugin;
namespace NotesLeftCounter
{
    class NotesLeftCounterUI
    {
        public static bool ShowNoteCounter { get; private set; } = false;


        public static void CreateUI()
        {

            var toggleOption = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.ModifiersLeft, "Show Notes Left Counter", "MainMenu", "Shows the amount of notes left in the song as you progress");
            toggleOption.GetValue = ModPrefs.GetBool("NotesLeftCounter", "showNoteCounter", true, true);
            toggleOption.OnToggle += (value) => { ShowNoteCounter = value; ModPrefs.SetBool("NotesLeftCounter", "showNoteCounter", value); };

        }


        public static void ReadPrefs()
        {
            ShowNoteCounter = ModPrefs.GetBool("NotesLeftCounter", "showNoteCounter", true, true);
        }


    }
}
