namespace TicTacToeShared
{
    public enum RulesModes
    {
        /// <summary>
        /// default rule as used in competition. 
        /// when a smallboad is won, the other player cannot win it also.
        /// a move to an already won board is illegal.
        /// </summary>
        SingleWinSmallBoard, 

        /// <summary>
        /// when a smallboad is won, the other player could still win it also.
        /// </summary>
        MultiWinSmallBoard 
    }
}
