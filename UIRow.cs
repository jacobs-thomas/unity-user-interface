using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface
{
    [ExecuteInEditMode]
    public class UIRow : UIContent
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
            float totalWidth = RectangleTransform.rect.width;
            float width = totalWidth / UIChildren.Length;
            float height = RectangleTransform.rect.height;

            // Calculate starting position for centering
            float startX = -totalWidth / 2 + width / 2;

            // Set the child's size to match the parent's width and height
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

            // Position the child with proper centering and offset
            child.RectangleTransform.localPosition = new Vector3(startX + (width * position), 0.0f, 0.0f);
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
