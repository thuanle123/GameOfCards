/*
using UnityEngine;

using UnityEngine.Networking;

using UnityEngine.Networking.Match;

using UnityEngine.SceneManagement;

public class ChanceManager : NetworkManager
{
    // Server callbacks

    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("A client connected to the server: " + conn);
        SceneManager.LoadScene(6);

    }
    public override void OnStartHost()
    {

        Debug.Log("Host has started");
        SceneManager.LoadScene(6);


    }
    public override void OnClientConnect(NetworkConnection conn)
    {

        base.OnClientConnect(conn);

        Debug.Log("Connected successfully to server, now to set up other stuff for the client...");
        SceneManager.LoadScene(6);
    }
    public override void OnStopServer()
    {

        Debug.Log("Server has stopped");
        SceneManager.LoadScene(0);

    }

    public override void OnStopHost()
    {

        Debug.Log("Host has stopped");
        SceneManager.LoadScene(0);

    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {

        NetworkServer.DestroyPlayersForConnection(conn);

        if (conn.lastError != NetworkError.Ok)
        {

            if (LogFilter.logError) { Debug.LogError("ServerDisconnected due to error: " + conn.lastError); }

        }

        Debug.Log("A client disconnected from the server: " + conn);
        SceneManager.LoadScene(0);
    }
    public override void OnClientDisconnect(NetworkConnection conn)
    {

        StopClient();

        if (conn.lastError != NetworkError.Ok)

        {

            if (LogFilter.logError) { Debug.LogError("ClientDisconnected due to error: " + conn.lastError); }

        }

        Debug.Log("Client disconnected from server: " + conn);
        SceneManager.LoadScene(0);
    }
    /*
    public override void OnServerReady(NetworkConnection conn)
    {

        NetworkServer.SetClientReady(conn);

        Debug.Log("Client is set to the ready state (ready to receive state updates): " + conn);

    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {

        var player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        Debug.Log("Client has requested to get his player added to the game");

    }

    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
    {

        if (player.gameObject != null)

            NetworkServer.Destroy(player.gameObject);

    }

    public override void OnServerError(NetworkConnection conn, int errorCode)
    {

        Debug.Log("Server network error occurred: " + (NetworkError)errorCode);

    }

    

    public override void OnStartServer()
    {

        Debug.Log("Server has started");

    }

    // Client callbacks

    

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {

        Debug.Log("Client network error occurred: " + (NetworkError)errorCode);

    }

    public override void OnClientNotReady(NetworkConnection conn)
    {

        Debug.Log("Server has set client to be not-ready (stop getting state updates)");

    }

    public override void OnStartClient(NetworkClient client)
    {

        Debug.Log("Client has started");

    }

    public override void OnStopClient()
    {

        Debug.Log("Client has stopped");

    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {

        base.OnClientSceneChanged(conn);

        Debug.Log("Server triggered scene change and we've done the same, do any extra work here for the client...");

    }
   
}*/