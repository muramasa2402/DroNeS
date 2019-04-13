﻿using UnityEngine;

namespace Drones.UI
{
    public class AddFoldable : FoldableMenu
    {
        protected override void Start()
        {
            Buttons[0].onClick.AddListener(MakeHub);
            Buttons[1].onClick.AddListener(MakeNFZ);
            base.Start();
        }

        void MakeNFZ()
        {
            NoFlyZone nfz = (NoFlyZone)ObjectPool.Get(typeof(NoFlyZone));
            var pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            var pos2 = nfz.transform.position;
            pos = Selectable.Cam.ScreenToWorldPoint(pos);
            pos2.x = pos.x;
            pos2.z = pos.z;
            nfz.transform.position = pos2;
        }

        void MakeHub()
        {
            Hub hub = (Hub)ObjectPool.Get(typeof(Hub));
            var pos = new Vector3(Screen.width/2, Screen.height/2, 0);
            var pos2 = hub.transform.position;
            pos = Selectable.Cam.ScreenToWorldPoint(pos);
            pos2.x = pos.x;
            pos2.z = pos.z;
            hub.transform.position = pos2;
        }
    }
}