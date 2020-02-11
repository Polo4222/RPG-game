using System;

public class EUISetup
{
    #region singleton
    private static readonly object padlock = new object();
    private static EUISetup _instance;
    public static EUISetup Instance {
        get
        {
            lock(padlock)
                {
                if(_instance == null)
                {
                    _instance = new EUISetup();
                }
                return _instance;
            }
        }
    }


    #endregion

    public event Action UIHasBeenSetup;
    public event Action CombatUIHasBeenSetup;

    public void OnCombatUISetup()
    {
        CombatUIHasBeenSetup();
    }

    public void OnUISetup()
    {
        UIHasBeenSetup();
    }
}
