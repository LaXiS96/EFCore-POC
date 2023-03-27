namespace WebApplication1.DataModel
{
    public static class DbInitializer
    {
        public static void Initialize(MyContext context)
        {
            if (context.Entities.Any())
                return;

            var entities = new Entity[]
            {
                new() { Id = Guid.NewGuid(), JsonDocument = """{"key1":"value1","Key2":1234}""" },
                new() { Id = Guid.NewGuid(), JsonDocument = """{"asd":"kek","ROFL":5555}""" },
            };
            context.Entities.AddRange(entities);
            context.SaveChanges();
        }
    }
}
