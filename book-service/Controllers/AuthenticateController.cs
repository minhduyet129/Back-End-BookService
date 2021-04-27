using System.Text;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using book_service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace book_service.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthenticateController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> _userManager;  
    private readonly RoleManager<IdentityRole> _roleManager;  
    private readonly IConfiguration _configuration;  

    public AuthenticateController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration){
      _userManager=userManager;
      _roleManager=roleManager;
      _configuration=configuration;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterModel model){
      var userExists=await _userManager.FindByNameAsync(model.Username);
      if(userExists !=null)
        return StatusCode(StatusCodes.Status500InternalServerError,new Response{Status="Error",Message="User already exists!"});


      ApplicationUser user =new ApplicationUser(){
        Email=model.Email,
        UserName=model.Username
      };
      var result =await _userManager.CreateAsync(user,model.Password);
      if(!result.Succeeded)
      return StatusCode(StatusCodes.Status500InternalServerError,new Response{Status="Error",Message="User creation failed! Please check user details and try again."});
      if(!await _roleManager.RoleExistsAsync(RoleName.User))
      {
        await _roleManager.CreateAsync(new IdentityRole(RoleName.User));
      }
      await _userManager.AddToRoleAsync(user,RoleName.User);
      return Ok(new Response{Status="Success",Message="User created successfully!"});
    }

    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin(RegisterModel model){
      var userExists=await _userManager.FindByNameAsync(model.Username);
      if(userExists !=null)
        return StatusCode(StatusCodes.Status500InternalServerError,new Response{Status="Error",Message="User already exists!"});


      ApplicationUser user =new ApplicationUser(){
        Email=model.Email,
        UserName=model.Username
      };
      var result =await _userManager.CreateAsync(user,model.Password);
      if(!result.Succeeded)
      return StatusCode(StatusCodes.Status500InternalServerError,new Response{Status="Error",Message="User creation failed! Please check user details and try again."});
      if(!await _roleManager.RoleExistsAsync(RoleName.Admin))
      {
        await _roleManager.CreateAsync(new IdentityRole(RoleName.Admin));
      }
      await _userManager.AddToRoleAsync(user,RoleName.Admin);
      return Ok(new Response{Status="Success",Message="User created successfully!"});
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginModel model){
      var user =await _userManager.FindByNameAsync(model.Username);
      if(user !=null && await _userManager.CheckPasswordAsync(user,model.Password)){
        var userRoles=await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>{
          new Claim(ClaimTypes.Name,user.UserName)
        };
        foreach(var userRole in userRoles){
          authClaims.Add(new Claim(ClaimTypes.Role,userRole));
        }
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));  

        var token = new JwtSecurityToken(  
                    issuer: _configuration["JWT:ValidIssuer"],  
                    audience: _configuration["JWT:ValidAudience"],  
                    expires: DateTime.Now.AddHours(3),  
                    claims: authClaims,  
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
                    ); 

        return Ok(new  
                {  
                    token = new JwtSecurityTokenHandler().WriteToken(token),  
                    expiration = token.ValidTo ,
                    role =userRoles,
                    userId=user
                });  
      }
       return Unauthorized(); 
    }
  }
}