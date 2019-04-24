﻿using System.Collections;
using UnityEngine;

namespace Drones.Utils
{
    public class AudioSensor : MonoBehaviour
    {
        private bool _Active = false;

        private Drone _Drone;

        private int _inRadius;

        private TimeKeeper.Chronos _Time;

        private readonly WaitForSeconds _Wait = new WaitForSeconds(1 / 10f);

        private void OnDisable()
        {
            StopCoroutine(StartTimer());
        }

        public Drone AssignedDrone
        {
            get
            {
                if (_Drone == null)
                {
                    _Drone = transform.parent.GetComponent<Drone>();
                }
                return _Drone;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 11 || other.gameObject.layer == 12 || other.gameObject.layer == 13)
            {
                _inRadius++;
                if (!_Active) StartCoroutine(StartTimer());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 11 || other.gameObject.layer == 12 || other.gameObject.layer == 13)
            {
                _inRadius--;
                if (_inRadius <= 0)
                {
                    _inRadius = 0;
                    _Active = false;
                }
            }
        }

        private IEnumerator StartTimer()
        {
            _Active = true;
            if (_Time == null) _Time = TimeKeeper.Chronos.Get();
            while (_Active)
            {
                _Time.Now();
                yield return _Wait;
                float dt = _Time.Timer();
                AssignedDrone.UpdateAudible(dt);
                SimManager.UpdateAudible(dt);
            }
            yield break;

        }

        public void SetSensorRadius(float radius)
        {
            transform.localScale = transform.worldToLocalMatrix * Vector3.one * radius;
        }
    }
}

