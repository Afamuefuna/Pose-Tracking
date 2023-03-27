using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BodyInput : MonoBehaviour
{
    [SerializeField] private Transform pos;
    [SerializeField] private Transform trackedPos;
    [SerializeField] private TMP_Text xPos, yPos, zPos;
    [SerializeField] private int annote;
    [SerializeField] private int firstMatchPos, secondMatchPos;
    [SerializeField] private bool hasMatchedFirst, hasMatchedSecond;

    public static bool triggerJump;

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    public void Jump()
    {
        if (pos.GetChild(annote) != null)
        {
            trackedPos = pos.GetChild(annote);
            var localPosition = trackedPos.localPosition;
            
            xPos.text = localPosition.x.ToString();
            yPos.text = localPosition.y.ToString();
            zPos.text = localPosition.z.ToString();

            if (trackedPos.localPosition.y >= secondMatchPos)
            {
                if (!hasMatchedSecond)
                {
                    hasMatchedSecond = true;
                    hasMatchedFirst = false;
                    triggerJump = true;
                }
            }
            
            if (trackedPos.localPosition.y <= firstMatchPos)
            {
                if (!hasMatchedFirst)
                {
                    hasMatchedFirst = true;
                    hasMatchedSecond = false;
                }
            }
        }
    }
}
