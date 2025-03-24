using SystemForManagingFitnessTrainings.Entities;

namespace SystemForManagingFitnessTrainings.Helpers
{
    public class ExerciseTableViewModel
    {
        public List<Exercise> Exercises { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public string SelectedCategory { get; set; } = "All";
    }
}
