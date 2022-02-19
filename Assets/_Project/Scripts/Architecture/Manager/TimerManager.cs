
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SirGames.Showcase.Managers
{
    public class TimerManager : MonoBehaviour
    {
        private List<Timer> _activeTimers = new List<Timer>();
        public void Register(float duration, Action timerEnd)
        {
            _activeTimers.Add(new Timer(duration, timerEnd));
        }

        void Update()
        {
            foreach (var timer in _activeTimers)
            {
                timer.Update(Time.deltaTime);
            }
                        
            _activeTimers.RemoveAll(x=> x.IsFinished);
            
        }

        private class Timer
        {
            public bool IsFinished { get; private set; }
            private float _duration;
            private event Action _onTimerEnd;

            public Timer(float duration, Action timerEnd)
            {
                _duration = duration;
                _onTimerEnd = timerEnd;
                IsFinished = false;
            }

            public void Update(float elapsedTime)
            {
                _duration -= elapsedTime;
                if (_duration <= 0)
                {
                    _onTimerEnd?.Invoke();
                    IsFinished = true;
                }
            }
        }
    }
}