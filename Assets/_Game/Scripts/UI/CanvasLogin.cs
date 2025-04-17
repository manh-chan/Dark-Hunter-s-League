using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLogin : MonoBehaviour
{
    public CanvasForgotPass preForgot;
    public void ForgotButton() {
        preForgot.gameObject.SetActive(true);
    }
}
