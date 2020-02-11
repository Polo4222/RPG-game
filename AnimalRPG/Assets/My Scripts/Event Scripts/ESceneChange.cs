using System;

public class ESceneChange 
{
    #region singleton
    private static ESceneChange _instance;
    private static readonly object padlock = new object();

    public static ESceneChange Instance
    {
        get
        {
            lock(padlock)
            {
                if(_instance == null)
                {
                    _instance = new ESceneChange();
                }
                return _instance;
            }
        }
    }

    #endregion

    public delegate void ChangedSceneToCombatEventHandler(int i);
    public event ChangedSceneToCombatEventHandler ESetUpScene;

    public void EventTriggerToCombatScene()
    {
        ESetUpScene(0);
    }
    
    public void EventTriggerToNonCombatScene()
    {
        ESetUpScene(1);
    }
}
