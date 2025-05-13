using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Cylinder : MonoBehaviour
{
    [SerializeField] private List<Potion> _potionList = new List<Potion>();
    [SerializeField] private List<Transform> _posList = new List<Transform>();

    [SerializeField] private Potion _lastElement;
    [SerializeField] private int _potionCount;
    [SerializeField] private Transform _movePoint;

    public bool IsEmpty;

    public int PotionCount
    {
        get 
        { 
            return _potionCount; 
        }
        set
        {
            _potionCount = value;
        }
    }

    void Start()
    {
        _potionList = GetComponentsInChildren<Transform>()
            .Select(t => t.GetComponent<Potion>())
            .Where(p => p != null)
            .OrderByDescending(p => p.transform.position.y)
            .ToList();

        if (_potionList.Count == 0) IsEmpty = true;
        _potionCount = _potionList.Count;

    }


    public void SetReadyPotions()
    {
        var tempList = _potionList.ToList();
        Debug.Log(tempList.Count);

        //tempList[0].transform.DOMoveY(_movePoint.transform.position.y, .15f).SetEase(Ease.OutBack);

        for (int i = 0; i < tempList.Count; i++)
        {
            var x = tempList[i];
            x.transform.DOMoveY(_movePoint.transform.position.y - (i * .55f), .15f)
                .SetDelay(i * .03f)
                .SetEase(Ease.OutSine)
                .OnComplete(() =>
                {
                    x.ShakeBottle();
                });
        }
    }

    public Potion GetFirstPotion()
    {
        return _potionList.FirstOrDefault();
    }

    //public int PotionCount() { return _potionCount; }
    
    //public int 

}
