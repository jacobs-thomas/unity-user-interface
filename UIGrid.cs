using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterface
{
    public class UIGrid : UIContent
    {
        // Instance attributes:
        public uint numberOfColumns = 1;
        public uint numberOfRows = 1;

        // Methods:
        public override void OnScaleChildrenUpdate()
        {
            for (int i = 0; i < UIChildren.Length && i < (numberOfRows * numberOfColumns); i++)
            {
                ScaleChild(UIChildren[i], i);
            }
        }

        public void ScaleChild(UIContent child, int position)
        {
            int row = position / (int)numberOfRows;
            int column = position % (int)numberOfRows;


            float totalWidth = RectangleTransform.rect.width;
            float totalHeight = RectangleTransform.rect.height;

            float width = totalWidth / numberOfColumns;
            float height = totalHeight / numberOfRows;

            // Calculate starting position for centering
            float startX = -(totalWidth / 2) + (width / 2);
            float startY = -(totalHeight / 2) + (height / 2);

            // Set the child's size to match the parent's width and height
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

            // Position the child with proper centering and offset
            child.RectangleTransform.localPosition = new Vector3(startX + (width * column), startY + (height * row), 0.0f);
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