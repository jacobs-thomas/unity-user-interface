using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

namespace LG
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class UIBase : MonoBehaviour
    {
        // Instance attributes:
        [SerializeField] public bool inheritRect = true; 
        private RectTransform? _rectTransform = null;
        private UIBase? _parentUIContainer = null;

        // Properties:
        public RectTransform Rect
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

        public bool HasUIContainerParent => _parentUIContainer != null;


        // Methods:
        private void Update()
        {
            ForceChildToFitParent();
        }

        [ExecuteInEditMode]
        private void OnTransformParentChanged()
        {
            if (transform.parent == null)
            {
                _parentUIContainer = null;
                Debug.Log("Transform parent removed.");
                return;
            }

            _parentUIContainer = transform.parent.GetComponent<UIBase>();

            if (_parentUIContainer)
            {
                Debug.Log("Transform parent added.");
            }
        }

        public void OnTransformChildrenChanged()
        {
            
        }

        private void RemoveAnchors()
        {

        }

        [ExecuteInEditMode]
        private void OnEnable()
        {
            ForceChildToFitParent();
        }

        [ExecuteInEditMode]
        private void ForceChildToFitParent()
        {
            if (inheritRect && Rect != null && _parentUIContainer != null)
            {
                if (_parentUIContainer.Rect == null) { return; }

                OnEnterParentUIContainer(_parentUIContainer);
            }
        }

        public virtual void OnEnterParentUIContainer(UIBase uiContainer)
        {
            // Set the child's width and height to match the parent's width and height, while respecting anchors.
            Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _parentUIContainer.Rect.rect.width);
            Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _parentUIContainer.Rect.rect.height);

            // Optionally, you could reset the localPosition to ensure it stays within the bounds.
            Rect.localPosition = Vector3.zero;  // You can modify this if you need specific positioning behavior
        }
    }
}
