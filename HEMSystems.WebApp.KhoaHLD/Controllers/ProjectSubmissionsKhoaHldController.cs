using HEMSystems.Entities.KhoaHLD.Models;
using HEMSystems.Services.KhoaHLD;
using HEMSystems.Services.KhoaHLD.DTOs.Common;
using HEMSystems.Services.KhoaHLD.DTOs.ProjectSubmission;
using HEMSystems.WebApp.KhoaHLD.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace HEMSystems.WebApp.KhoaHLD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectSubmissionsKhoaHldController : ControllerBase
    {
        private readonly IProjectSubmissionsKhoaHldService _projectSubmissionsKhoaHldService;

        public ProjectSubmissionsKhoaHldController(IProjectSubmissionsKhoaHldService projectSubmissionsKhoaHldService)
        {
            _projectSubmissionsKhoaHldService = projectSubmissionsKhoaHldService;
        }

        [HttpGet]
        [Authorize]
        [EnableQuery]
        public async Task<List<ProjectSubmissionsKhoaHld>> Get()
        {
            try
            {
                return await _projectSubmissionsKhoaHldService.GetAllProjectSubmissionsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return [];
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var response = await _projectSubmissionsKhoaHldService.GetProjectSubmissionByIdAsync(id);

                if (response == null)
                {
                    var notFoundResponse = new ApiResponse<ProjectSubmissionGetByIdResponse>
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Project submission not found",
                        Data = null
                    };
                    return StatusCode(StatusCodes.Status404NotFound, notFoundResponse);
                }

                var apiResponse = new ApiResponse<ProjectSubmissionGetByIdResponse>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Project submission retrieved successfully",
                    Data = response
                };
                return StatusCode(StatusCodes.Status200OK, apiResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var apiResponse = new ApiResponse<ProjectSubmissionGetByIdResponse>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Project submission retrieved unsuccessfully",
                    Data = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, apiResponse);
            }
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery] ProjectSubmissionSearchRequest request)
        {
            try
            {
                var response = await _projectSubmissionsKhoaHldService.SearchProjectSubmissionsAsync(request);

                var apiResponse = new ApiResponse<PagedResult<ProjectSubmissionsKhoaHld>>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Project submissions searched successfully",
                    Data = response
                };
                return StatusCode(StatusCodes.Status200OK, apiResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var apiResponse = new ApiResponse<PagedResult<ProjectSubmissionsKhoaHld>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Project submissions searched unsuccessfully",
                    Data = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, apiResponse);
            }
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Post([FromBody] ProjectSubmissionCreateRequest request)
        {
            try
            {
                var response = await _projectSubmissionsKhoaHldService.CreateProjectSubmissionAsync(request);

                if (response > 0)
                {
                    var apiResponse = new ApiResponse<string?>
                    {
                        StatusCode = StatusCodes.Status201Created,
                        Message = "Project submission created successfully",
                        Data = null
                    };
                    return StatusCode(StatusCodes.Status201Created, apiResponse);
                }

                var failedResponse = new ApiResponse<string?>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Project submission created unsuccessfully",
                    Data = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, failedResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var apiResponse = new ApiResponse<string?>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Project submission created unsuccessfully",
                    Data = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, apiResponse);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Put(int id, [FromBody] ProjectSubmissionUpdateRequest request)
        {
            try
            {
                var response = await _projectSubmissionsKhoaHldService.UpdateProjectSubmissionAsync(id, request);

                if (response > 0)
                {
                    var apiResponse = new ApiResponse<string?>
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Project submission updated successfully",
                        Data = null
                    };
                    return StatusCode(StatusCodes.Status200OK, apiResponse);
                }

                var failedResponse = new ApiResponse<string?>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Project submission updated unsuccessfully",
                    Data = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, failedResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var apiResponse = new ApiResponse<string?>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Project submission updated unsuccessfully",
                    Data = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, apiResponse);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var isDeleted = await _projectSubmissionsKhoaHldService.DeleteProjectSubmissionAsync(id);

                if (isDeleted)
                {
                    var apiResponse = new ApiResponse<string?>
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Project submission deleted successfully",
                        Data = null
                    };
                    return StatusCode(StatusCodes.Status200OK, apiResponse);
                }

                var failedResponse = new ApiResponse<string?>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Project submission deleted unsuccessfully",
                    Data = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, failedResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var apiResponse = new ApiResponse<string?>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Project submission deleted unsuccessfully",
                    Data = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, apiResponse);
            }
        }
    }
}
