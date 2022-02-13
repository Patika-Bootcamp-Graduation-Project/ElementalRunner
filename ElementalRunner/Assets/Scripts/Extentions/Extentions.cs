using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Olcay
{
    public static class Extentions
    {
        //this is for dont look at camera.main always for culling mask thats because of this is doin str query at hierarchy 
        private static Camera _camera;
        public static Camera Camera
        {
            get
            {
                if (_camera == null) _camera = Camera.main;
                return _camera;
            }
        }
        //this is for using garbage collector much less than when we are using waitforsecond every time
        private static readonly Dictionary<float, WaitForSeconds> WaitDictionary =
            new Dictionary<float, WaitForSeconds>();
        //this is looking for dictionary if is there alreay any waitforsecond with same time this is using that waitforsecond not a new one
        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionary.TryGetValue(time, out var wait)) return wait;

            WaitDictionary[time] = new WaitForSeconds(time);
            return WaitDictionary[time];
        }

        //if we are hitting ui our ray will be detach more den 0 
        private static PointerEventData eventDataCurrentPosition;
        private static List<RaycastResult> results;

        public static bool IsOverUi()
        {
            eventDataCurrentPosition = new PointerEventData(EventSystem.current) {position = Input.mousePosition};
            results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition,results);
            return results.Count > 0;
        }


    }
}
