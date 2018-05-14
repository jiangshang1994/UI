namespace UI.Assets.Manage
{
    using System;
    using System.Collections;
    using Assets.Panel;
    using System.Collections.Generic;

    public class UIManager
    {
        private List<Panel> panelList = new List<Panel>();

        private static UIManager instance;
        public static UIManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UIManager();
                }
                return instance;
            }
        }

        private UIManager() { }
    }
}