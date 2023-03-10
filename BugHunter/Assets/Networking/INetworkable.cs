using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INetworkable
{
    struct KeyValuePair<TKey, Tvalue>
    {
        public TKey key { get; set; }
        public Tvalue value { get; set; }
    }

    void NetworkSend<T>(T Data, int ConnectionID);

    void NetworkReceive<T>(T Data);

}
