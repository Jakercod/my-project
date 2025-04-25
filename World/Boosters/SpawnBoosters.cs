namespace GameV10.World.Boosters
{
    internal class SpawnBoosters
    {
        List<HealthBooster> healthBoosters = new();
        Texture2D HealthBoosterTexture;
        int spawnTimer = 0;
        public SpawnBoosters(Texture2D healthboostertexture)
        {
            HealthBoosterTexture = healthboostertexture;
        }
        public void Update(Game1 game1)
        {
            Random rnd = new();
            if (healthBoosters.Count < 3)
            {
                if(spawnTimer > 1000)
                {
                    healthBoosters.Add(new HealthBooster(HealthBoosterTexture, new Vector2(rnd.Next(0, 800), rnd.Next(0, 800)), 500));
                    spawnTimer = 0;
                }
                spawnTimer++;
            }
            foreach (HealthBooster healthBooster in healthBoosters)
            {
                healthBooster.Update(game1);
            }
            for(int i = 0; i < healthBoosters.Count; i++)
            {
                if (healthBoosters[i].IsRemoved)
                {
                    healthBoosters.RemoveAt(i);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (HealthBooster healthBooster in healthBoosters)
            {
                healthBooster.Draw(spriteBatch);
            }
        }
    }
}
