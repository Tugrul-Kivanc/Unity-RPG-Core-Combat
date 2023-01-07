namespace RPG.Control
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Cursor", menuName = "Cursors/Create New Scriptable Cursor", order = 0)]
    public class CursorType : ScriptableObject
    {
        [SerializeField] private Texture2D cursor;
        private Vector2 hotSpot;

        public void SetCursor()
        {
            if (cursor == null) return;

            Cursor.SetCursor(cursor, hotSpot, CursorMode.Auto);
        }
    }
}