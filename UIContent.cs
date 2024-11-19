using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable enable

namespace LG.UserInterface.Layouts
{
    [System.Serializable]
    public struct Style
    {
        /**
        * Represents the styling configuration for UI elements, 
        * including padding and margins.
        */


        // Instance attributes:
        public float padding;
        public float margins;
    }


    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class UIContent : MonoBehaviour
    {
        /**
        * UIContent is a base class for UI elements that handles scaling, 
        * positioning, and management of child elements. It provides methods 
        * for dynamically updating the layout and ensures elements are properly 
        * sized and positioned based on style properties.
        */

        // Class attributes:
        private static readonly UIContent[] EMPTY_ARRAY = new UIContent[0];

        // Instance attributes:
        [SerializeField] public bool inheritRect = true;
        public Style style;
        private RectTransform? _rectTransform = null;
        [SerializeField] private UIContent[] _uiContentChildren = EMPTY_ARRAY;

        // Properties:
        public RectTransform RectangleTransform
        {
            get
            {
                /**
                * Gets the RectTransform associated with this UIContent.
                * Initializes the RectTransform if not already initialized.
                */


                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        public float Width
        {
            /**
            * Gets or sets the adjusted width of the UIContent, considering the padding defined in the style.
            */
            get => RectangleTransform.rect.width - style.padding;

            set
            {

            }
        }

        public float Height
        {
            /**
            * Gets or sets the adjusted height of the UIContent, considering the padding defined in the style.
            */
            get => RectangleTransform.rect.height - style.padding;
            set
            {

            }
        }

        public UIContent[] UIChildren => _uiContentChildren;

        // Methods:
        private void Update()
        {
            OnScaleChildrenUpdate();
        }

        public void OnTransformChildrenChanged()
        {
            /**
            * Called when the transform hierarchy changes. 
            * Updates the _uiContentChildren array to include only immediate child UIContent elements.
            */


            // Create a list to store the UIContent components from immediate children
            List<UIContent> immediateChildren = new List<UIContent>();

            // Loop through each child transform of the current GameObject
            foreach (Transform child in transform)
            {
                // Get the UIContent component from the immediate child
                UIContent component = child.GetComponent<UIContent>();

                // If the component exists, add it to the list
                if (component != null)
                {
                    immediateChildren.Add(component);
                }
            }

            // Convert the list to an array and return
            _uiContentChildren = immediateChildren.ToArray();
        }

        public virtual void OnScaleChildrenUpdate()
        {
            /**
            * Updates the scale and position of all child elements that inherit the RectTransform.
            * Iterates through each child and calls the OnScaleChild method.
            */


            for (int i = 0; i < _uiContentChildren.Length; i++)
            {
                if (!_uiContentChildren[i].inheritRect) { continue; }

                OnScaleChild(_uiContentChildren[i], i);
            }
        }

        public virtual void OnScaleChild(UAddedIContent child, int position)
        {
            /**
            * Scales and positions a single child element based on its index in the array.
            *
            * @param child The child UIContent element to be scaled.
            * @param position The index of the child element.
            */


            // Set the child's width and height to match the parent's width and height, while respecting anchors.
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Width);
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Height);

            // Optionally, you could reset the localPosition to ensure it stays within the bounds.
            child.RectangleTransform.localPosition = Vector3.zero;  // You can modify this if you need specific positioning behavior
        }
    }
}

