using System.Collections;
using System.Collections.Generic;
using TFG.Behaviour;
using TFG.Behaviour.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace TFG.UI
{
    // Button that activates the visibility events of the body layers
    public class VisibilityButton : MonoBehaviour
    {
        [SerializeField] Sprite visibleSprite;
        [SerializeField] Sprite hiddenSprite;

        private bool isVisible = true;
        private Image buttonImage;

        [SerializeField] BodyVisibilityController controller;
        [SerializeField] BodyLayer bodyLayer;

        private void Awake()
        {
            buttonImage = GetComponent<Image>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Controller"))
            {
                isVisible = !isVisible;

                switch (bodyLayer)
                {
                    case BodyLayer.skeleton:
                        controller.ToggleSkeletonVisibility();
                        break;
                    case BodyLayer.ligaments:
                        controller.ToggleLigamentsVisibility();
                        break;
                    case BodyLayer.nervs:
                        controller.ToggleNervsVisibility();
                        break;
                    case BodyLayer.cardio:
                        controller.ToggleCardioVisibility();
                        break;
                }

                // Sprite change to indicate visibility
                if (isVisible) buttonImage.sprite = visibleSprite;
                else buttonImage.sprite = hiddenSprite;
            }
        }
    }
}