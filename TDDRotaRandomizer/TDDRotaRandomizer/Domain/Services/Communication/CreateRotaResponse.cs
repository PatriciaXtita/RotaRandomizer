using RotaRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Domain.Services.Communication
{
    public class CreateRotaResponse : BaseResponse
    {
        public Rota Rota { get; private set; }

        public CreateRotaResponse(bool success, string message, Rota rota) : base(success, message)
        {
            Rota = rota;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="rota">Saved rota.</param>
        /// <returns>Response.</returns>
        public CreateRotaResponse(Rota rota) : this(true, string.Empty, rota)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public CreateRotaResponse(string message) : this(false, message, null)
        { }
    }
}
