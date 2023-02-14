using System;
namespace CSharpDemos.ClassLibrary.AsyncDemo
{
    public class InvokeAsyncDemo
    {
        public async Task InvokeMethod()
        {
            await MakeTeaAsync();
        }

        public async Task<string> MakeTeaAsync()
        {
            var boiling_water = BoilWaterAsync(); // Function runs until the Task.Delay(2000)

            "take the cups out.".Dump();
            "put tea in cup.".Dump();

            string water = await boiling_water; // Function resumes from the Task.Delay()

            string tea = $"pour {water} in cups.".Dump();
            return tea;
        }

        public async Task<string> BoilWaterAsync()
        {
            "start the kettle.".Dump();
            "waiting for the kettle.".Dump();
            await Task.Delay(5000);
            "kettle finished boiling.".Dump();

            return "water";
        }

        public string MakeTea()
        {
            string water = BoilWater();

            "take the cups out.".Dump();
            "put tea in cup.".Dump();
            string tea = $"pouring {water} in cups.".Dump();
            return tea;
        }

        public string BoilWater()
        {
            "start the kettle.".Dump();
            "waiting for the kettle.".Dump();
            Task.Delay(2000).GetAwaiter().GetResult();
            "kettle finished boiling.".Dump();

            return "water";
        }
    }
}

