using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskMVC.Models;

namespace TaskMVC.Service
{
    public class TaskService: BaseService
    {
        private readonly HttpClientService _httpClientService;
       

        public TaskService(HttpClient client, HttpClientService httpClientService, 
            IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(client, httpContextAccessor, config)
        {
            _httpClientService = httpClientService;
         
        }
      
        public async Task<ReturnTaskDto> Create(CreateTaskDto taskDto)
        {
            

            const string apiUrl = "api/tasks";

            var result =
                await _httpClientService.PostAsync<CreateTaskDto, ReturnTaskDto>(apiUrl,
                    taskDto);
           
            if (result == null)
                return null;

            return result;
        }
     
        public async Task<IEnumerable<ReturnTaskDto>> GetAllTasksAsync()
        {
            const string address = "api/tasks";

            var result =
                await _httpClientService.GetAsync<IEnumerable<ReturnTaskDto>>(address);

            if (result == null) return Enumerable.Empty<ReturnTaskDto>();

          

            return result;
        }
        public async Task<ReturnTaskDto> GetTaskById(string taskId)
        {
            var apiUrl = $"api/tasks/{taskId}";

            var result = await _httpClientService.GetAsync<ReturnTaskDto>(apiUrl);

            return result;
        }

        
        public async Task<IEnumerable<ReturnTaskDto>> GetTasksByAssignedToIdAsync(string assignedToId)
        {
            var apiUrl = $"api/tasks/assigned-to/{assignedToId}";

            var result = await _httpClientService.GetAsync<IEnumerable<ReturnTaskDto>>(apiUrl);

            return result ?? Enumerable.Empty<ReturnTaskDto>();
        }

        public async Task<IEnumerable<ReturnTaskDto>> GetTasksByAssignedByIdAsync(string assignedById)
        {
            var apiUrl = $"api/tasks/assigned-by/{assignedById}";

            var result = await _httpClientService.GetAsync<IEnumerable<ReturnTaskDto>>(apiUrl);

            return result ?? Enumerable.Empty<ReturnTaskDto>();
        }

      

        public async Task<ReturnTaskDto> UpdateTask(string taskId, UpdateTaskDto taskDto)
        {
            var apiUrl = $"api/tasks/{taskId}";

            var result = await _httpClientService.PutAsync<UpdateTaskDto, ReturnTaskDto>(apiUrl, taskDto);

            return result;
        }

        //public async Task<bool> DeleteTask(string taskId)
        //{
        //    var apiUrl = $"api/tasks/{taskId}";

        //    var result = await _httpClientService.DeleteAsync<bool>(apiUrl);

        //    return result;
        //}
    }
}
