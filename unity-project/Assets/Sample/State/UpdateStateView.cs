using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using NeCo;

public class UpdateStateView : MonoBehaviour
{
    [SerializeField]
    private Button m_button;

    [SerializeField]
    private Text m_text;

    private List<Action> onClicked;
    private int index = 0;

    [Inject]
    public void Init(CurrentlyStateObserver observer)
    {
        onClicked = new List<Action>()
        {
            () => observer.Currently.ChangeState<StateB>(),
            () => observer.Currently.ChangeState<StateC>(),
            () => observer.Currently.ChangeState<StateD>(),
            () => observer.Currently.ChangeState<StateE>(),
            () => observer.Currently.ChangeState<StateA>()
        };

        m_button.onClick.AddListener(
            () =>
            {
                if (index >= onClicked.Count)
                    index = 0;

                onClicked[index].Invoke();
                index++;
            });

        observer.OnUpdatedState += (state) => m_text.text = state.Name;
    }

}
