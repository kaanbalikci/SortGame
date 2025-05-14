using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Cylinder : MonoBehaviour
{
    [SerializeField] private List<Potion> _potionList = new List<Potion>();
    [SerializeField] private List<Potion> _tempPotionList = new List<Potion>();
    [SerializeField] private List<Transform> _posList = new List<Transform>();

    [SerializeField] private Potion _lastElement;
    [SerializeField] private int _potionCount;
    [SerializeField] private int _holdingPotionCount;
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

    public int HoldingPotionCount
    {
        get
        {
            return _holdingPotionCount;
        }
        set
        {
            _holdingPotionCount = value;
        }
    }

    void Start()
    {
        SetPotionList();

        if (_potionList.Count == 0) IsEmpty = true;
        PotionCount = _potionList.Count;

        if (PotionCount > 0)
        {
            SetIndexToPotion();

        }
    }

    private void SetPotionList()
    {
        _potionList = GetComponentsInChildren<Transform>()
            .Select(t => t.GetComponent<Potion>())
            .Where(p => p != null)
            .OrderByDescending(p => p.transform.position.y)
            .ToList();
    }

    public void SetIndexToPotion()
    {
        var index = 3;
        for (int i = PotionCount - 1; i >= 0; i--)
        {
            _potionList[i].Index = index;
            index--;
        }
    }

    public void SetReadyPotions()
    {
        var type = _potionList[0].GetType();

        for (int i = 0; i < PotionCount; i++)
        {
            if (_potionList[i].GetType() == type)
                _tempPotionList.Add(_potionList[i]);
            else
                break;
        }

        _holdingPotionCount = _tempPotionList.Count;

        for (int i = 0; i < _tempPotionList.Count; i++)
        {
            var x = _tempPotionList[i];

            x.transform.DOKill();
            x.transform.DOMoveY(_movePoint.transform.position.y - (i * .55f), .15f)
                .SetDelay(i * .03f)
                .SetEase(Ease.OutSine)
                .OnComplete(() =>
                {
                    //x.ShakeBottle();
                });
            
        }
    }

    public Potion GetFirstPotion()
    {
        return _potionList.FirstOrDefault();
    }

    public void SetDownPotions(int downCount)
    {
        var newList = _tempPotionList.ToList();
        _tempPotionList.Clear();

        for (int i = newList.Count - 1; i >= 0; i--)
        {

            newList[i].transform.DOKill();

            newList[i].transform.eulerAngles = Vector3.zero;

            newList[i].transform.DOMoveY(_posList[newList[i].Index].position.y, .05f);
        }

        //var x = _tempPotionList.Count - downCount;




    }

}
