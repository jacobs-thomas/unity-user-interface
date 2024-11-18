using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface
{
    [ExecuteInEditMode]
    public class UIColumn : UIContent
    {
        public override void OnScaleChildrenUpdate()
        {
            for (int i = 0; i < UIChildren.Length; i++)
            {
                ScaleChild(UIChildren[i], i);
            }
        }

        public void ScaleChild(UIContent child, int position)
        {
            float totalHeight = RectangleTransform.rect.height;
            float height = totalHeight / UIChildren.Length;
            float width = RectangleTransform.rect.width;

            // Calculate starting position for centering
            float startY = -totalHeight / 2 + height / 2;

            // Set the child's size to match the parent's width and height
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

            // Position the child with proper centering and offset
            child.RectangleTransform.localPosition = new Vector3(0.0f, startY + (height * position), 0.0f);
        }

        public override void OnScaleChildUI(UIContent child)
        {
            // Set the child's width and height to match the parent's width and height, while respecting anchors.
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, RectangleTransform.rect.width);
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, RectangleTransform.rect.height);

            // Optionally, you could reset the localPosition to ensure it stays within the bounds.
            child.RectangleTransform.localPosition = Vector3.zero;  // You can modify this if you need specific positioning behavior
        }
    }
}
