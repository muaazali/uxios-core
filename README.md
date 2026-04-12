# Uxios Core

A modern, async-first HTTP client for Unity — inspired by Axios and fetch, designed for simplicity and productivity.

## Installation

**Via Unity Package Manager (UPM):**

1. Open your Unity project.
2. In the Project window, right-click the `Packages` folder and select **Show in Explorer**.
3. Open `manifest.json` in a text editor.
4. Add the following line to the `dependencies` section:

```
"com.uxios.core": "https://github.com/muaazali/uxios-core.git#main"
```

5. Save the file and return to Unity. Uxios Core will appear in your Packages.

---

## Basic Usage

````csharp
using Uxios.Core;

// GET request
var response = await UxiosApi.Get<MyData>("https://jsonplaceholder.typicode.com/posts/1");
Debug.Log(response.Data.title);

// POST request
var newPost = new MyData { title = "foo", body = "bar" };
var postResponse = await UxiosApi.Post<MyData>("https://jsonplaceholder.typicode.com/posts", newPost);
Debug.Log(postResponse.Data.id);
 
 // FILE UPLOAD (multipart/form-data)
 byte[] fileData = System.Text.Encoding.UTF8.GetBytes("Hello, world!");
 var uploadResponse = await UxiosApi.SendFile<object>(
     url: "https://httpbin.org/post",
     fileData: fileData,
     fileName: "hello.txt",
     fieldName: "file",
     contentType: "text/plain",
     formFields: new Dictionary<string, string> { { "description", "Test file upload" } },
     method: HttpMethod.Post
 );
 Debug.Log(uploadResponse.StatusCode);
 
 // CUSTOM REQUEST (advanced)
 var customRequest = new UxiosRequest {
     Method = HttpMethod.Patch,
     Url = "https://jsonplaceholder.typicode.com/posts/1",
     Body = "{ \"title\": \"updated\" }",
     Headers = new Dictionary<string, string> { { "Authorization", "Bearer ..." } }
 };
 var customResponse = await UxiosApi.SendAsync<object>(customRequest);
 Debug.Log(customResponse.StatusCode);

// Define your data model
[System.Serializable]
public class MyData {
    public int id;
    public string title;
    public string body;
}
````

---

## Features
- Async/await API
- Generics for typed responses
- JSON serialization (Unity's JsonUtility by default)
- Global config for base URL, timeout, and headers
- CancellationToken support
- Beautiful request/response logging
- File upload (multipart/form-data)
- Custom request support (all HTTP methods, headers, body, files)

---

For advanced configuration, see the sample scene included in the package.
