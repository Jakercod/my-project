namespace GameV10.Heuristics
{
    public interface ICalculateHeuristic
    {
        int Calculate(Position source, Position destination);
    }
}