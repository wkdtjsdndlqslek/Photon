using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	public Rigidbody rb;
	public ParticleSystem particlePrefab;
	public Player owner;

    private void OnTriggerEnter(Collider other)
    {
        ParticleSystem particle = Instantiate(particlePrefab, transform.position, particlePrefab.transform.rotation);
		particle.Play();
		Destroy(particle.gameObject, 3);

		//�浹 ��� renderer�� collider ��Ȱ��ȭ �Ͽ� �������� ������ ���� �ʵ���
		GetComponent<Renderer>().enabled = false;
		GetComponent<Collider>().enabled = false;

		Destroy(particle.gameObject, 0.1f);

		Collider[] contectedColliders = Physics.OverlapSphere(transform.position, 1.5f);

		foreach(Collider collider in contectedColliders)
		{
			if(collider.tag == "Player")
			{
				collider.SendMessage("Hit", 1);
				PhotonView target = collider.GetComponent<PhotonView>();
				print($"{owner.NickName}�� ���� ��ź�� {target.Owner.NickName}���� ����");
			}
		}
    }

    private void Reset()
	{
		rb = GetComponent<Rigidbody>();
		particlePrefab = Resources.Load<ParticleSystem>("BombParticle");
	}
}