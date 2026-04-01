using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Uxios.Core;
using Uxios.Core.Models;

public class BasicUsageExample : MonoBehaviour
{
    private async void Start()
    {
        // Example GET request
        var response = await UxiosApi.Get<Post>("https://jsonplaceholder.typicode.com/posts/1");
        Debug.Log($"Title: {response.Data.title}\nBody: {response.Data.body}");

        // Example POST request
        var newPost = new Post { userId = 1, title = "foo", body = "bar" };
        var postResponse = await UxiosApi.Post<Post>("https://jsonplaceholder.typicode.com/posts", newPost);
        Debug.Log($"Created Post ID: {postResponse.Data.id}");
    }

    [System.Serializable]
    public class Post
    {
        public int userId;
        public int id;
        public string title;
        public string body;
    }
}