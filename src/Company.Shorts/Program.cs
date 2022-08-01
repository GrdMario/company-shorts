namespace Company.Shorts
{
    using Company.Shorts.Blocks.Bootstrap;

    public static class Program
    {
        public static async Task Main(string[] args) => await ApplicationLauncher.RunAsync<Startup>(args);
    }
}
