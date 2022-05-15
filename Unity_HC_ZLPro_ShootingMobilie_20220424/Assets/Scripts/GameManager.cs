using UnityEngine;
using Photon.Pun;
using Photon.Pun;
using System.Collections.Generic; //�ޥΨt�ΤΤ@��(��Ƶ��c�AList�AArrayList...)
using System.Linq; //�ޥΨt�άd�߻y�� (��Ƶ��c�ഫ API)

namespace JACK
{
	/// <summary>
	///�C���޲z��
	///�P�_�p�G�O�s�u�i�J���a
	///�N�ͦ����⪫��(�Ԥh)
	/// </summary>
	public class GameManager : MonoBehaviourPun
	{
		[SerializeField, Header("���⪫��")]
		private GameObject goCharacter;
		[SerializeField, Header("�ͦ��y�Ъ���")]
		private Transform[] traSpawnPoint;

		/// <summary>
		/// �x�s�ͦ��y�вM��
		/// </summary>
		[SerializeField]
		private List<Transform> traSpawnPointList;


		//����ƥ�:����C���ɰ���@���A��l�Ƴ]�w
		private void Awake()
		{
			traSpawnPointList = new List<Transform>(); //�s�W�M�檫��
			traSpawnPointList = traSpawnPoint.ToList(); //�}�C�ର�M���Ƶ��c

			Screen.SetResolution(024, 576, false);

			//�ù�.�]�w�ѪR��(�e�A���A�O���ù�)
			PhotonNetwork.ConnectUsingSettings();

			//�p�G�O�s�u�i�J�����a�N�b���A���ͦ����⪫��
			if (photonView.IsMine)
			{
				int indexRandom = Random.Range(0, traSpawnPointList.Count); //���o�H���M��(0�A�M�����)
				Transform tra = traSpawnPointList[indexRandom]; //���o�H���y��

				//Photon ���A��.�ͦ�(����.�W�ٮy�СA����)
				PhotonNetwork.Instantiate(goCharacter.name, tra.position, tra.rotation);

				traSpawnPointList.RemoveAt(indexRandom); //�R���w���o���ͦ��y�и��
			}
		}
	}
}