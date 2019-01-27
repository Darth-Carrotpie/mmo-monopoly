using System.Collections.Generic;
using System.Linq;

public class EventName
{
    public class UI
    {
        public static string UpdPlayerCount() { return "UpdatePlayerCount"; }
        public static string UpdLeaderboard() { return "UpdateTargetMarks"; }
        public static string UpdWealth() { return "UpdWealth"; }
        public static string UpdDistance() { return "UpdDistance"; }
        public static List<string> Get() { return new List<string> { UpdPlayerCount(), UpdLeaderboard(), UpdWealth(), UpdDistance() }; }
    }
    public class Player
    {
        public static string NewPosition() { return "NewPosition"; }
        public static string SetMainPlayer() { return "SetMainPlayer"; }
        public static string SetMainPlSceneRef() { return "SetMainPlSceneRef"; }
        public static string PossibleAction() { return "PossibleAction"; }
        public static List<string> Get() { return new List<string> { NewPosition(), SetMainPlayer(), SetMainPlSceneRef(), PossibleAction() }; }
    }
    public class Input
    {
        public static string StartGame() { return "StartGame"; }
        public static string BuildHouse() { return "BuildHouse"; }
        public static string BuildHotel() { return "BuildHotel"; }
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
            BuildHouse(),
            BuildHotel(),
            Back()}.Concat(TalentUpgrade.Get()).Concat(Gem.Get()).ToList(); }
    }
    public class System
    {
        public static string UpdateBoard() { return "UpdateBoard"; }
        public static string MoveBoard() { return "MoveBoard"; }
        public static string TilesDownloaded() { return "TilesDownloaded"; }
        public static string SpawnPlayers() { return "SpawnPlayers"; }
        public static string Turn() { return "Turn"; }
        public static List<string> Get() { return new List<string> { TilesDownloaded(), UpdateBoard(),MoveBoard(), SpawnPlayers(), Turn() }; }
    }

    public List<string> Get()
    {
        return new List<string> { }.Concat(UI.Get())
                                    .Concat(Player.Get())
                                    .Concat(Input.Get())
                                    .Concat(System.Get())
                                    .Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
    }
}