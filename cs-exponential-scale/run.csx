#load "../lib/LogItem.csx"

using System;
using System.Threading.Tasks;

public static void Run(Item inbound, DateTimeOffset InsertionTime, ICollector<Item> outbound, ICollector<LogItem> telemetry, TraceWriter log)
{
    log.Verbose($"C# Queue trigger function processed: {inbound}");
    
    telemetry.Add(new LogItem(inbound, InsertionTime));
    
    if(inbound.current_generation < inbound.max_generation)
    {
        for(int i = 0; i < inbound.multiplier; i++) {
            outbound.Add(Item.nextGen(inbound));
        }
    }
}
