namespace UI.Assets.Panel
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    public abstract class Panel : UIBehaviour
    {
        private string id;

        protected Panel()
        {
            this.useGUILayout = true;
            this.enabled = true;
        }
    }
}