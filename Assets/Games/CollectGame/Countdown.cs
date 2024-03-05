using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
        public TMP_Text geriSayimText;
        public int baslangicSuresi = 3;
        
        
        public IEnumerator GeriSayimiBaslat()
        {
            int sayac = baslangicSuresi;
    
            while (sayac >= 0)
            {
                geriSayimText.text = sayac.ToString();
                yield return new WaitForSeconds(1f);
                sayac--;
            }
    
            geriSayimText.text = "Başla!";
            yield return new WaitForSeconds(.75f);
            gameObject.SetActive(false);

            // Geri sayım tamamlandıktan sonra istediğiniz işlemi yapabilirsiniz
        }
}
