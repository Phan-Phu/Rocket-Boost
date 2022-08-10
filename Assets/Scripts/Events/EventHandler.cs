using System;

public static class EventHandler
{
    public static event Action BeforeSceneUnloadFadeOutEvent;
    public static void CallBeforeSceneUnloadFadeOutEvent()
    {
        if (BeforeSceneUnloadFadeOutEvent != null)
        {
            BeforeSceneUnloadFadeOutEvent();
        }
    }

    public static event Action AfterSceneLoadFadeInEvent;
    public static void CallAfterSceneLoadFadeInEvent()
    {
        if (AfterSceneLoadFadeInEvent != null)
        {
            AfterSceneLoadFadeInEvent();
        }
    }

    public static event Action SaveGameEvent;
    public static void CallSaveGameEvent()
    {
        if (SaveGameEvent != null)
        {
            SaveGameEvent();
        }
    }

    public static event Action LoadGameEvent;
    public static void CallLoadGameEvent()
    {
        if (LoadGameEvent != null)
        {
            LoadGameEvent();
        }
    }
}
