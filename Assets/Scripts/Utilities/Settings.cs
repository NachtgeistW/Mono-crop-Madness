public class Settings
{
    public const float itemFadeDuration = 0.35f;
    public const float fadeAlpha = 0.35f;

    //Time
    public const float hourThreshold = 2f; //Smaller the threshold, faster the time
    public const int hourHold = 23; //critical value
    public const int dayHold = 10; //��ʱ��Ϊ10��Ϊ1����
    public const int monthHold = 6; //��ʱ��Ϊ6����Ϊ1��

    //Transition
    public const float sceneFadeDuration = 1;

    //Healthy Score
    public const int GrassRecoveryScore = 1;
    public const int BushRecoveryScore = 2;
    public const int TreeRecoveryScore = 3;
    public const int DailyReduceScore = 2;
}
