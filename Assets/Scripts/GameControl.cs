using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public GameObject completeLevel;
    GameObject token;
    GameObject _temp;
    public int count = 0;
    public List<int> faceIndexes = new List<int> {0,1,2,3,4,5,6,7,0,1,2,3,4,5,6,7};
    public static System.Random random = new System.Random();
    public int shuffleNumber = 0;
    int[] visibleFaces = { -1,-2};

    private void Start()
    {
        int originalLenght = faceIndexes.Count;
        float yPosition = 3f;
        float xPosition = -2.5f;

        for (int i = 0; i < 15; i++)
        {
            shuffleNumber = random.Next(0, (faceIndexes.Count));
            _temp = Instantiate(token, new Vector3(
                    xPosition, yPosition, 0),
                Quaternion.identity);
            _temp.GetComponent<MainToken>().faceIndex = faceIndexes[shuffleNumber];
            faceIndexes.Remove(faceIndexes[shuffleNumber]);
            xPosition += 2.9f;
            if (i == originalLenght / 2 )
            {
                yPosition = 0f;
                xPosition = -7.4f;
            } else if (i == originalLenght / 4 - 1 )
            {
                yPosition = -3.1f;
                xPosition = -5.4f;
            }
        }

        token.GetComponent<MainToken>().faceIndex = faceIndexes[0];

    }

    public bool TwoCardsUp()
    {
        bool cardsUp = false;
        if(visibleFaces[0] >= 0 && visibleFaces[1] >= 0)
        {
            cardsUp = true;
        }
        return cardsUp;
    }

    public void AddVisibleFace(int index)
    {
        if(visibleFaces[0] == -1)
        {
            visibleFaces[0] = index;
        }
        else if (visibleFaces[1] == -2)
        {
            visibleFaces[1] = index;
        }
    }

    public void RemoveVisibleFace(int index)
    {
        if (visibleFaces[0] == index)
        {
            visibleFaces[0] = -1;
        }
        else if (visibleFaces[1] == index)
        {
            visibleFaces[1] = -2;
        }
    }
    
    public bool CheckMatch()
    {
        bool sucessEqual = false;
        if (visibleFaces[0] == visibleFaces[1])
        {
            visibleFaces[0] = -1;
            visibleFaces[1] = -2;
            sucessEqual = true;
            count += 1;
            print(count);
            Win();
        }

        return sucessEqual;
    }

    public void Win()
    {
        if (count >= 8)
        {
            completeLevel.SetActive(true);
        }
    }
    private void Awake()
    {
        token = GameObject.Find("Token");
    }
}
