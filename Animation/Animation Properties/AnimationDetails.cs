namespace GameV10.Animation
{
    public class AnimationDetails
    {
        public enum Stance
        {
            idle1,
            idle2,
            walking,
            running,
            jumping,
            runningjump,
            sliding,
            impact,
            die1,
            die2,
            block1,
            block2,
            attack,
            comboattack,
            jumpattack,
            jumpspinattack,
            spinattack,
            kick,
            knock,
            charging,
            chargeattack,
            powerup
        }
        public Stance state;
        public int colpos { get; set; }
        //number changes depending on animation
        public int numcol { get; set; }
        //When counter reaches the interval it runs the animation (higher values portray a slower animation)
        public int interval { get; set; }
        public int rowpos { get; set; }
        protected int counter { get; set; }
    }
}
