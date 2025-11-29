// DTOs for LKPSalary Entity

using System;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.BaseLibrary.DTOs
{
    // Read DTO (OUTPUT)
    public class SalaryReadDto
    {
        public int SalaryID { get; set; }
        public int EmployeeID { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime PayDate { get; set; }
    }

    // Create DTO (INPUT)
    public class SalaryCreateDto
    {
        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Base Salary is required.")]
        public decimal BaseSalary { get; set; }

        [Required(ErrorMessage = "Bonus is required.")]
        public decimal Bonus { get; set; }

        [Required(ErrorMessage = "Deductions is required.")]
        public decimal Deductions { get; set; }

        [Required(ErrorMessage = "Net Salary is required.")]
        public decimal NetSalary { get; set; }

        [Required(ErrorMessage = "Pay Date is required.")]
        public DateTime PayDate { get; set; }
    }

    // Update DTO (INPUT)
    public class SalaryUpdateDto
    {
        [Required(ErrorMessage = "Salary ID is required for update.")]
        public int SalaryID { get; set; }

        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Base Salary is required.")]
        public decimal BaseSalary { get; set; }

        [Required(ErrorMessage = "Bonus is required.")]
        public decimal Bonus { get; set; }

        [Required(ErrorMessage = "Deductions is required.")]
        public decimal Deductions { get; set; }

        [Required(ErrorMessage = "Net Salary is required.")]
        public decimal NetSalary { get; set; }

        [Required(ErrorMessage = "Pay Date is required.")]
        public DateTime PayDate { get; set; }
    }
}




