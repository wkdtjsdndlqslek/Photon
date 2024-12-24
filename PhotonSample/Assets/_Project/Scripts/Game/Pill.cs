using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Pill : MonoBehaviourPun
{
	public Renderer render;

	private float healAmount; //���� ����

    private void Awake()
    {
        //PhotonNetwork.Instatiate ȣ�� �� �Բ� ���� data�Ķ����
        object[] param = photonView.InstantiationData;

        if (param != null)
        {
            Vector3 colorVector = (Vector3)param[0];
            float healAmount = (float)param[1];

            render.material.color = new Color(colorVector.x, colorVector.y, colorVector.z);
            this.healAmount = healAmount;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
		if(other.tag=="Player")
		{
			other.SendMessage("Heal", healAmount);
        }
        Destroy(gameObject);
    }

    private void Reset() {
		render = GetComponentInChildren<Renderer>();

		
	}
}


