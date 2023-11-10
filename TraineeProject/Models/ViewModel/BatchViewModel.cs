namespace TraineeProject.Models.ViewModel
{
    public class BatchViewModel
    {
        public int SelectBatch { get; set; }
        public IEnumerable<Batch> Batches { get; set; }
        public IEnumerable<Trainee> Trainees { get; set; }
    }
}
