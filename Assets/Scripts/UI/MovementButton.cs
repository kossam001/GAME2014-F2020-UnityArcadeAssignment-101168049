/* MovementButton.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-14
 * 
 * Movement controls for touch input.
 * 
 * 2020-10-14: Added this script.
 * 2020-10-15: Added fingerID so distinguish independent touches 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementButton : Button
{
    private Vector3 m_touchesEnded;

    public bool isHolding = false;
    public Vector3 buttonOrigin;

    private float maxDist = 70;
    public float MaxDist
    {
        get { return maxDist; }
    }

    private Vector3 holdDirection;
    public Vector3 HoldDirection
    {
        get { return holdDirection; }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (!isHolding)
        {
            isHolding = true;
            StartCoroutine(Hold());
        }
    }

    IEnumerator Hold()
    {
        buttonOrigin = gameObject.GetComponent<RectTransform>().position;
        while (isHolding)
        {
            foreach (var touch in Input.touches)
            {
                // Only detect the initial touch
                if (touch.fingerId == 0)
                {
                    var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

                    gameObject.GetComponent<RectTransform>().position = touch.position;

                    float currentDist = Vector3.Distance(buttonOrigin, gameObject.GetComponent<RectTransform>().position);
                    Vector3 fromCenter = gameObject.GetComponent<RectTransform>().position - buttonOrigin;

                    if (currentDist > maxDist)
                    {
                        fromCenter = fromCenter.normalized;
                        fromCenter *= (maxDist - currentDist);
                        gameObject.GetComponent<RectTransform>().position += fromCenter;
                    }

                    holdDirection = (gameObject.GetComponent<RectTransform>().position - buttonOrigin).normalized;
                    m_touchesEnded = worldTouch;
                    
                }
            }
            yield return null;
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        isHolding = false;

        gameObject.GetComponent<RectTransform>().position = buttonOrigin;
        holdDirection = Vector3.zero;
    }
}
