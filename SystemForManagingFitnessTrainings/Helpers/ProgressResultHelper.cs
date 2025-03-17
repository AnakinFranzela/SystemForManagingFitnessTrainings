namespace SystemForManagingFitnessTrainings.Helpers
{
    public class ProgressResultHelper
    {
        public static string ToResultString(int weight, int reps, int timeInSeconds)
        {
            // e.g. "weight=80.5;reps=12;time=45"
            return $"weight={weight};reps={reps};time={timeInSeconds}";
        }

        public static (int Weight, int Reps, int TimeInSeconds) FromResultString(string results)
        {
            // If results is null or empty, return defaults
            if (string.IsNullOrEmpty(results))
                return (0, 0, 0);

            // Expected format e.g. "weight=80;reps=10;time=45"
            // We'll split by ";" then by "="
            var parts = results.Split(';', StringSplitOptions.RemoveEmptyEntries);
            int weight = 0;
            int reps = 0, time = 0;

            foreach (var part in parts)
            {
                var keyValue = part.Split('=', StringSplitOptions.RemoveEmptyEntries);
                if (keyValue.Length != 2) continue;

                var key = keyValue[0].Trim();
                var value = keyValue[1].Trim();

                if (key == "weight" && int.TryParse(value, out var w))
                    weight = w;
                else if (key == "reps" && int.TryParse(value, out var r))
                    reps = r;
                else if (key == "time" && int.TryParse(value, out var t))
                    time = t;
            }
            return (weight, reps, time);
        }
    }
}
