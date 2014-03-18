using UnityEngine;
using System.Collections;

public class BaseScreen : MonoBehaviour {
	
	public virtual void Init() {}
	public virtual void Init(int theme) {}
	
	public virtual void Close() {
		Destroy(gameObject);
	}
	
	public virtual void Open() {
		
	}
	
}
