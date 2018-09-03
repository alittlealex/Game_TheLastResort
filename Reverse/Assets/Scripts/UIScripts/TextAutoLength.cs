using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAutoLength : MonoBehaviour {
    public GameObject textView;
    public ScrollRect scrollView;
    private string contentString;
	
	void Start ()
    {
    }
	
	void Update ()
    {
        if (Network.m_Actor.isChatAck)
        {
            GameObject go = Instantiate(textView, transform);
            if(Network.m_Actor.chatAck.gender == Network.m_Actor.gender)
            {
                contentString = Network.m_Actor.name +": " + Network.m_Actor.chatAck.chat;
            }
            else
            {
                contentString = Network.m_Actor.anotherName + ": " + Network.m_Actor.chatAck.chat;
            }
            go.GetComponent<Text>().text = contentString;
            StartCoroutine("ScrollBarBotton");
            Network.m_Actor.isChatAck = false;
        }
	}

    IEnumerator ScrollBarBotton()
    {
        yield return new WaitForEndOfFrame();
        scrollView.verticalNormalizedPosition = 0f;
    }
}
