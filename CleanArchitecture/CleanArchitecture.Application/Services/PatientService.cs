﻿using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Application.Interfaces.Services;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientDto> AddPatientAsync(PatientCreationDto patientDto)
        {
            var patient = new Patient(patientDto.Name);
            await _patientRepository.AddAsync(patient);
            await _patientRepository.SaveChangesAsync();

            return new PatientDto { Id = patient.Id, Name = patient.Name };
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _patientRepository.GetAllAsync();
            return patients.Select(p => new PatientDto { Id = p.Id, Name = p.Name }).ToList();
        }
    }

}
