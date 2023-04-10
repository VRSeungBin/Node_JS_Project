using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Protocols
{
    public class Packets
    {
        public class common
        {
            public int cmd;
        }
        public class req_scores : common
        {
            public string id;
            public int score;
        }
        [System.Serializable]
        public class res_scores : common
        {
            public string message;
        }

        [System.Serializable]
        public class user
        {
            public string id;
            public int score;
        }
        [System.Serializable]
        public class res_scores_top3 : res_scores
        {
            public user[] result;
        }
        [System.Serializable]
        public class res_scores_id : res_scores
        {
            public user result;
        }
        [System.Serializable]
        public class userid
        {
            public string id;
            public string pw;
        }
        [System.Serializable]
        public class req_registerid : common
        {
            public userid userid;
        }

        public class res_registerid : common
        {
            public string message;
        }

    }
}