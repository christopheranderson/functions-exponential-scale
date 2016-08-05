#r "Newtonsoft.Json"
#r "Microsoft.WindowsAzure.Storage"

#load "./Item.csx"

using System;
using System.Net;
using System.Text;
using System.Threading.Tasks; 
using Newtonsoft.Json;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;

public class LogItem : Item, ITableEntity
{
    private TableEntity tableEntity;

    public DateTimeOffset enqueue_time { get; set; }
    public DateTimeOffset process_time { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public string ETag { get; set; }

    public LogItem() { }

    public LogItem(Item item, DateTimeOffset InsertionTime)
    {
        this.tableEntity = new TableEntity();

        this.run_id = item.run_id;
        this.multiplier = item.multiplier;
        this.max_generation = item.max_generation;
        this.current_generation = item.current_generation;
        this.id = item.id;
        this.parent_id = item.parent_id;
        this.PartitionKey = item.run_id;
        this.RowKey = Guid.NewGuid().ToString();
        this.enqueue_time = InsertionTime;
        this.process_time = DateTimeOffset.UtcNow;
    }

    public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
    {
        tableEntity.ReadEntity(properties, operationContext);
    }

    public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
    {
        return tableEntity.WriteEntity(operationContext);
    }
}

public class LogItem2 : TableEntity
{
    public string run_id { get; set; }
    public int multiplier { get; set; }
    public int max_generation { get; set; }
    public int current_generation { get; set; }
    public string id { get; set; }
    public string parent_id { get; set; }
    public DateTimeOffset enqueue_time { get; set; }
    public DateTimeOffset process_time { get; set; }

    public LogItem2() { }
}