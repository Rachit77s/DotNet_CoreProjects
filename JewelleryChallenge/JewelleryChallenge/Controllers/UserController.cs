using JewelleryChallenge.Data.Entities;
using JewelleryChallenge.Data.Services;
using JewelleryChallenge.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JewelleryChallenge.Controllers
{
    //("api/user/")
    [Route("api/[controller]")]
    [ApiController]
    [TokenAuthenticationFilter]
    public class UserController : ControllerBase
    {
        private readonly IUser _repository;

        public UserController(IUser repository)
        {
            _repository = repository;
        }


        public  User GetUser(string username, string password)
        {
           return  _repository.GetUser(username, password);
        }



        [HttpGet("{username}")]
        //("api/user/username")
        public IActionResult Get(string username, string password)
        {
            try
            {
                var result = _repository.GetUser(username);

                if (result == null)
                {
                    return NotFound($"Could not find the User with the username of {username}");
                }
                return Ok($"Welcome {result.Username} user.");

                //return Ok(new
                //{
                //    Response = $"Welcome {result.Username} user.",
                //    UserId = result.Id,
                //    Username = result.Username
                //});
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        //[HttpGet("calculateprice/{goldPrice:float}/{weight:float}/{discount:int}")]
        [HttpGet("calculateprice")]
        //("api/user/calculateprice")
        public IActionResult CalculatePrice(float goldPrice, float weight, int discount = 0)
        {
            try
            {

                float totalAmount = 0;

                //var rev = Request.Headers["Authorization"];
                Request.Headers.TryGetValue("Authorization", out var traceValue);

                if (goldPrice == 0 || weight == 0)
                {
                    return BadRequest("Please provide both the GoldPrice and Weight to calculate the TotalAmount.");
                }

                if (!String.IsNullOrEmpty(traceValue) && traceValue.Any() )
                {
                    if (discount >= 1 && traceValue[0].Contains("Privileged"))
                    {
                        totalAmount = (goldPrice * weight) - discount;
                        return Ok($"You are a privileged customer, therefore with discount the total amount is : {totalAmount}");
                    }
                    else if (traceValue[0].Contains("Privileged"))
                    {
                        totalAmount = (goldPrice * weight);
                        return Ok($"You are a privileged customer, but you have not entered any discount, hence the total amount is : {totalAmount}");
                    }
                    else
                    {
                        totalAmount = (goldPrice * weight);
                        return Ok($"You are a normal customer, the total amount is : {totalAmount}");
                    }
                }
                else
                {
                    return BadRequest("Please provide Token as Header");
                }                                          
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("printscreen")]
        //("api/user/printscreen")
        public IActionResult PrintScreen()
        {
            return Ok("File has been downloaded and message popup is shown.");
        }

        [HttpGet("printfile")]
        //("api/user/printfile")
        public Object PrintFile()
        {


            var result = new HttpResponseMessage(HttpStatusCode.OK);

            // 1) Get file bytes
            var filePath = Path.Combine(
                             Directory.GetCurrentDirectory(),
                             "Resources", "JewelleryChallengeTestingPostman.docx");

            var fileBytes = System.IO.File.ReadAllBytes(filePath);

            // 2) Add bytes to a memory stream
            var fileMemStream = new MemoryStream(fileBytes);

            // 3) Add memory stream to response
            result.Content = new StreamContent(fileMemStream);

            // 4) build response headers
            var headers = result.Content.Headers;

            headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            headers.ContentDisposition.FileName = "JewelleryChallengeTestingPostman.docx";

            headers.ContentType =  new MediaTypeHeaderValue("application/octet-stream");

            headers.ContentLength = fileMemStream.Length;

            //Prints the output in JSON Format
            //return result;


            //Prints the output in actual file content
            return File(fileMemStream, "application/pdf", "test.pdf");

        }

        [HttpGet("printpaper")]
        //("api/user/printpaper")
        public IActionResult PrintPaper()
        {
            return Ok("Print to Paper Successfull.");
        }
    }
}
