﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
