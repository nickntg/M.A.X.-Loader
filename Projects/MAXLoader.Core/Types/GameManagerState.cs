using MAXLoader.Core.Types.Enums;

namespace MAXLoader.Core.Types
{
    public class GameManagerState
    {
        public Team ActiveTurnTeam { get; set; }
        public Team PlayerTeam { get; set; }
        public int TurnCounter { get; set; }
        public short GameState { get; set; }
        public short TurnTimer { get; set; }
        public int Effects { get; set; }
        public int ClickScroll { get; set; }
        public int QuickScroll { get; set; }
        public int FastMovement { get; set; }
        public int FollowUnit { get; set; }
        public int AutoSelect { get; set; }
        public int EnemyHalt { get; set; }
    }
}
