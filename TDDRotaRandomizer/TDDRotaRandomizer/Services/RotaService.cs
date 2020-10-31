using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Threading.Tasks;
using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Domain.Services;
using RotaRandomizer.Domain.Services.Communication;
using RotaRandomizer.Models;

namespace RotaRandomizer.Services
{
    public class RotaService : IRotaService
    {
        private readonly IRotaRepository _rotaRepository;
        private readonly IShiftService _shiftService;
        private readonly IUnitOfWork _unitOfWork;

        public RotaService(IRotaRepository rotaRepository, IShiftService shiftService, IUnitOfWork unitOfWork)
        {
            _rotaRepository = rotaRepository;
            _shiftService = shiftService;
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<Rota>> ListAsync()
        {
            return await _rotaRepository.ListAsync();
        }

        public async Task<CreateRotaResponse> SaveAsync(Rota rota)
        {
            try
            {
                //Find the beginning and end date of next Rota
                DateTime beginningOfRotaDay = getRotaStart(rota.Start, DayOfWeek.Monday);   //TODO - put this day of week configurable
                DateTime endOfRotaDay = getRotaEnd(beginningOfRotaDay);
                rota.Start = beginningOfRotaDay;
                rota.End = endOfRotaDay;

                //Create Shifts for this Rota  TODO          
                //rota.Shifts = shiftService.CreateShiftsForRota(beginningOfRotaDay, endOfRotaDay);

                await _rotaRepository.AddAsync(rota);
                await _unitOfWork.CompleteAsync();

                return new CreateRotaResponse(rota);
            }
            catch (Exception ex)
            {
                return new CreateRotaResponse($"An error occurred when saving the rota: {ex.Message}");
            }
        }

        private DateTime getRotaEnd(DateTime beginningOfRotaDay)
        {
            DateTime result = beginningOfRotaDay;
            List<DayOfWeek> nonWorkingDays = new List<DayOfWeek>(); //TODO - put nonWorkingDays configurable
            nonWorkingDays.Add(DayOfWeek.Saturday);
            nonWorkingDays.Add(DayOfWeek.Sunday);

            int rotaWorkingDays = 10; //TODO - make it configurable

            int rotaDuration = 1;
            while (rotaDuration < rotaWorkingDays)
            {
                result.AddDays(1);
                if (!nonWorkingDays.Contains(result.DayOfWeek))
                    rotaDuration++;
            }
            return result;
        }

        private DateTime getRotaStart(DateTime date, DayOfWeek dayofWeekStart)
        {
            DateTime result = date.Date;
            while (result.DayOfWeek != dayofWeekStart)
            {
                result.AddDays(1);
            }
            return result;
        }
    }
}
