// namespace Infrastructure.Services;

// using Domain.Entities;
// using Infrastructure.Data;
// using Microsoft.AspNetCore.Hosting;
// using Domain.Dtos;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Http;

// public class TodoService
// {
//     private readonly DataContext _context;
//     private readonly IWebHostEnvironment _environment;
//     public TodoService(DataContext context, IWebHostEnvironment environment)
//     {
//         _context = context;
//         _environment = environment;
//     }
//     public async Task<List<GetTodoDto>> GetTodoLists()
//     {
//         var list = await _context.Todos.Select(t => new GetTodoDto()
//         {
//             Id = t.Id,
//             Title = t.Title,
//             Description = t.Description,
//             ImageName = t.ImageName
//         }).ToListAsync();
//         return list;
//     }
//     public async Task<GetTodoDto> GetById(int id)
//     {
//         var find = await _context.Todos.FindAsync(id);
//         var response = new GetTodoDto()
//         {
//             Id = find.Id,
//             Title = find.Title,
//             Description = find.Description,
//             ImageName = find.ImageName
//         };
//         return response;
//     }
//     public async Task<GetTodoDto> AddTodo(AddTodoDto todo){
//         var response = new GetTodoDto()
//         {
//             Id = todo.Id,
//             Title = todo.Title,
//             Description = todo.Description,
//             ImageName = todo.Image.FileName
//         };
//         var newTodo = new Todo(){
//             Id = todo.Id,
//             Title = todo.Title,
//             Description = todo.Description,
//             ImageName = todo.Image.FileName
//         };
//         newTodo.ImageName = await UploadFile(todo.Image);
//         _context.Todos.Add(newTodo);
//         await _context.SaveChangesAsync();
//         return response;
//     }

//     public async Task<GetTodoDto> UpdateTodo(AddTodoDto todo){
//         var response = new GetTodoDto(){
//             Id = todo.Id,
//             Title = todo.Title,
//             Description = todo.Description,
//             ImageName = todo.Image.FileName
//         };

//         var find = await _context.Todos.FindAsync(todo.Id);
//         find.Title = todo.Title;
//         find.Description = todo.Description;

//         if (todo.Image != null)
//         {
//             find.ImageName = await UpdateFile(todo.Image,find.ImageName);
//         }
//         return response;
//     }
    
//     public async Task<string> UploadFile(IFormFile file){
//         if (file == null) return null;
//         var path = Path.Combine(_environment.WebRootPath,"todo");
//         if (Directory.Exists(path)==false) Directory.CreateDirectory(path);

//         var filepath = Path.Combine(path, file.FileName);
//         using (var stream = new FileStream(filepath, FileMode.Create)){
//             await file.CopyToAsync(stream);
//         }

//         return file.FileName;
//     }

//     public async Task<string> UpdateFile(IFormFile file, string oldFileName)
//     {
//         if(oldFileName != null){
//         var filepath = Path.Combine(_environment.WebRootPath,"todo",oldFileName);
//         if(File.Exists(filepath)==true) File.Delete(filepath);}
       
//         var newFilepath = Path.Combine(_environment.WebRootPath,"todo",file.FileName);
//         using (var stream = new FileStream(newFilepath,FileMode.Create))
//         {
//             await file.CopyToAsync(stream);
//         }
//         return file.FileName;
//     }
//     public async Task<int> DeleteTodo(int id){
//         var find = await _context.Todos.FindAsync(id);
//         _context.Remove(find);
//         return await _context.SaveChangesAsync();
//     }
// }
