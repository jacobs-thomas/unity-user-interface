using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LG.UserInterface.Layouts
{
    public class UIFlowLayout : UIContent
    {
        // Instance attributes:
        public LayoutDirection layoutDirection;

        // Enums:
        public enum LayoutDirection
        {
            HORIZONTAL,
            VERTICAL
        }

        // Methods:
        public virtual void OnScaleChild(UIContent child, int position)
        {



            // Set the child's width and height to match the parent's width and height, while respecting anchors.
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Width);
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Height);



            // Optionally, you could reset the localPosition to ensure it stays within the bounds.
            child.RectangleTransform.localPosition = Vector3.zero;  // You can modify this if you need specific positioning behavior
        }
    }
}