public enum Sound
{
   Arrow = 0,
   ArrowHit = 1,
   EnemyDead = 2,
   EnemyReached = 3,
   PlayerWin = 4,
   PlayerLose = 5,   
   Fireball = 6
}
public static class SoundExtensions
{
    public static void Play(this Sound sound)
    {
        SoundPlayer.Instance.Play(sound);
    }
}