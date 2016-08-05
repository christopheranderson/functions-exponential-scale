#r "Newtonsoft.Json"

#load "../lib/Item.csx"

using System;
using System.Net;
using System.Text;
using System.Threading.Tasks; 
using Newtonsoft.Json;

public static Task<HttpResponseMessage> Run(HttpRequestMessage req, ICollector<Item> queue, TraceWriter log)
{
    log.Verbose($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    // Get request body
    string jsonContent = req.Content.ReadAsStringAsync().Result;
    dynamic data = JsonConvert.DeserializeObject(jsonContent); 

    HttpResponseMessage res = null;
    
    string runId = data?.run_id ?? DateTimeOffset.UtcNow.ToString();
    int multiplier = data?.multiplier ?? 2 ;
    int maxGeneration = data?.max_generation ?? 2;
    
    queue.Add(new Item()
    {
        run_id = runId,
        multiplier = multiplier,
        max_generation = maxGeneration,
        current_generation = 0,
        id = "start",
        parent_id = null
    });
    
    res = new HttpResponseMessage(HttpStatusCode.OK)
    {
        Content = new StringContent(JsonConvert.SerializeObject(new { error = false, run_id = runId, multiplier = multiplier, max_generation = maxGeneration }), Encoding.UTF8, "application/json")
    };
        
    return Task.FromResult(res);
}


