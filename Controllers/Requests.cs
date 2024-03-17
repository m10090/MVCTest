namespace testMvc.Controllers
{

    public record UserRegister
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
    }

}
