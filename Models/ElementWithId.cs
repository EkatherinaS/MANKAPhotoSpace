namespace PracticalTraining.Models
{
    public class ElementWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public object AddditionalInfo { get; set; }

        public ElementWithId(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public ElementWithId(int id, string name, object additionalInfo)
        {
            Id = id;
            Name = name;
            AddditionalInfo = additionalInfo;
        }
    }
}
