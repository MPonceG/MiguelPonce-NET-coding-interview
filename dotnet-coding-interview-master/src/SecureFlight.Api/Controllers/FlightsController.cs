﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Models;
using SecureFlight.Api.Utils;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;
using System;

namespace SecureFlight.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController(IService<Flight> flightService, IMapper mapper)
    : SecureFlightBaseController(mapper)
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FlightDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> Get()
    {
        var flights = await flightService.GetAllAsync();
        return MapResultToDataTransferObject<IReadOnlyList<Flight>, IReadOnlyList<FlightDataTransferObject>>(flights);
    }
    
    [HttpGet("/active")]
    [ProducesResponseType(typeof(IEnumerable<FlightDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> GetAllActive()
    {
        var flights = await flightService.FilterAsync(f => f.FlightStatusId == Core.Enums.FlightStatus.Active);
        return MapResultToDataTransferObject<IReadOnlyList<Flight>, IReadOnlyList<FlightDataTransferObject>>(flights);
    }


    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<FlightDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> Post()
    {
        var flights = await flightService.GetAllAsync();
        return MapResultToDataTransferObject<IReadOnlyList<Flight>, IReadOnlyList<FlightDataTransferObject>>(flights);
    }
}