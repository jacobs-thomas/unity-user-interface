using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable enable

namespace UserInterface
{
    [System.Serializable]
    public struct Style
    {
        // Instance attributes:
        public float padding;
        public float margins;
    }


    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class UIContent : MonoBehaviour
    {
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
                // While we could initialise the rect on awake doing it this way on the property ensures 
                // that the component is initialised no matter the state of the game or engine.
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        public float Width
        {
            get => RectangleTransform.rect.width - style.padding;

            set
            {

            }
        }

        public float Height
        {
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
            for (int i = 0; i < _uiContentChildren.Length; i++)
            {
                if (!_uiContentChildren[i].inheritRect) { continue; }

                OnScaleChildUI(_uiContentChildren[i]);
            }
        }

        public virtual void OnScaleChildUI(UIContent child)
        {
            // Set the child's width and height to match the parent's width and height, while respecting anchors.
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Width);
            child.RectangleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Height);

            // Optionally, you could reset the localPosition to ensure it stays within the bounds.
            child.RectangleTransform.localPosition = Vector3.zero;  // You can modify this if you need specific positioning behavior
        }
    }
}

