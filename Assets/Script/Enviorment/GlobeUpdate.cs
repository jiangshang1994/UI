using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Enviorment
{
    public class GlobeUpdate : MonoBehaviour
    {
        public Action OnUpdate;

        private void Update()
        {
            if (OnUpdate != null)
            {
                OnUpdate();
            }
        }


    }
}
