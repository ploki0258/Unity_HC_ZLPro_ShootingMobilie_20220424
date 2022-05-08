using UnityEngine;

//namespace �R�W�Ŷ�:�����϶�
namespace JACK
{
	/// <summary>
	/// ����t��:��ð����ʨt��
	/// �����n�챱��Ⲿ��
	/// </summary>
	public class SystemControl : MonoBehaviour
	{
		[SerializeField, Header("�����n��")]
		private Joystick joystick;
		[SerializeField, Header("���ʳt��"), Range(0, 300)]
		private float speed = 3.5f;
		[SerializeField, Header("�����V�ϥ�")]
		private Transform traDirectionIcon;
		[SerializeField, Header("�����V�ϥܽd��"), Range(0, 5)]
		private float rangeDirectionIcon = 2.5f;
		[SerializeField, Header("�������t��"), Range(0, 100)]
		private float speedTurn = 1.5f;
		[SerializeField, Header("�ʵe�Ѽƨ���")]
		private string parmeterWalk = "�}���]�B";



		private Rigidbody rig;
		private Animator ani;

		private void Awake()
		{
			rig = GetComponent<Rigidbody>();
			ani = GetComponent<Animator>();
		}

		private void Update()
		{
			//GetJoystickValue();
			UpdateDirectionIconPos();
			LookDirectionIcon();
			UpdateAnimation();
		}

		private void FixedUpdate()
		{
			Move();
		}

		/// <summary>
		/// ���o�����n��
		/// </summary>
		private void GetJoystickValue()
		{
			print("<color=yellow>���� : " + joystick.Horizontal + "</color>");
		}

		/// <summary>
		/// ���ʥ\��
		/// </summary>
		private void Move()
		{
			//����.�[�t�� = �T���V�q(X�AY�AZ)
			rig.velocity = new Vector3(joystick.Horizontal, 0, joystick.Vertical ) * speed;
		}

		/// <summary>
		/// ��s�����V�ϥܪ��y��
		/// </summary>
		private void UpdateDirectionIconPos()
		{
			//�y�� = ���⪺�y�� + �T���V�q(�����n�줧�����P����) * ��V�ϥܽd��
			Vector3 pos = transform.position + new Vector3(joystick.Horizontal, 0.5f, joystick.Vertical) * rangeDirectionIcon;
			//��s��V�ϥܪ��y�� = �s�y��
			traDirectionIcon.position = pos;
		}

		/// <summary>
		/// ���V��V�ϥ�
		/// </summary>
		private void LookDirectionIcon()
		{
			//���o���ۨ��� = �|�줸.���ۨ���(��V�ϥ� - ����) - ��V�ϥܻP���⪺�V�q
			Quaternion look = Quaternion.LookRotation(traDirectionIcon.position - transform.position);
			//���⪺���� = �|�줸.����(���⪺���סA���ۨ��סA����t�� * �@�V���ɶ�)
			transform.rotation = Quaternion.Lerp(transform.rotation, look, speedTurn * Time.deltaTime);
			//���⪺���� = �T���V�q(0,�쥻���کԨ���Y,0)
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		
		}

        /// <summary>
		/// ��s�ʵe
		/// </summary>
		private void UpdateAnimation()
        {
			//�O�_�]�B = �����n�� ���������s �� ���������s
			bool run = joystick.Horizontal != 0 || joystick.Vertical != 0;
			ani.SetBool(parmeterWalk, run);
        }
    }


}


