using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/files")]

public class FileController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    public FileController(IWebHostEnvironment env)
    {
        _env = env;
    }
}