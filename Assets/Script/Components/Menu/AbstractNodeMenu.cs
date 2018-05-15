using System;
using UICommon.Collection;
using UnityEngine.EventSystems;

namespace Assets.Script.Components.Menu
{
    /// <summary>
    /// 没有子节点会被禁用，且无单击事件，hover自动显示子节点
    /// </summary>
    public abstract class AbstractNodeMenu:UIBehaviour,IMenu
    {
        private string menuText;
        private ObservebleValues<IMenu> children;

        protected AbstractNodeMenu()
        {
            DisableClickEvent();
        }

        protected override void Start()
        {
            base.Start();
            menuText = "菜单（未命名）";
            children = ObservebleValues<IMenu>.ListValue;
            if (GetComponent<EventTrigger>() == null)
            {
                gameObject.AddComponent<EventTrigger>();
            }
        }

        public void AddChild(IMenu menu)
        {
            if (children.Contains(menu))
            {
                throw new Exception("菜单项已存在！");
            }
            children.Add(menu);
        }

        public void AddEvent(EventTriggerType eventTrigger, EventTrigger.Entry entry)
        {
            EventTrigger trigger = GetComponent<EventTrigger>();
            if (!trigger.triggers.Contains(entry))
            {
                trigger.triggers.Add(entry);
                return;
            }
            throw new Exception("移除失败");
        }

        public string GetName()
        {
            return this.name;
        }

        public void RemoveAllChild()
        {
            if(this.children.Count > 0)
               this.children.Clear();
        }

        public void RemoveAllEvent()
        {
            EventTrigger trigger = GetComponent<EventTrigger>();
            trigger.triggers.Clear();
        }

        public void RemoveChild(IMenu menu)
        {
            if (children.Contains(menu))
            {
                children.Remove(menu);
                return;
            }
            throw new Exception("菜单项不存在！");
        }

        public void RemoveEvent(EventTriggerType eventTrigger, EventTrigger.Entry entry)
        {
            EventTrigger trigger = GetComponent<EventTrigger>();
            if (trigger.triggers.Contains(entry))
            {
                trigger.triggers.Remove(entry);
                return;
            }
            throw new Exception("移除失败");
        }

        /// <summary>
        /// 单击事件拦截
        /// </summary>
        public void DisableClickEvent()
        {
            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) => {  });
            trigger.triggers.Add(entry);
        }
    }
}
