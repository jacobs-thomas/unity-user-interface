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
        public uint numberOfColumns = 1;
        public uint numberOfRows = 1;


        // Methods:
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