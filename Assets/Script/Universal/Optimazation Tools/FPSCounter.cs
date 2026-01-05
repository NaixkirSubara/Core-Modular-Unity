using UnityEngine;

namespace MyStudio.Core.Optimization
{
    public class FPSCounter : MonoBehaviour
    {
        private float _deltaTime = 0.0f;
        
        [Header("Settings")]
        [SerializeField] private Color _textColor = Color.white;
        [SerializeField] private int _fontSize = 25;

        void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        }

        void OnGUI()
        {
            int w = Screen.width, h = Screen.height;
            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(20, 20, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / _fontSize;
            style.normal.textColor = _textColor;
            
            float msec = _deltaTime * 1000.0f;
            float fps = 1.0f / _deltaTime;
            
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
    }
}