namespace SystemForManagingFitnessTrainings.Entities
{
    public class TrainingPlans
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Exercises> Exercises { get; set; }
        public int FrequencyOfTrainigns { get; set; }
    }
}
