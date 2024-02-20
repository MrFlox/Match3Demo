using System.Collections;
using UnityEngine;

namespace UI
{
    public interface ICoroutineProvider
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}