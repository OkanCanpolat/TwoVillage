using System;
using System.Collections.Generic;
using UnityEngine.Events;

public static class UnityEventConverter
{
    public static List<UnityAction> ConvertEventToAction(UnityEvent uevent)
    {
        int actionCount = uevent.GetPersistentEventCount();
        List<UnityAction> result = new List<UnityAction>();
        
        for (int i = 0; i < actionCount; i++)
        {
            UnityEngine.Object target = uevent.GetPersistentTarget(i);
            string methodName = uevent.GetPersistentMethodName(i);
            Type targetType = target.GetType();
            UnityAction ac = (UnityAction)Delegate.CreateDelegate(typeof(UnityAction), target, methodName);
            result.Add(ac);
        }
        return result;
    }
}






