using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LG.UserInterface.Layouts
{
    public class UIGrid : UIContent
    {
        /**
        * UIGrid is a custom layout class for arranging UIContent elements in a grid format.
        * It scales and positions child elements evenly within a defined number of rows and columns.
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


        // Instance attributes:
        public uint numberOfColumns = 1;
        public uint numberOfRows = 1;


        // Methods:
        public override void OnScaleChildrenUpdate(UIContent[] children)
        {


            // Width:
            float totalSpacingWidth = (Mathf.Max(numberOfColumns - 1, 0)) * _gap;
            float totalWidth = Width - totalSpacingWidth;

            // Height:
            float totalSpacingHeight = (Mathf.Max((int)(children.Length / numberOfRows) - 1, 0)) * _gap;
            float totalHeight = Height - totalSpacingHeight;


            for (int i = 0; i < children.Length; i++)
            {
                // Determine the row and column based on the position.
                int row = i / (int)numberOfColumns;
                int column = i % (int)numberOfColumns;

                // Calculate the width and height of each grid cell.
                float width = totalWidth / numberOfColumns;
                float height = totalHeight / numberOfRows;

                // Calculate the starting X position for centering the row layout.
                // float startX = -RectangleTransform.rect.width / 2 + width / 2;
                // float startY = -RectangleTransform.rect.height / 2 + height / 2;

                // Calculate starting position for centering
                float startX = -Width / 2+ (width / 2);
                float startY = -Height / 2+ (height / 2);



                // Set the size of the child element to match the parent's calculated width and height.
                children[i].RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
                children[i].RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

                // Calculate position
                float xOffset = startX + column * (width + _gap); // Adjust for width and spacing.
                float YOffset = startY + row * (height + _gap); // Adjust for width and spacing.
                Vector3 position = new Vector3(xOffset, YOffset, 0.0f);
                children[i].RectangleTransform.localPosition = position;
            }
        }


        public override void OnScaleChild(UIContent child, int position)
        {
            /**
            * Scales and positions a specific child element within the grid based on its position index.
            *
            * @param child The child UIContent element to be scaled and positioned.
            * @param position The index of the child element within the grid.
            */


            // Determine the row and column based on the position.
            int row = position / (int)numberOfColumns;
            int column = position % (int)numberOfColumns;

            // Calculate the total available width and height for the grid.
            float totalWidth = Width;
            float totalHeight = Height;

            // Calculate the width and height of each grid cell.
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
    }
}