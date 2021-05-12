using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

public class ServerController : MonoBehaviour
{

    public JsonData data;

    static string serverIP = "109.191.103.182";
    static int serverPort = 5555;
    bool connected = false;
    public static long myId = -1;

    int receivedPackets = 0;
    int usedPackets = 0;

    public static UdpClient client = new UdpClient(serverIP, serverPort);

    void Start()
    {

        Thread connector = new Thread(Connect);
        connector.Start();

        string data = "connect";
        byte[] udp = Encoding.UTF8.GetBytes(data);
        client.Send(udp, udp.Length);
        int a = 0;
        while (!connected && a < 3)
        {
            a++;
            Thread.Sleep(1000);
        }
        print("connected: " + connected);
        connector.Abort();

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().myId = myId;

        Thread receiver = new Thread(ReceiveData);
        receiver.Start();
    }

    void Update()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().data = data;
    }

    void ReceiveData()
    {
        Dictionary<long, Dictionary<int, JsonResponse>> dict = new Dictionary<long, Dictionary<int, JsonResponse>>();


        while (true)
        {
            long currentPacketId = -1;
            JsonResponse response = NextPacket();
            receivedPackets++;

            if (!dict.ContainsKey(response.id))
            {
                dict[response.id] = new Dictionary<int, JsonResponse>();
            }

            dict[response.id].Add(response.packetNumber, response);

            StringBuilder str = new StringBuilder();
            foreach (Dictionary<int, JsonResponse> minidict in dict.Values)
            {
                if (minidict.ContainsKey(1) && minidict[1].packetAmount == minidict.Count)
                {
                    for (int i = 1; i<= minidict[1].packetAmount; i++)
                    {
                        str.Append(minidict[i].data);
                    }
                    data = JsonConvert.DeserializeObject<JsonData>(str.ToString());
                    currentPacketId = minidict[1].id;
                    usedPackets += minidict[1].packetAmount;
                }
            }

            List<long> forDelete = new List<long>();
            foreach(long id in dict.Keys)
            {
                if (id <= currentPacketId)
                {
                    forDelete.Add(id);
                }
            }

            foreach (long id in forDelete)
            {
                dict.Remove(id);
            }

            float packetLoss = 1 - (float)usedPackets / receivedPackets;
/*            Debug.Log(packetLoss);
*/        }
    }

    void Connect()
    {
        IPEndPoint remoteIp = null;
        byte[] data = client.Receive(ref remoteIp);
        string id = Encoding.UTF8.GetString(data);
        myId = Int32.Parse(id);
        connected = true;
    }

    JsonResponse NextPacket()
    {
        IPEndPoint remoteIp = null;
        byte[] udp = client.Receive(ref remoteIp);
        string data = Encoding.UTF8.GetString(udp);
        return JsonConvert.DeserializeObject<JsonResponse>(data);
    }

    public static void Move(float horizontalInput, float verticalInput, float time)
    {
        List<string> parameters = new List<string>();
        parameters.Add(horizontalInput.ToString());
        parameters.Add(verticalInput.ToString());
        parameters.Add(time.ToString());

        JsonReply reply = new JsonReply(myId, "move", parameters);
        string data = JsonConvert.SerializeObject(reply);
        byte[] udp = Encoding.UTF8.GetBytes(data);
        client.Send(udp, udp.Length);
    }

    public static void Attack(float x, float y)
    {
        List<string> parameters = new List<string>();
        parameters.Add(x.ToString());
        parameters.Add(y.ToString());

        JsonReply reply = new JsonReply(myId, "fire", parameters);
        string data = JsonConvert.SerializeObject(reply);
        byte[] udp = Encoding.UTF8.GetBytes(data);
        client.Send(udp, udp.Length);
    }

    public static void PickUpItem(long id)
    {
        List<string> parameters = new List<string>();
        parameters.Add(id.ToString());

        JsonReply reply = new JsonReply(myId, "pickupitem", parameters);
        string data = JsonConvert.SerializeObject(reply);
        byte[] udp = Encoding.UTF8.GetBytes(data);
        client.Send(udp, udp.Length);
    }

    public static void DropItem(int itemPosition)
    {
        List<string> parameters = new List<string>();
        parameters.Add(itemPosition.ToString());

        JsonReply reply = new JsonReply(myId, "dropitem", parameters);
        string data = JsonConvert.SerializeObject(reply);
        byte[] udp = Encoding.UTF8.GetBytes(data);
        client.Send(udp, udp.Length);
    }

    public static void MoveItem(int itemPosition1, int itemPosition2, int amount)
    {
        List<string> parameters = new List<string>();
        parameters.Add(itemPosition1.ToString());
        parameters.Add(itemPosition2.ToString());
        parameters.Add(amount.ToString());

        JsonReply reply = new JsonReply(myId, "moveitem", parameters);
        string data = JsonConvert.SerializeObject(reply);
        byte[] udp = Encoding.UTF8.GetBytes(data);
        client.Send(udp, udp.Length);
    }

    public static void EquipItem(int itemPosition)
    {
        List<string> parameters = new List<string>();
        parameters.Add(itemPosition.ToString());

        JsonReply reply = new JsonReply(myId, "equipitem", parameters);
        string data = JsonConvert.SerializeObject(reply);
        byte[] udp = Encoding.UTF8.GetBytes(data);
        client.Send(udp, udp.Length);
    }

    public static void DequipItem(long id)
    {
        List<string> parameters = new List<string>();
        parameters.Add(id.ToString());

        JsonReply reply = new JsonReply(myId, "dequip", parameters);
        string data = JsonConvert.SerializeObject(reply);
        byte[] udp = Encoding.UTF8.GetBytes(data);
        client.Send(udp, udp.Length);
    }

    public static void DeleteItem(int itemPosition)
    {
        List<string> parameters = new List<string>();
        parameters.Add(itemPosition.ToString());

        JsonReply reply = new JsonReply(myId, "deleteitem", parameters);
        string data = JsonConvert.SerializeObject(reply);
        byte[] udp = Encoding.UTF8.GetBytes(data);
        client.Send(udp, udp.Length);
    }

}
