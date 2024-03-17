namespace testMvc.Controllers
{

    public record UserRegister
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
    }
    public record StudentRegister
    {
        public string name { get; set; }
        public string email { get; set; }
        public int degree { get; set; }
        public int age { get; set; }
    }

}
