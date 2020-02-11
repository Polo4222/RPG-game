using System;

public class EQuestConditionDone 
{
    #region singleton
    private static EQuestConditionDone _instance;
    private static readonly object padlock = new object();

    public static EQuestConditionDone Instance
    {
        get
        {
            lock(padlock)
            {
                if(_instance == null)
                {
                    _instance = new EQuestConditionDone();
                }
                return _instance;
            }
        }
    }
    #endregion

    public event Action EQuestConditionCheck;

    public void EventTrigger()
    {
        EQuestConditionCheck();
    }
}