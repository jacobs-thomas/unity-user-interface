using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LG.UserInterface.Layouts
{
    [ExecuteInEditMode]
    public class UIRow : UIContent
    {
        /**
        * UIRow is a custom layout class for arranging UIContent elements horizontally.
        * It evenly distributes child elements along the horizontal axis, adjusting their scale and position
        * to fit within the parent container's width.
        */


        // Methods:
        public override void OnScaleChild(UIContent child, int position)
        {
            /**
            * Scales and positions a specific child element within the row based on its position index.
            * Ensures each child is proportionally scaled and placed to maintain a consistent layout.
            *
            * @param child The child UIContent element to be scaled and positioned.
            * @param position The index of the child element within the row.
            */


            // Get the total width of the container and calculate each child's width.
            float totalWidth = RectangleTransform.rect.width;
            float width = totalWidth / UIChildren.Length;
            float height = RectangleTransform.rect.height;

            // Calculate the starting X position for centering the row layout.
            float startX = -totalWidth / 2 + width / 2;

            // Set the size of the child element to match the parent's calculated width and height.
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

            // Position the child element at the correct offset within the row.
            child.RectangleTransform.localPosition = new Vector3(startX + (width * position), 0.0f, 0.0f);
        }
    }
}
