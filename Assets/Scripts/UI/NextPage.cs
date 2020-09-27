using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPage : MonoBehaviour
{
    public List<GameObject> pages;
    private int pageNum = 0;

    public void TurnPage()
    {
        pages[pageNum].SetActive(false);

        if (pageNum == pages.Count - 1)
        {
            pageNum = 0;
        }
        else
        {
            pageNum++;
        }

        pages[pageNum].SetActive(true);
    }
}
