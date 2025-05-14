using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private float angleX=6, angleY=4, angleZ=3;
    private float duration;

    public int Index;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShakeBottle()
    {
        duration = Random.Range(1.0f, 2.0f);
        transform.DOLocalRotate(new Vector3(angleX, -angleY, angleZ), duration / 2);
        //_normalPos = transform.position;
        DOVirtual.DelayedCall(.15f, () => {
            transform.DOLocalRotate(new Vector3(-angleX, angleY, -angleZ), duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);

            transform.DOMoveY(-.05f,duration*1.5f).SetRelative().SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
        });


    }

    public void StopTween(float time, Vector3 movePoint)
    {
        transform.DOKill();
        transform.DOLocalRotate(Vector3.zero, time);
        transform.DOMoveY(movePoint.y, time);
    }

}
