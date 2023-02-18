using System;
using Lean.Touch;
using UnityEngine;

namespace _Script.Input
{
    public class LeanTouchInputService : MonoBehaviour, IInputService
    {
        public Vector2 InputPosition { get; private set; }
        public bool IsHolding => isCurrentHold;
        public bool IsReleased => isCurrentReleased;

        private bool isCurrentHold;
        private bool isCurrentReleased;

        private void OnEnable()
        {
            LeanTouch.OnFingerDown += OnFingerDown;
            LeanTouch.OnFingerUpdate += OnFingerUpdate;
            LeanTouch.OnFingerUp += OnFingerReleased;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerTap -= OnFingerDown;
            LeanTouch.OnFingerUpdate -= OnFingerUpdate;
            LeanTouch.OnFingerUp -= OnFingerReleased;
        }

        private void OnFingerReleased(LeanFinger obj)
        {
            InputPosition = obj.ScreenPosition;
            isCurrentHold = false;
            isCurrentReleased = true;
        }

        private void OnFingerUpdate(LeanFinger obj)
        {
            InputPosition = obj.ScreenPosition;
        }

        private void OnFingerDown(LeanFinger obj)
        {
            isCurrentHold = true;
            isCurrentReleased = false;
        }
    }
}