using System.Text;

namespace GameV10.World
{
    internal class CreateGridTiles
    {
        public string ConvertVectorsToString(Dictionary<Vector2, int> vectors, int width, int height)
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (vectors.ContainsKey(new Vector2(x, y)))
                    {
                        sb.Append('X');
                    }
                    else
                    {
                        sb.Append('1');
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

    }
}
