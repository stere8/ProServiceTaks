namespace TaskManagerBackend
{
    public class Enums
    {
        public enum TaskType
        {
            Implementation = 0,
            Deployment = 1,
            Maintenance = 2,
        }

        public enum ImplementationStatus
        {
            ToBeDone = 0,
            Completed = 1,
        }

        public enum UserType
        {
            DevOpsAdministrator = 0,
            Programmer = 1,
        }
    }
}
