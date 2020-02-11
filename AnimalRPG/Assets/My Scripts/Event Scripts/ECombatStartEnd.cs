using System;

public class ECombatStartEnd 
{
    #region singleton
    private static ECombatStartEnd _instance;
    private static readonly object padlock = new object();
    public static ECombatStartEnd Instance
    {
        get
        {
            lock (padlock){
                if (_instance == null)
                {
                    _instance = new ECombatStartEnd();
                }
                return _instance;
            }
            
        }
    }
    #endregion

  
    public event Action ECombatStarted;

    public void EventTrigger()
    {
        ECombatStarted();
    }
}
