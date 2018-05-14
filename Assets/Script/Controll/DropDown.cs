namespace MyUI.Assets.CheckBox
{
    using System;
    using UICommon.Collection;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>
    /// 1、下拉列表可以绑定数据对象
    /// 3、普通对象会封装成Text，
    /// </summary>
    /// <typeparam name="T">列表绑定类型</typeparam>
    public class DropDown<T> : Selectable, IPointerClickHandler
    {
        public Action<DropDown<T>> OnFocusAction;
        public Action<T> OnItemFocusAction;
        private Text text;
        private GUIStyle gUIStyle;
        private ObservebleValues<T> itemList;
        private T onFocusItem;

        public DropDown(String text, T[] items){ }

        public DropDown(T[] items) : this("", items)
        {
            itemList = ObservebleValues<T>.ListValue;
            itemList.Add(items);
        }
        
        private void SetItemsStype(GUISettings gUISettings)
        {
        }

        public override void Select()
        {
            base.Select();
            if (OnFocusAction != null)
            {
                OnFocusAction(this);
            }
        }

        private void AddItemsListener(T obj)
        {

        }

        public void OnItemFocus()
        {
            if (OnItemFocusAction != null && onFocusItem != null)
            {
                OnItemFocusAction(onFocusItem);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {

        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.EndVertical();
        }

        

        public ObservebleValues<T> ItemList
        {
            get
            {
                return itemList;
            }

            set
            {
                itemList = value;
            }
        }

        public Type getBeen()
        {
            return typeof(T);
        }
    }
}