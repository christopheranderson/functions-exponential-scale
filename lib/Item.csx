public class Item
{
    public string run_id { get; set; }
    public int multiplier { get; set; }
    public int max_generation { get; set; }
    public int current_generation { get; set; }
    public string id { get; set; }
    public string parent_id { get; set; }

    public static Item nextGen(Item item)
    {
        return new Item()
        {
            run_id = item.run_id,
            multiplier = item.multiplier,
            max_generation = item.max_generation,
            current_generation = item.current_generation + 1,
            id = Guid.NewGuid().ToString(),
            parent_id = item.id
        };
    }
}