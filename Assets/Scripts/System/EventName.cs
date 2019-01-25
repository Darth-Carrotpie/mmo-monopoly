using System.Collections.Generic;
using System.Linq;

public class EventName
{
    public class UI
    {
        //Announce,
        //ShowInfoDetails,
        //LanguageChanged,
        //InsufficientFunds,
        //OptionUpdate,
        public static string None() { return null; }
        public static string UpdateTalentTree() { return "UpdateTalentTree"; }
        public static string UpdateTargetMarks() { return "UpdateTargetMarks"; }
        public static List<string> Get() { return new List<string> { None(), UpdateTalentTree(), UpdateTargetMarks() }; }
    }
    public class Editor
    {
        public static string Redraw() { return "Redraw"; }
        public static string None() { return null; }
        public static List<string> Get() { return new List<string> { Redraw(), None() }; }
    }
    public class Input
    {
        public static string StartGame() { return "StartGame"; }
        public static string CellSelected() { return "CellSelected"; }
        public static string BuildingSelected() { return "BuildingSelected"; }
        public static string BuildingMoved() { return "BuildingMoved"; }
        public static string BuildLocationSettled() { return "BuildLocationSettled"; }
        public static string Build() { return "Build"; }
        public static string BuildMenuOpen() { return "BuildMenuOpen"; }
        public static string BuildMenuClose() { return "BuildMenuClose"; }
        public static string BuildMenuPostClose() { return "BuildMenuPostClose"; }
        public static string SelectableSelected() { return "SelectableSelected"; }
        public static string SelectableDeselected() { return "SelectableDeselected"; }
        //VolumetricDragable:
        public static string BeginDrag() { return "BeginDrag"; }
        public static string Drag() { return "Drag"; }
        public static string EndDrag() { return "EndDrag"; }
        public static string ControllerLocation() { return "ControllerLocation"; }
        //public static string Scroll() { return "Scroll"; } //later remove these. THese basic inputs have to be object dependend, not global systemic
        public static string FactionSelected() { return "FactionSelected"; }
        public static string Back() { return "Back"; }
        public class Gem
        {
            public static string AttachedToTree() { return "AttachedToTree"; }
            public static string DettachedFromTree() { return "DettachedFromTree"; }
            public static string Save() { return "Save"; }
            public static List<string> Get() { return new List<string> { AttachedToTree(), DettachedFromTree(), Save() }; }
        }
        public class TalentUpgrade
        {
            public static string Range() { return "Range"; }
            public static string Damage() { return "Damage"; }
            public static string Speed() { return "Speed"; }
            public static string Hitpoints() { return "Hitpoints"; }
            public static List<string> Get() { return new List<string> { Range(), Damage(), Speed(), Hitpoints() }; }
        }
        public static List<string> Get() { return new List<string> {
            StartGame(),
            CellSelected(),
            BuildingSelected(),
            BuildingMoved(),
            BuildLocationSettled(),
            Build(), BuildMenuOpen(), BuildMenuClose(),BuildMenuPostClose(),
            SelectableSelected(), SelectableDeselected(),
            BeginDrag(),Drag(),EndDrag(),
            ControllerLocation(),
            FactionSelected(),
            Back()}.Concat(TalentUpgrade.Get()).Concat(Gem.Get()).ToList(); }
    }
    public class System
    {

        //Turn,
        //Upkeep,
        //Produce,
        //Build,
        //Victory,
        //Defeat,

        public static string Destroyed() { return "Destroyed"; }
        public static string GameModeChanged() { return "GameModeChanged"; }
        public static string NextScene() { return "NextScene"; }
        public static string LoadScene() { return "LoadScene"; }
        public static List<string> Get() { return new List<string> { GameModeChanged(), Destroyed(), NextScene(), LoadScene() }; }
    }
    public class AI
    {
        //Building,
        //Platform,
        public static string None() { return null; }
        public static List<string> Get() { return new List<string> { None() }; }
    }
    public List<string> Get()
    {
        return new List<string> { }.Concat(UI.Get())
                                    .Concat(Editor.Get())
                                    .Concat(Input.Get())
                                    .Concat(System.Get())
                                    .Concat(AI.Get())
                                    .Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
    }
}