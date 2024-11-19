using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LG.UserInterface.Layouts
{
    [ExecuteInEditMode]
    public class UIColumn : UIContent
    {
        /**
        * UIColumn is a custom layout class for arranging UIContent elements vertically.
        * It evenly distributes child elements along the vertical axis, adjusting their scale and position
        * to fit within the parent container's bounds.
        */


        // Methods:
        public override void OnScaleChild(UIContent child, int position)
        {
            /**
            * Scales and positions a specific child element within the column based on its position index.
            *
            * @param child The child UIContent element to be scaled and positioned.
            * @param position The index of the child element within the column.
            */


            // Get the total height of the container and calculate each child's height.
            float totalHeight = RectangleTransform.rect.height;
            float height = totalHeight / UIChildren.Length;
            float width = RectangleTransform.rect.width;

            // Calculate the starting Y position for centering the column layout.
            float startY = -totalHeight / 2 + height / 2;

            // Set the size of the child element to match the parent's width and calculated height.
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

            // Position the child element at the correct offset within the column.
            child.RectangleTransform.localPosition = new Vector3(0.0f, startY + (height * position), 0.0f);
        }
    }
}
