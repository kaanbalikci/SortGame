using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SortManager : MonoBehaviour
{
    [SerializeField] private LayerMask _cylinderLayer;
    [SerializeField] private bool _isSelectedCylinder;

    [SerializeField] private Cylinder _currentCylinder;
    [SerializeField] private Cylinder _holdingCylinder;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastCheck();
    }


    private void RaycastCheck()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _cylinderLayer))
            {
                _currentCylinder = hit.collider.GetComponent<Cylinder>();

                if (!_isSelectedCylinder)
                {
                    if (!_currentCylinder.IsEmpty)
                    {
                        SetReadyPotion(_currentCylinder);
                        Debug.Log("1");
                    }
                }
                else
                {
                    if (_currentCylinder.IsEmpty)
                    {
                        //jump to cylinder
                    }
                    else
                    {
                        if(_currentCylinder == _holdingCylinder)
                        {
                            Debug.Log("Same cylinder");
                            _holdingCylinder.SetDownPotions(_holdingCylinder.HoldingPotionCount);
                            _isSelectedCylinder = false;
                        }
                        else
                        {
                            Debug.Log("SSS");
                            _holdingCylinder.SetDownPotions(_holdingCylinder.HoldingPotionCount);
                            SetReadyPotion(_currentCylinder);
                            //if (_currentCylinder.PotionCount != 4)
                            //{
                            //    if (CheckPotionsType(_currentCylinder, _holdingCylinder))
                            //    {
                            //        ControlPotionCount(_currentCylinder, _holdingCylinder);
                            //    }
                            //}
                            //else
                            //{
                            //    _holdingCylinder.SetDownPotions(_holdingCylinder.HoldingPotionCount);
                            //    SetReadyPotion(_currentCylinder);
                            //}

                        }
                    }

                    
                }
            }
        }
    }

    private void SetReadyPotion(Cylinder cylinder)
    {
        cylinder.SetReadyPotions();
        _isSelectedCylinder = true;
        _holdingCylinder = cylinder;
    }

    private bool CheckPotionsType(Cylinder firstCyl, Cylinder secondCyl)
    {
        return firstCyl.GetFirstPotion().GetType() == secondCyl.GetFirstPotion().GetType() ? true : false;
    }

    private void ControlPotionCount(Cylinder firstCyl, Cylinder secondCyl)
    {
        var transferCount = (4 - firstCyl.PotionCount);

        var holdingCount = _holdingCylinder.HoldingPotionCount;

        if(holdingCount > transferCount)
        {
            var downCount = holdingCount - transferCount;

            //transferCount jump potion 

            secondCyl.SetDownPotions(downCount);
        }



    }

}
