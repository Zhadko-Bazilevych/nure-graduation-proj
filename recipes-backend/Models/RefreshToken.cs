namespace recipes_backend.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Device { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
