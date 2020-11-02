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
        private readonly IConfigService _configService;
        private readonly IUnitOfWork _unitOfWork;

        public RotaService(IRotaRepository rotaRepository, IShiftService shiftService, IUnitOfWork unitOfWork, IConfigService configService)
        {
            _rotaRepository = rotaRepository;
            _shiftService = shiftService;
            _unitOfWork = unitOfWork;
            _configService = configService;
        }


        public async Task<IEnumerable<Rota>> ListAsync()
        {
            return await _rotaRepository.ListAsync();
        }

        public async Task<CreateRotaResponse> SaveAsync(Rota rota)
        {
            try
            {    //Find the beginning and end date of next Rota
                DayOfWeek startDayOfWeek = (DayOfWeek)Convert.ToInt32((await _configService.GetAsyncRotaStart()).Value);
                DateTime beginningOfRotaDay = GetRotaStart(rota.Start, startDayOfWeek);

                Rota existingRota = await _rotaRepository.Find(beginningOfRotaDay);
                if (existingRota != null)
                {
                    return new CreateRotaResponse(false, "Rota already exists", existingRota);
                }
                Rota inBetween = await InBetweenRotaAsyncTest(beginningOfRotaDay);
                if (inBetween != null)
                {
                    return new CreateRotaResponse(false, "Rota already exists for this period", inBetween);
                }

                DateTime endOfRotaDay = await GetRotaEnd(beginningOfRotaDay);
                rota.Start = beginningOfRotaDay;
                rota.End = endOfRotaDay;

                rota.Shifts = await _shiftService.CreateShiftsForRota(beginningOfRotaDay, endOfRotaDay);

                await _rotaRepository.AddAsync(rota);
                await _unitOfWork.CompleteAsync();

                return new CreateRotaResponse(true, "Created with Success", rota);
            }
            catch (Exception ex)
            {
                return new CreateRotaResponse($"An error occurred when saving the rota: {ex.Message}");
            }
        }

        public async Task<Rota> InBetweenRotaAsyncTest(DateTime beginningOfRotaDay)
        {
            List<Rota> rotas = (await ListAsync()).ToList();
            foreach (Rota rota in rotas)
            {
                if (rota.Start <= beginningOfRotaDay && rota.End >= beginningOfRotaDay)
                {
                    return rota;
                }
            }
            return null;
        }

        public async Task<DateTime> GetRotaEnd(DateTime beginningOfRotaDay)
        {
            DateTime result = beginningOfRotaDay;
            List<DayOfWeek> nonWorkingDays = _configService.GetNonWorkingDays().ToList();

            int rotaWorkingDays = Convert.ToInt32((await _configService.GetAsyncRotaDuration()).Value);

            int rotaDuration = 1;
            while (rotaDuration < rotaWorkingDays)
            {
                result = result.AddDays(1);
                if (!nonWorkingDays.Contains(result.DayOfWeek))
                    rotaDuration++;
            }
            return result;
        }

        public DateTime GetRotaStart(DateTime date, DayOfWeek dayofWeekStart)
        {
            DateTime result = date.Date;
            while (result.DayOfWeek != dayofWeekStart)
            {
                result = result.AddDays(1);
            }
            return result;
        }
    }
}
