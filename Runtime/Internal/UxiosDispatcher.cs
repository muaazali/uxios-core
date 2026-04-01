using UnityEngine;

namespace Uxios.Core.Internal
{
    public class UxiosDispatcher : MonoBehaviour
    {
        private static UxiosDispatcher _instance;
        public static UxiosDispatcher Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("__UxiosDispatcher");
                    DontDestroyOnLoad(go);
                    go.hideFlags = HideFlags.HideAndDontSave;
                    _instance = go.AddComponent<UxiosDispatcher>();
                }
                return _instance;
            }
        }

        public void RunCoroutine(System.Collections.IEnumerator routine)
        {
            StartCoroutine(routine);
        }
    }
}