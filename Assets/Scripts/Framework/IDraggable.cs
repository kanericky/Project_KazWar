using UnityEngine;

namespace Framework
{
    public interface IDraggable
    {
        public void OnMouseDown();

        public void OnMouseUp();

        public void OnMouseDrag();

        public void CheckAndPlace();
    }
}