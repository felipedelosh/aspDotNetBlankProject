﻿using Microsoft.AspNetCore.Mvc;

namespace aspDotNetBlankProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok("Server is RUN");
        }
    }
}
