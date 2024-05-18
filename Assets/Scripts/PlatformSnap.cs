using UnityEngine;

public class PlatformSnap : MonoBehaviour
{
    [SerializeField] private Transform _targetPlatform; // Mevcut platformun transformu.
    [SerializeField] private float _snapTolerance = 0.1f; // Tolerans değeri, editörden ayarlanabilir.
    [SerializeField] private GameObject _player; // Oyuncunun GameObject'i.

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float distance = Mathf.Abs(transform.position.z - _targetPlatform.position.z);
            if (distance <= _snapTolerance)
            {
                SnapToTarget();
                PlayComboSound(); // Combo ses fonksiyonu.
            }
            else
            {
                BreakAndSnapOrFall(distance); // Yanlış snapleme durumunda kırma veya düşme işlemi.
            }
        }
    }

    private void SnapToTarget()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _targetPlatform.position.z);
        // Platformu tam olarak hedefe yerleştir.
    }

    private void BreakAndSnapOrFall(float distance)
    {
        if (distance <= _targetPlatform.localScale.z / 2) // Eğer platform yarısından fazla doğru pozisyonda ise
        {
            // Kırılacak parça hesaplamaları ve uygulaması.
            float breakPoint = _targetPlatform.position.z + (_targetPlatform.localScale.z / 2) * Mathf.Sign(transform.position.z - _targetPlatform.position.z);
            BreakPlatform(breakPoint);
            SnapToTargetPartially(breakPoint); // Platformun doğru kısmını yerleştir.
        }
        else
        {
            // Platform tamamen yanlış yerleştirilmişse, karakter düşsün.
            _player.GetComponent<PlayerController>().Fall();
        }
    }

    private void BreakPlatform(float breakPoint)
    {
        // Platformun belirlenen noktadan kırılmasını ve kırılan parçanın düşürülmesini sağla.
    }

    private void SnapToTargetPartially(float snapPoint)
    {
        // Yalnızca doğru olan kısmı hedefe snaple.
        transform.position = new Vector3(transform.position.x, transform.position.y, snapPoint);
    }

    private void PlayComboSound()
    {
        // Combo başarılı olduğunda ses oynat.
    }
}
