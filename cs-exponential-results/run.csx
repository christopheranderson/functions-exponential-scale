#r "Newtonsoft.Json"
#r "Microsoft.WindowsAzure.Storage"

#load "../lib/LogItem.csx"

using System;
using System.Net;
using System.Text;
using System.Threading.Tasks; 
using Newtonsoft.Json;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;

public static Task<HttpResponseMessage> Run(HttpRequestMessage req, IQueryable<LogItem2> telemetry, TraceWriter log)
{
    log.Verbose($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}"); 

    // parse query parameter
    var queryParam = req.GetQueryNameValuePairs()
         .ToDictionary(p => p.Key, p => p.Value, StringComparer.OrdinalIgnoreCase);
    string runid;
    bool getQueryParam = queryParam.TryGetValue("runid", out runid);

    HttpResponseMessage res = null;
    
    if(getQueryParam)
    {
        var query = ( from t in telemetry where t.PartitionKey == runid select t);
        
        res = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json")
        };
    }
    else
    {
        var query = (from t in telemetry select t).ToList().GroupBy(i => i.PartitionKey).Select(g => new {
            runid = g.Key,
            count = g.Count(),
            min = g.Min(v => v.Timestamp),
            max = g.Max(v => v.Timestamp),
            duration = (g.Max(v => v.Timestamp).UtcTicks/10000 - g.Min(v => v.Timestamp).UtcTicks / 10000),
            completed = g.Max(v => v.current_generation) == g.Max(v=>v.max_generation)
        });
    res = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json")
        };
    }
    return Task.FromResult(res);
} 


