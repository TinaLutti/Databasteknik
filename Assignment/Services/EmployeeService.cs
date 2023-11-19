using Assignment.Entities;
using Assignment.Models;
using Assignment.Repositories;
using System.Linq.Expressions;

namespace Assignment.Services;

public class EmployeeService
{
    private readonly AddressRepository _addressRepository;    
    private readonly EmployeeRepository _employeeRepository;

    public EmployeeService(AddressRepository addressRepository, EmployeeRepository employeeRepository)
    {
        _addressRepository = addressRepository;        
        _employeeRepository = employeeRepository;
    }

    public async Task<bool> CreateEmployeeAsync(EmployeeRegistrationForm form)
    {
        
        if (!await _employeeRepository.ExistsAsync(x => x.Email == form.Email))
        {
            
            AddressEntity addressEntity = await _addressRepository.GetAsync(x => x.StreetName == form.StreetName && x.PostalCode == form.PostalCode);

            
            addressEntity ??= await _addressRepository.CreateAsync(new AddressEntity { StreetName = form.StreetName, PostalCode = form.PostalCode, City = form.City });

            
            EmployeeEntity employeeEntity = await _employeeRepository.CreateAsync(new EmployeeEntity { FirstName = form.FirstName, LastName = form.LastName, Email = form.Email, AddressId = addressEntity.Id, Salary=form.Salary });

            
            if (employeeEntity != null)
                return true;
        }

      
        return false;
    }

    public async Task<IEnumerable<EmployeeEntity>> GetAllAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees;
    }

        //HÄMTA EN kund via mail
        public async Task<EmployeeEntity> GetAsync(string email)
    {
        return await _employeeRepository.GetAsync(x => x.Email == email);
    }
    // DELETE

    // Metod för att ta bort en anställd baserat på ett uttryck
    public async Task<bool> DeleteAsync(Expression<Func<EmployeeEntity, bool>> expression)
    {
        // Hämta anställd baserat på det givna uttrycket
        var employee = await _employeeRepository.GetAsync(expression);

        // Om anställd finns, ta bort och spara ändringar
        if (employee != null)
        {
            await _employeeRepository.DeleteAsync(expression);
            return true;
        }

        // Om anställd inte finns
        return false;
    }

    // Metod för att ta bort en anställd baserat på e-postadress
    public async Task<bool> DeleteAsync(string email)
    {
        // Använd DeleteAsync från _employee Repository för att ta bort kund anst på e-postadress
        return await _employeeRepository.DeleteAsync(x => x.Email == email);
    }

    //UPDATE

    public async Task<bool> UpdateEmployeeAsync(EmployeeUpdateForm form, string email)
    {
        // Hämta anst baserat på e-postadress
        var existingEmployee = await _employeeRepository.GetAsync(x => x.Email == email);

        // Om asntälld inte finns, returnera false
        if (existingEmployee == null || existingEmployee.Address == null)
        {
            return false;
        }

        // Uppdatera anställdes egenskaper med värden från formuläret
        existingEmployee.FirstName = form.FirstName;
        existingEmployee.LastName = form.LastName;
        existingEmployee.Salary= form.Salary;
        existingEmployee.Address.StreetName = form.StreetName;
        existingEmployee.Address.PostalCode = form.PostalCode;
        existingEmployee.Address.City = form.City;

        

        // Uppdatera anställd i databasen
        var updatedEmployee = await _employeeRepository.UpdateAsync(existingEmployee);

        // Returnera true om uppdateringen lyckades, annars false
        return updatedEmployee != null;
    }
}
