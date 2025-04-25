namespace GameV10.Animation
{
    public class Animations 
    {
        public Dictionary<string, AnimationDetails> animations { get; private set; }
        public Animations()
        {
            animations = new Dictionary<string, AnimationDetails>
            {
                //Animations are stored as dictionaries 
                //they contain a string by which their data can be accessed
                //onced opened it sets the current animation settings to that which will work with the set animation
                //for example state sets the enum which can provide row and priority, numcol sets the amount of frames in the cycle and interval sets the time between frames
                { "idle1", new AnimationDetails {state = AnimationDetails.Stance.idle1, numcol = 17, interval = 7} },
                { "idle2", new AnimationDetails {state = AnimationDetails.Stance.idle2,numcol = 25, interval = 7} },
                { "walking", new AnimationDetails {state = AnimationDetails.Stance.walking,numcol = 11, interval = 6} },
                { "running", new AnimationDetails {state = AnimationDetails.Stance.running,numcol = 8, interval = 5} },
                { "jumping", new AnimationDetails {state = AnimationDetails.Stance.jumping, numcol = 9, interval = 4} },
                { "runningjump", new AnimationDetails {state = AnimationDetails.Stance.runningjump, numcol = 8, interval = 4} },
                { "sliding", new AnimationDetails {state = AnimationDetails.Stance.sliding, numcol = 15, interval = 5} },
                { "impact", new AnimationDetails {state = AnimationDetails.Stance.impact, numcol = 9, interval = 6} },
                { "die1", new AnimationDetails {state = AnimationDetails.Stance.die1, numcol = 19, interval = 6} },
                { "die2", new AnimationDetails {state = AnimationDetails.Stance.die2, numcol = 20, interval = 6} },
                { "block1", new AnimationDetails {state = AnimationDetails.Stance.block1, numcol = 5, interval = 3} },
                { "block2", new AnimationDetails {state = AnimationDetails.Stance.block2, numcol = 0, interval = 3} },
                { "attack", new AnimationDetails {state = AnimationDetails.Stance.attack, numcol = 15, interval = 4} },
                { "comboattack", new AnimationDetails {state = AnimationDetails.Stance.comboattack, numcol = 35, interval = 3} },
                { "jumpattack", new AnimationDetails {state = AnimationDetails.Stance.jumpattack, numcol = 23, interval = 4} },
                { "jumpspinattack", new AnimationDetails {state = AnimationDetails.Stance.jumpspinattack, numcol = 24, interval = 4} },
                { "spinattack", new AnimationDetails {state = AnimationDetails.Stance.spinattack, numcol = 17, interval = 3} },
                { "kick", new AnimationDetails {state = AnimationDetails.Stance.kick, numcol = 12, interval = 4} },
                { "knock", new AnimationDetails {state = AnimationDetails.Stance.knock, numcol = 10, interval = 4} },
                { "charging", new AnimationDetails {state = AnimationDetails.Stance.charging, numcol = 30, interval = 6} },
                { "chargeattack", new AnimationDetails {state = AnimationDetails.Stance.chargeattack, numcol = 30, interval = 4} },
                { "powerup", new AnimationDetails {state = AnimationDetails.Stance.powerup, numcol = 0, interval = 10} },
               
            };
        }
    }
}
