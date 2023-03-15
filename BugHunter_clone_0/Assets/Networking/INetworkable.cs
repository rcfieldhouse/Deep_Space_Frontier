using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface INetwork
{

    public void Serialize(ClientPackets packet);

    public void Deserialize(ServerPackets packet);


}
