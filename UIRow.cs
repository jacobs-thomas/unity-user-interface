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
            float totalWidth = RectangleTransform.rect.width - totalSpacing;


            for (int i = 0; i < children.Length; i++)
            {
                // Get the total width of the container and calculate each child's width.
                float width = totalWidth / UIChildren.Length;
                float height = RectangleTransform.rect.height;

                // Calculate the starting X position for centering the row layout.
                float startX = -RectangleTransform.rect.width / 2 + width / 2;

                // Set the size of the child element to match the parent's calculated width and height.
                children[i].RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
                children[i].RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

                // Calculate position
                float xOffset = startX + i * (width + _gap); // Adjust for width and spacing.
                Vector3 position = new Vector3(xOffset, 0.0f, 0.0f);
                children[i].RectangleTransform.localPosition = position;
            }
        }

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
