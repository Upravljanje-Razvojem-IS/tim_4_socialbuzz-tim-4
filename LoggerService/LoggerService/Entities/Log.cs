using System;
using System.Collections.Generic;

#nullable disable

namespace LoggerService.Entities
{
    /// <summary>
    /// Model loga koji prikazuje informacije o dogadjaju neke akcije
    /// </summary>
    public partial class Log
    {

        /// <summary>
        /// Id loga
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// LogLevel (Information, Warning, Error)
        /// </summary>
        public string LogLevel { get; set; }

        /// <summary>
        /// Naizv mikroservisa koji je izvrsio neku akciju
        /// </summary>
        public string Microservice { get; set; }

        /// <summary>
        /// Poruka sta se desilo
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Vreme desavanja akcije
        /// </summary>
        public string TimeOfAction { get; set; }
    }
}
