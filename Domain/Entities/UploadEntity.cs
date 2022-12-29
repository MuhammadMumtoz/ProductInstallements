// namespace Domain.Entities;
// using System.ComponentModel.DataAnnotations;
// public class Todo {
//     [Key]
//     public int Id {get; set;}
//     public string Title {get; set;}
//     public string Description {get; set;}
//     public string ImageName {get; set;}
// }
// // record public class Todo(int Id, string Title, string Desctiption, string ImageName);

// namespace Domain.Entities;
// using Domain.Entities;
// using Microsoft.AspNetCore.Http;
// using System.ComponentModel.DataAnnotations;
// public class Employee
// {
//     [Key]
//     public int EmployeeId { get; set; }
//     public string FirstName { get; set; }
//     public string LastName { get; set; }
//     public string Email { get; set; }
//     public string PhoneNumber { get; set; }
//     public DateTime HireDate { get; set; }
//     public string JobId { get; set; }
//     public virtual Job Job { get; set; }
//     public int CommissionPct { get; set; }
//     public virtual List<JobHistory> JobHistories { get; set; }

//     public int? ManagerId { get; set; }
//     public Employee? Manager { get; set; }
//     public int DepartmentId { get; set; }
//     public virtual Department Department { get; set; }
//     [System.ComponentModel.DataAnnotations.Schema.NotMapped]
//     public IFormFile File { get; set; }
//     public string FileName { get; set; }
//     // public virtual List<Department> Departments { get; set; }

// }