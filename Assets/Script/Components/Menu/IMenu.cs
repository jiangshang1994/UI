using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Script.Components.Menu
{
    public interface IMenu
    {
        string GetName();
        void AddChild(IMenu menu);
        void RemoveChild(IMenu menu);
        void AddEvent(EventTriggerType eventTrigger, EventTrigger.Entry entry);
        void RemoveEvent(EventTriggerType eventTrigger, EventTrigger.Entry entry);
        void RemoveAllChild();
        void RemoveAllEvent();
    }
}
