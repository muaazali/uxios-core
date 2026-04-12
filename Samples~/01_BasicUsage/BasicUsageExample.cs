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

        // Example Multipart/Form-Data File Upload (using SendFile)
        byte[] fileData = System.Text.Encoding.UTF8.GetBytes("Hello, world!"); // Replace with actual file data
        var formFields = new System.Collections.Generic.Dictionary<string, string> { { "description", "Test file upload" } };
        var uploadResponse = await UxiosApi.SendFile<object>(
            url: "https://httpbin.org/post",
            fileData: fileData,
            fileName: "hello.txt",
            fieldName: "file",
            contentType: "text/plain",
            formFields: formFields,
            method: HttpMethod.Post
        );
        Debug.Log($"Upload Status: {uploadResponse.StatusCode}\nResponse: {uploadResponse.RawBody}");
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