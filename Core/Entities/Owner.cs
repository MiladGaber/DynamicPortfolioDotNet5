namespace Core.Entities
{
    public class Owner : EntityBase
    {
        public string FullName { get; set; }
        public string JopTitle { get; set; }
        public string AvatarUrl { get; set; }

        public Address Address{ get; set; }
    }
}
