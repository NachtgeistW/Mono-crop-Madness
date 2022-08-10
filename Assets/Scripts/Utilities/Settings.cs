public class Settings
{
    public const float itemFadeDuration = 0.35f;
    public const float fadeAlpha = 0.35f;

    //Time
    public const float hourThreshold = 2f; //Smaller the threshold, faster the time
    public const int hourHold = 23; //critical value
    public const int dayHold = 10; //暂时设为10天为1个月
    public const int monthHold = 6; //暂时设为6个月为1年

    //Transition
    public const float sceneFadeDuration = 1;

    //Healthy Score
    public const int GrassRecoveryScore = 1;
    public const int BushRecoveryScore = 2;
    public const int TreeRecoveryScore = 3;
    public const int DailyReduceScore = 2;
}
