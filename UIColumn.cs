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


        // Instance attributes:
        [SerializeField] private float _gap = 0.0f;


        // Properties:
        public float Gap
        {
            get => _gap;
            set
            {
                _gap = Mathf.Clamp(value, 0, Width / 2);
            }
        }


        // Methods:
        public override void OnScaleChildrenUpdate(UIContent[] children)
        {
            float totalSpacing = (Mathf.Max(children.Length - 1, 0)) * _gap;
            float totalHeight = RectangleTransform.rect.height - totalSpacing;


            for (int i = 0; i < children.Length; i++)
            {
                // Get the total width of the container and calculate each child's width.
                float height = totalHeight / UIChildren.Length;
                float width = RectangleTransform.rect.width;

                // Calculate the starting X position for centering the row layout.
                float startY = -RectangleTransform.rect.height / 2 + height / 2;

                // Set the size of the child element to match the parent's calculated width and height.
                children[i].RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
                children[i].RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

                // Calculate position
                float YOffset = startY + i * (height + _gap); // Adjust for width and spacing.
                Vector3 position = new Vector3(0.0f, YOffset, 0.0f);
                children[i].RectangleTransform.localPosition = position;
            }
        }

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
