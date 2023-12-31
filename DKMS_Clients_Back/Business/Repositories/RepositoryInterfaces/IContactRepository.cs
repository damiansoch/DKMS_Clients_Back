﻿using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<int> AddAsync(Contact contact);
    Task<int> UpdateAsync(Contact contact);
}