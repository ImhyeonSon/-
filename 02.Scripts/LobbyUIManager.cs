using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour //Canvas¿¡ ³Ö±â
{
    // Start is called before the first frame update


    public GameObject RankPannel;
    public TextMeshProUGUI Rank1;
    public TextMeshProUGUI Rank2;
    public TextMeshProUGUI Rank3;
    public void RankPopUPClose()
    {
        RankPannel.SetActive(false);
    }
    public void RankPopUP()
    {
        List<int> RankList = PlayerInfoManager.Instance.LoadScoreData();
        Rank1.text = "" + RankList[0] + " Round";
        Rank2.text = "" + RankList[1] + " Round";
        Rank3.text = "" + RankList[2] + " Round";
        RankPannel.SetActive(true);
    }

}
