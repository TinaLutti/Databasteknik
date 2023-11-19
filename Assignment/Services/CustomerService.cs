using Assignment.Entities;
using Assignment.Models;
using Assignment.Repositories;
using System;
using System.Linq.Expressions;

namespace Assignment.Services;

public class CustomerService
{
    private readonly AddressRepository _addressRepository;
    private readonly CustomerRepository _customerRepository;

    public CustomerService(AddressRepository addressRepository, CustomerRepository customerRepository)
    {
        _addressRepository = addressRepository;
        _customerRepository = customerRepository;
    }

    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
       
        if (!await _customerRepository.ExistsAsync(x => x.Email == form.Email))
        {
            
            AddressEntity addressEntity = await _addressRepository.GetAsync(x => x.StreetName == form.StreetName && x.PostalCode == form.PostalCode);
            addressEntity ??= await _addressRepository.CreateAsync(new AddressEntity { StreetName = form.StreetName, PostalCode = form.PostalCode, City = form.City });

            
            CustomerEntity customerEntity = await _customerRepository.CreateAsync(new CustomerEntity { FirstName = form.FirstName, LastName = form.LastName, Email = form.Email, AddressId = addressEntity.Id });
            if (customerEntity != null)
                return true;

        }

        return false;

    }

    public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers;
    }

    //HÄMTA EN kund via mail
    public async Task<CustomerEntity> GetAsync(string email)
    {
        return await _customerRepository.GetAsync(x => x.Email == email);
    }
    // DELETE

    // Metod för att ta bort en kund baserat på ett uttryck
    public async Task<bool> DeleteAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        // Hämta kund baserat på det givna uttrycket
        var member = await _customerRepository.GetAsync(expression);

        // Om kunden finns, ta bort och spara ändringar
        if (member != null)
        {
            await _customerRepository.DeleteAsync(expression);
            return true;
        }

        // Om kund inte finns
        return false;
    }

    // Metod för att ta bort en medlem baserat på e-postadress
    public async Task<bool> DeleteAsync(string email)
    {
        // Använd DeleteAsync från _customer Repository för att ta bort kund baserat på e-postadress
        return await _customerRepository.DeleteAsync(x => x.Email == email);
    }

    //UPDATE

    public async Task<bool> UpdateCustomerAsync(CustomerUpdateForm form, string email)
    {
        // Hämta kund baserat på e-postadress
        var existingCustomer = await _customerRepository.GetAsync(x => x.Email == email);

        // Om kunden inte finns, returnera false
        if (existingCustomer == null || existingCustomer.Address == null)
        {
            return false;
        }

        // Uppdatera kundens egenskaper med värden från formuläret
        existingCustomer.FirstName = form.FirstName;
        existingCustomer.LastName = form.LastName;
        existingCustomer.Address.StreetName = form.StreetName;
        existingCustomer.Address.PostalCode = form.PostalCode;
        existingCustomer.Address.City = form.City;

        // Uppdatera andra egenskaper enligt behov

        // Uppdatera kund i databasen
        var updatedCustomer = await _customerRepository.UpdateAsync(existingCustomer);

        // Returnera true om uppdateringen lyckades, annars false
        return updatedCustomer != null;
    }
}

